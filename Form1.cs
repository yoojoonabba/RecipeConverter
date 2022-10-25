using rcpChange.New;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rcpChange
{
    public partial class Form1 : Form
    {

        private string _selectedDbPath = string.Empty;
        
        public Form1()
        {
            InitializeComponent();
        }

       

        private async void btStart_Click(object sender, EventArgs e)
        {
            var tokenSource = new CancellationTokenSource();

            if (string.IsNullOrWhiteSpace(_selectedDbPath))
            {
                lbState.Text = "상태 : DB 경로를 선택해 주세요!!";
                return;
            }

            var groups = await GetGroupsNameAsync();

            foreach(var group in groups)
            {
                await GroupDataProcessAsync(group, tokenSource.Token);
                var recipes = await GetDevicesNameAsync(group, tokenSource.Token);

                foreach(var recipe in recipes)
                {
                    // tokenSource.Cancel(); // 필요시 비동기 작업 취소...
                    lbState.Text = await DeviceDataProcessAsync(group, recipe, tokenSource.Token);
                }
            }
            lbState.Text = "상태 : 완료!!";
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            dlg.ShowDialog(); 
            _selectedDbPath = dlg.SelectedPath;
            lbPath.Text = dlg.SelectedPath;
        }


        private async Task GroupDataProcessAsync(string groupName, CancellationToken token)
        {
            var files = await GetFilesNameAsync(Path.Combine(_selectedDbPath, groupName), token);
            foreach (var fileName in files)
            {
                var dirPath  = Path.Combine(_selectedDbPath, groupName, fileName);
                var oldRecipe = await ReadOldRecipeAsync(dirPath, fileName, token);
                var newRecipe = new New.MtRecipeIndexes();
                for (int i = 0; i < 50; i++)
                {
                    newRecipe.Idx[i].Pos.Val = oldRecipe.Idx[i].Pos.Val;
                    newRecipe.Idx[i].Pos.Min = oldRecipe.Idx[i].Pos.Min;
                    newRecipe.Idx[i].Pos.Max = oldRecipe.Idx[i].Pos.Max;

                    newRecipe.Idx[i].Speed.Val = oldRecipe.Idx[i].Speed.Val;
                    newRecipe.Idx[i].Speed.Min = oldRecipe.Idx[i].Speed.Min;
                    newRecipe.Idx[i].Speed.Max = oldRecipe.Idx[i].Speed.Max;

                    newRecipe.Idx[i].Acc.Val = oldRecipe.Idx[i].Acc.Val;
                    newRecipe.Idx[i].Acc.MAT = oldRecipe.Idx[i].Acc.MAT;

                    newRecipe.Idx[i].Name = oldRecipe.Idx[i].Name;
                    if (oldRecipe.Idx[i].IsCommon)
                        newRecipe.Idx[i].Mode = 1;
                    else
                        newRecipe.Idx[i].Mode = 0;
                }
                await WriteNewRecipeAsync(Path.Combine(_selectedDbPath, $"_{groupName}"), fileName, newRecipe, token);
            }
            await FileCopyAsync(Path.Combine(_selectedDbPath, groupName), "group.xml",
                                Path.Combine(_selectedDbPath, $"_{groupName}"), "group.xml", token);
        }


        private async Task<string> DeviceDataProcessAsync(string groupName, string deviceName, CancellationToken token)
        {
            return await Task<string>.Run(async() =>
            {
                var files = await GetFilesNameAsync(Path.Combine(_selectedDbPath, groupName, deviceName), token);

                foreach (var fileName in files)
                {
                    if(token.IsCancellationRequested)
                    {
                        return $"상태 : 그룹[{groupName}] 디바이스[{deviceName}] 취소!!";
                    }

                    var dirPath = Path.Combine(_selectedDbPath, groupName, deviceName);
                    var oldRecipe = await ReadOldRecipeAsync(dirPath, fileName, token);
                    var newRecipe = new New.MtRecipeIndexes();
                    for (int i = 0; i < 50; i++)
                    {
                        newRecipe.Idx[i].Pos.Val = oldRecipe.Idx[i].Pos.Val;
                        newRecipe.Idx[i].Pos.Min = oldRecipe.Idx[i].Pos.Min;
                        newRecipe.Idx[i].Pos.Max = oldRecipe.Idx[i].Pos.Max;

                        newRecipe.Idx[i].Speed.Val = oldRecipe.Idx[i].Speed.Val;
                        newRecipe.Idx[i].Speed.Min = oldRecipe.Idx[i].Speed.Min;
                        newRecipe.Idx[i].Speed.Max = oldRecipe.Idx[i].Speed.Max;

                        newRecipe.Idx[i].Acc.Val = oldRecipe.Idx[i].Acc.Val;
                        newRecipe.Idx[i].Acc.MAT = oldRecipe.Idx[i].Acc.MAT;

                        newRecipe.Idx[i].Name = oldRecipe.Idx[i].Name;

                        if (oldRecipe.Idx[i].IsCommon)
                            newRecipe.Idx[i].Mode = 1;
                        else
                            newRecipe.Idx[i].Mode = 0;
                    }

                    await WriteNewRecipeAsync(Path.Combine(_selectedDbPath, $"_{groupName}", deviceName), fileName, newRecipe, token);

                }
                await DeviceRecipeUpdate(groupName, deviceName, token);

                //await FileCopyAsync(Path.Combine(_selectedDbPath, groupName, deviceName), "device.xml",
                //                    Path.Combine(_selectedDbPath, $"_{groupName}", deviceName), "device.xml", token);
                return $"상태 : 그룹[{groupName}] 디바이스[{deviceName}] 완료!!";
            });
        }



        private async Task FileCopyAsync(string srcDirPath, string srcFileName, string destDirPath, string destFileName, CancellationToken token)
        {
            await Task.Run(() =>
            {
                var srcDirInfo = new DirectoryInfo(srcDirPath);
                if (!srcDirInfo.Exists)
                    return;

                var srcFilePath = Path.Combine(srcDirPath, srcFileName);
                var srcFileInfo = new FileInfo(srcFilePath);
                if (srcFileInfo is null)
                    return;

                var destDirInfo = new DirectoryInfo(destDirPath);
                if (!destDirInfo.Exists)
                    destDirInfo.Create();

                var destFilePath = Path.Combine(destDirPath, destFileName);
                srcFileInfo.CopyTo(destFilePath, true);
            });
        }



        /// <summary>
        /// DB 폴더의 Group리스트를 반환..
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> GetGroupsNameAsync()
        {
            return await Task<List<string>>.Run(() =>
            {
                var groups = new List<string>();
                groups.Clear();
                string[] dirs = Directory.GetDirectories(_selectedDbPath, "*.*", SearchOption.TopDirectoryOnly);
                

                foreach (var d in dirs)
                {
                    var dir = new DirectoryInfo(d);
                    if (!dir.Name.Equals("[Global]") && !dir.Name.Equals("Common") 
                     && !dir.Name.Substring(0,1).Equals("_")) //[Global] 폴더는 제외..전역 모터데이터만 저장되는곳임..
                        groups.Add(dir.Name);
                }
                return groups;
            });
        }



        /// <summary>
        /// 해당 그룹의 디바이스 리스트를 반환..
        /// </summary>
        /// <param name="groupDir"></param>
        /// <returns></returns>
        private async Task<List<string>> GetDevicesNameAsync(string groupDir, CancellationToken token)
        {
            return await Task<List<string>>.Run(() =>
            {
                var recipes = new List<string>();
                recipes.Clear();
                string targetDir = System.IO.Path.Combine(_selectedDbPath, groupDir);

                string[] dirs = Directory.GetDirectories(targetDir, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var d in dirs)
                {
                    var dir = new DirectoryInfo(d);
                    if (!dir.Name.Equals("[Global]"))
                        recipes.Add(dir.Name);
                }
                return recipes;
            });
        }



        private async Task<List<string>> GetFilesNameAsync(string dirPath, CancellationToken token)
        {
            return await Task<List<string>>.Run(()=>
            {
                var files = new List<string>();
                files.Clear();
                var dInfo = new DirectoryInfo(dirPath);
                FileInfo[] infos = dInfo.GetFiles("*.xml");

                foreach (var f in infos)
                {
                    if (!f.Name.Equals("group.xml") && !f.Name.Equals("device.xml")) //[Global] 폴더는 제외..전역 모터데이터만 저장되는곳임..
                        files.Add(f.Name);
                }
                return files;
            });
        }


        private async Task WriteNewRecipeAsync(string dirPath, string fileName, New.MtRecipeIndexes indexes, CancellationToken token)
        {
            await Task.Run(() =>
            {
                FileService.Xml.Write<rcpChange.New.MtRecipeIndexes>(dirPath, fileName, indexes);
            });
        }



        private async Task<Old.MotorPosIndexArray> ReadOldRecipeAsync(string dirPath, string fileName, CancellationToken token)
        {
            return await Task<Old.MotorPosIndexArray>.Run(() =>
            {
                try
                {
                    var indexArray = FileService.Xml.Read<Old.MotorPosIndexArray>(dirPath, fileName);
                    if (indexArray is null)
                        return new Old.MotorPosIndexArray();
                    return
                        indexArray;
                }
                catch (Exception ex)
                {
                    return new Old.MotorPosIndexArray();
                }
            });
        }

        private async Task DeviceRecipeUpdate(string groupName, string deviceName, CancellationToken token)
        {
            await Task.Run(() =>
            {
                string groupPath = Path.Combine(_selectedDbPath, groupName);
                string devicePath = Path.Combine(_selectedDbPath, groupName, deviceName);
                var recipeOld = FileService.Xml.Read<Old.DeviceArray>(devicePath, "device.xml");
                var groupOld  = FileService.Xml.Read<Old.GroupArray>(groupPath, "group.xml");

                var recipeNew = new DeviceDataArray();
                int i = 0;
                for(i = 0; i < 450; i++)
                {
                    recipeNew.Idx[i].Val = recipeOld.Idx[i].Val;
                    recipeNew.Idx[i].Key = FileService.EnumHelper.GetDescription((New.Def.Device)i);
                    recipeNew.Idx[i].IndexType = (int)New.Def.IndexType.Device;
                }
                for (i = 450; i < 650; i++)
                {
                    recipeNew.Idx[i].Val = groupOld.Idx[i - 450].Val;
                    recipeNew.Idx[i].Key = FileService.EnumHelper.GetDescription((New.Def.Device)i);
                    recipeNew.Idx[i].IndexType = (int)New.Def.IndexType.Group;
                }
                for (i = 650; i < 1000; i++)
                {
                    recipeNew.Idx[i].Val = recipeOld.Idx[i].Val;
                    recipeNew.Idx[i].Key = FileService.EnumHelper.GetDescription((New.Def.Device)i);
                    recipeNew.Idx[i].IndexType = (int)New.Def.IndexType.Device;
                }

                string devicePathNew = Path.Combine(_selectedDbPath, $"_{groupName}", deviceName);
                FileService.Xml.Write<New.DeviceDataArray>(devicePathNew, "DeviceData.xml", recipeNew);
            });
        }
    }
}
