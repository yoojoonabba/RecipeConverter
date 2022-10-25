using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace rcpChange.New
{

    #region => New device data
    [Serializable]
    [XmlRoot("DeviceNameInfo")]
    public class DeviceNameInfo
    {
        [XmlElement("Index")]
        public int Index { get; set; }

        [XmlElement("DeviceName")]
        public string DeviceName { get; set; }

        [XmlElement("PkgSize")]
        public string PkgSize { get; set; }

        [XmlElement("CustomerInfo")]
        public string CustomerInfo { get; set; }

        [XmlElement("SecsGemRecipe")]
        public string SecsGemRecipe { get; set; }

        [XmlElement("Comment")]
        public string Comment { get; set; }
    }

    [Serializable]
    [XmlRoot("DeviceNameArray")]
    public class DeviceNameArray
    {
        [XmlElement("DeviceNameInfo")]
        public DeviceNameInfo[] Idx { get; set; } = new DeviceNameInfo[1000];

        public DeviceNameArray()
        {
            for (int i = 0; i < Idx.Length; i++)
            {
                this.Idx[i] = new DeviceNameInfo();
                this.Idx[i].Index = i + 1;
                this.Idx[i].DeviceName = i == 0 ? "" : "";
                this.Idx[i].PkgSize = "";
                this.Idx[i].CustomerInfo = "";
                this.Idx[i].SecsGemRecipe = "";
                this.Idx[i].Comment = "";
            }
        }
    }

    [Serializable]
    [XmlRoot("DeviceDataIdx")]
    public class DeviceDataIdx
    {
        [XmlAttribute("No")]
        public int No { get; set; }
        [XmlAttribute("Key")]
        public string Key { get; set; }
        [XmlAttribute("Val")]
        public double Val { get; set; }
        [XmlAttribute("IndexType")]
        public int IndexType { get; set; }
        [XmlAttribute("Unit")]
        public string Unit { get; set; }
        [XmlAttribute("PlusLimit")]
        public double PlusLimit { get; set; }
        [XmlAttribute("MinusLimit")]
        public double MinusLimit { get; set; }
    }

    [Serializable]
    [XmlRoot("DeviceDataArray")]
    public class DeviceDataArray
    {
        [XmlElement("DeviceDataIdx")]
        public DeviceDataIdx[] Idx { get; set; } = new DeviceDataIdx[(int)Def.Device.Max];
        public DeviceDataArray()
        {
            for (int i = 0; i < Idx.Length; i++)
            {
                this.Idx[i] = new DeviceDataIdx();
                this.Idx[i].No = i;
                this.Idx[i].Key = $"{i:000}";
                this.Idx[i].Val = 0;
                if (i >= 450 && i <= 899)
                    this.Idx[i].IndexType = (int)Def.IndexType.Group;
                else
                    this.Idx[i].IndexType = (int)Def.IndexType.Device;
                this.Idx[i].Unit = string.Empty;
                this.Idx[i].PlusLimit = 999.999;
                this.Idx[i].MinusLimit = -999.999;
            }
        }
    }


    #endregion





    #region => Enums
    public static class Def
    {
        public enum IndexType
        {
            Device = 0,
            Group = 1,
            Common = 2,
        }


        /// <summary>
        /// 디바이스 데이터 정의..
        /// Description에 화면에 표시할 이름을 작성
        /// </summary>
        public enum Device
        {
            // 0 ~ 899 Device Parameter
            [Description("PCB 두께(mm)")]
            PcbThickness = 0,

            [Description("상면 클리닝 반복 횟수")]
            TopCleanRepeatCnt = 1,
            [Description("하면 클리닝 반복 횟수")]
            BtmCleanRepeatCnt = 2,


            [Description("RFID 읽기 제한 시간[Sec]")]
            ReadRfidTimeLimit = 11,
            [Description("RFID 읽기 재시도 회수[ea]")]
            ReadRfidRetryCnt = 12,
            [Description("RFID 코드 총 길이")]
            ReadRfidLength = 13,


            [Description("2D코드 읽기 제한 시간[Sec]")]
            BarcodeReadTimeLimit = 15,
            [Description("2D 읽기 재시도 회수[ea]")]
            BarcodeReadRetryCnt = 16,

            [Description("2D Code 총 길이")]
            Read2DLength = 17,
            [Description("2D Code 디바이스 ID 길이")]
            Read2DDeviceIDLength = 18,
            [Description("2D Code 랏 ID 길이")]
            Read2DLotIDLength = 19,
            [Description("2D Code 코드 ID 길이")]
            Read2DCodeIDLength = 20,


            [Description("좌 상단 PRS 마크 좌표(X)")]
            LeftTopPRsCoordX = 30,
            [Description("좌 상단 PRS 마크 좌표(Y)")]
            LeftTopPRsCoordY = 31,
            [Description("우 상단 PRS 마크 좌표(X)")]
            RightTopPRsCoordX = 32,
            [Description("우 상단 PRS 마크 좌표(Y)")]
            RightTopPRsCoordY = 33,
            [Description("우 하단 PRS 마크 좌표(X)")]
            RightBtmPRsCoordX = 34,
            [Description("우 하단 PRS 마크 좌표(Y)")]
            RightBtmPRsCoordY = 35,


            [Description("매거진 슬롯 00 샘플 사용")]
            MgzSlotSampleMap00 = 50,
            MgzSlotSampleMap01 = 51,
            MgzSlotSampleMap02 = 52,
            MgzSlotSampleMap03 = 53,
            MgzSlotSampleMap04 = 54,
            MgzSlotSampleMap05 = 55,
            MgzSlotSampleMap06 = 56,
            MgzSlotSampleMap07 = 57,
            MgzSlotSampleMap08 = 58,
            MgzSlotSampleMap09 = 59,
            MgzSlotSampleMap10 = 60,
            MgzSlotSampleMap11 = 61,
            MgzSlotSampleMap12 = 62,
            MgzSlotSampleMap13 = 63,
            MgzSlotSampleMap14 = 64,
            MgzSlotSampleMap15 = 65,
            MgzSlotSampleMap16 = 66,
            MgzSlotSampleMap17 = 67,
            MgzSlotSampleMap18 = 68,
            MgzSlotSampleMap19 = 69,
            MgzSlotSampleMap20 = 70,
            MgzSlotSampleMap21 = 71,
            MgzSlotSampleMap22 = 72,
            MgzSlotSampleMap23 = 73,
            MgzSlotSampleMap24 = 74,
            MgzSlotSampleMap25 = 75,
            MgzSlotSampleMap26 = 76,
            MgzSlotSampleMap27 = 77,
            MgzSlotSampleMap28 = 78,
            MgzSlotSampleMap29 = 79,


            [Description("매거진 슬롯 개수")]
            MgzSlotCnt = 450,
            [Description("매거진 폭(mm)")]
            MgzWidth = 451,


            [Description("자재 길이 (mm)")]
            PcbLength = 453,
            [Description("자재 폭 (mm)")]
            PcbWidth = 454,

            [Description("인-레일 : 픽업 진공 형성 시간")]
            InRailPickVacOnTime = 460,

            [Description("보트1(상면클린) : 픽업 진공 형성(피커) 시간")]
            Boat1PickVacOnTime = 461,
            [Description("보트1(상면클린) : 픽업 블로우(보트) 시간")]
            Boat1PickBlowTime = 462,
            [Description("보트1(상면클린) : 플레이스 다운(피커) 시간")]
            Boat1PlaceDownTime = 463,
            [Description("보트1(상면클린) : 플레이스 블로우(피커) 시간")]
            Boat1PlaceBlowTime = 464,

            [Description("보트2(하면클린) : 픽업 진공 형성(피커) 시간")]
            Boat2PickVacOnTime = 465,
            [Description("보트2(하면클린) : 픽업 블로우(보트) 시간")]
            Boat2PickBlowTime = 466,
            [Description("보트2(하면클린) : 플레이스 다운(피커) 시간")]
            Boat2PlaceDownTime = 467,
            [Description("보트2(하면클린) : 플레이스 블로우(피커) 시간")]
            Boat2PlaceBlowTime = 468,

            [Description("보트3(언로딩) : 픽업 진공 형성(피커) 시간")]
            Boat3PickVacOnTime = 469,
            [Description("보트3(언로딩) : 픽업 블로우(보트) 시간")]
            Boat3PickBlowTime = 470,
            [Description("보트3(언로딩) : Top 스캔 피커 플레이스 다운(피커) 시간")]
            Boat3TopScanPickerPlaceDownTime = 471,
            [Description("보트3(언로딩) : Top 스캔 피커 플레이스 블로우(피커) 시간")]
            Boat3TopScanPickerPlaceBlowTime = 472,
            [Description("보트3(언로딩) : Btm 스캔 피커 플레이스 다운(피커) 시간")]
            Boat3BtmScanPickerPlaceDownTime = 473,
            [Description("보트3(언로딩) : Btm 스캔 피커 플레이스 블로우(피커) 시간")]
            Boat3BtmScanPickerPlaceBlowTime = 474,

            [Description("Top 스캔 보트 : 픽업 진공 형성(피커) 시간")]
            TopScanBoatPickVacOnTime = 475,
            [Description("Top 스캔 보트 : 픽업 블로우(보트) 시간")]
            TopScanBoatPickBlowTime = 476,
            [Description("Top 스캔 보트 : 플레이스 다운(피커) 시간")]
            TopScanBoatPlaceDownTime = 477,
            [Description("Top 스캔 보트 : 플레이스 블로우(피커) 시간")]
            TopScanBoatPlaceBlowTime = 478,

            [Description("Btm 스캔 보트 : 픽업 진공 형성(피커) 시간")]
            BtmScanBoatPickVacOnTime = 479,
            [Description("Btm 스캔 보트 : 픽업 블로우(보트) 시간")]
            BtmScanBoatPickBlowTime = 480,
            [Description("Btm 스캔 보트 : 플레이스 다운(피커) 시간")]
            BtmScanBoatPlaceDownTime = 481,
            [Description("Btm 스캔 보트 : 플레이스 블로우(피커) 시간")]
            BtmScanBoatPlaceBlowTime = 482,


            [Description("아웃 레일 : 플레이스 다운(피커) 시간")]
            OutRailPlaceDownTime = 483,
            [Description("아웃 레일 : 플레이스 블로우(피커) 시간")]
            OutRailPlaceBlowTime = 484,




            // 900 ~ 1000 Device Use/Skip
            [Description("상면 3D검사 사용")]
            Top3DScanUse = 900,
            [Description("상면 2D검사 사용")]
            Top2DScanUse = 901,


            [Description("피커헤드 사이드 실린더")]
            PickerSideCylinder = 910,
            [Description("보트의 사이드 진공")]
            BoatSideVacuum = 911,



            [Description("매거진 RFID 사용")]
            MgzRfid = 913,
            [Description("PCB 바코드 사용")]
            PcbBarcode = 914,

            Max = 1000,
        }

    }

    #endregion

}
