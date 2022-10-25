using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace rcpChange
{
    public static class FileService
    {
        public static void Delete(string dirPath, string fileName)
        {
            if (fileName != null && File.Exists(Path.Combine(dirPath, fileName)))
            {
                File.Delete(Path.Combine(dirPath, fileName));
            }
        }


        /// <summary>
        /// json file read/write class
        /// </summary>
        public static class Json
        {

            /// <summary>
            /// Writes the given object instance to a Json file.
            /// <para>Object type must have a parameterless constructor.</para>
            /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
            /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
            /// </summary>
            /// <typeparam name="T">The type of object being written to the file.</typeparam>
            /// <param name="filePath">The file path to write the object instance to.</param>
            /// <param name="objectToWrite">The object instance to write to the file.</param>
            /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
            public static void Write<T>(string filePath, T objectToWrite, bool append = false) where T : new()
            {
                TextWriter writer = null;
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
                try
                {
                    var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Newtonsoft.Json.Formatting.Indented, settings);
                    writer = new StreamWriter(filePath, append);
                    writer.Write(contentsToWriteToFile);
                }
                finally
                {
                    writer?.Close();
                }

            }

            public static void Save<T>(string dirPath, string fileName, T content)
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                var fileContent = JsonConvert.SerializeObject(content);
                File.WriteAllText(Path.Combine(dirPath, fileName), fileContent, Encoding.UTF8);
            }


            public static async void WriteAsync<T>(string filePath, T objectToWrite, bool append = false) where T : new()
            {
                TextWriter writer = null;
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                };
                try
                {
                    var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Newtonsoft.Json.Formatting.Indented, settings);
                    writer = new StreamWriter(filePath, append);
                    await writer.WriteAsync(contentsToWriteToFile);
                }
                finally
                {
                    writer?.Close();
                }
            }

            /// <summary>
            /// Reads an object instance from an Json file.
            /// <para>Object type must have a parameterless constructor.</para>
            /// </summary>
            /// <typeparam name="T">The type of object to read from the file.</typeparam>
            /// <param name="filePath">The file path to read the object instance from.</param>
            /// <returns>Returns a new instance of the object read from the Json file.</returns>
            public static T Read<T>(string filePath) where T : new()
            {
                TextReader reader = null;
                try
                {
                    reader = new StreamReader(filePath);
                    var fileContents = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(fileContents);
                }
                finally
                {
                    reader?.Close();
                }
            }

            public static T Read<T>(string dirPath, string fileName)
            {
                var path = Path.Combine(dirPath, fileName);
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<T>(json);
                }

                return default;
            }
        }


        /// <summary>
        /// xml file read/write class
        /// </summary>
        public static class Xml
        {

            public static T Read<T>(string dirPath, string fileName) where T : new()
            {
                try
                {
                    var path = Path.Combine(dirPath, fileName);
                    if (File.Exists(path))
                    {
                        var serializer = new XmlSerializer(typeof(T));
                        using (var reader = new StreamReader(path))
                        {
                            return (T)serializer.Deserialize(reader);
                        }
                    }
                    return default(T);
                }
                catch (Exception ex)
                {
                    return default(T);
                }
            }

            public static void Write<T>(string dirPath, string fileName, T objectToWrite, bool append = false) where T : new()
            {
                try
                {
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);

                    var path = Path.Combine(dirPath, fileName);
                    var serializer = new XmlSerializer(typeof(T));

                    using (var writer = new StreamWriter(path, append))
                    {
                        serializer.Serialize(writer, objectToWrite);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }



        /// <summary>
        /// Csv파일 read/write class
        /// </summary>
        public static class Csv
        {
            /// <summary>
            /// csv file에서 읽어들임..
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="filePath"></param>
            /// <returns></returns>
            public static IEnumerable<T> Read<T>(string filePath)
            {
                try
                {
                    using (var reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            return csv.GetRecords<T>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return default(IEnumerable<T>);
                }
            }

            /// <summary>
            /// csv file에 write함...
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="filePath"></param>
            /// <param name="obj"></param>
            public static void Write<T>(string filePath, IEnumerable<T> obj)
            {
                try
                {
                    //using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                    //{
                    //    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    //    {
                    //        csv.WriteRecords(obj);
                    //    }
                    //}
                }
                catch
                {

                }
            }
        }


        public static class EnumHelper
        {
            public static string GetDescription(Enum source)
            {
                var fi = source.GetType().GetField(source.ToString());
                if (null == fi)
                    return "";
                var attr = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attr != null && 0 < attr.Length)
                    return attr[0].Description;
                else
                    return source.ToString();
            }
        }
    }
}
