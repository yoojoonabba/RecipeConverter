using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace rcpChange.Old
{
    #region => Old device data
    [Serializable]
    [XmlRoot("Idx")]
    public class DeviceIdx
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Val")]
        public double Val { get; set; }
    }

    [Serializable]
    [XmlRoot("DeviceArray")]
    public class DeviceArray
    {
        [XmlElement("Index")]
        public DeviceIdx[] Idx { get; set; } = new DeviceIdx[(int)Def.Device.Max];

        public DeviceArray()
        {
            for (int i = 0; i < Idx.Length; i++)
            {
                this.Idx[i] = new DeviceIdx();
                this.Idx[i].Key = $"{i:000}";
                this.Idx[i].Val = 0;
            }
        }
    }
    #endregion


    #region => Old group data
    [Serializable]
    [XmlRoot("Idx")]
    public class GroupIdx
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }

        [XmlAttribute("Val")]
        public double Val { get; set; }
    }

    [Serializable]
    [XmlRoot("GroupArray")]
    public class GroupArray
    {
        [XmlElement("Index")]
        public GroupIdx[] Idx { get; set; } = new GroupIdx[(int)Def.Group.Max];

        public GroupArray()
        {
            for (int i = 0; i < Idx.Length; i++)
            {
                this.Idx[i] = new GroupIdx();
                this.Idx[i].Key = $"{i:000}";
                this.Idx[i].Val = 0;
            }
        }
    }
    #endregion


    #region => Enums
    public static class Def
    {
        /// <summary>
        /// 그룹 데이터, 자주 검사기 경우 자재 사이즈별 그룹 나눔
        /// Description에 화면에 표시할 이름을 작성
        /// </summary>
        public enum Group
        {
            [Description("매거진 슬롯 개수")]
            MgzSlotCnt = 0,
            [Description("매거진 폭(mm)")]
            MgzWidth = 1,


            [Description("자재 길이 (mm)")]
            PcbLength = 3,
            [Description("자재 폭 (mm)")]
            PcbWidth = 4,

            [Description("인-레일 : 픽업 진공 형성 시간")]
            InRailPickVacOnTime = 10,

            [Description("보트1(상면클린) : 픽업 진공 형성(피커) 시간")]
            Boat1PickVacOnTime = 11,
            [Description("보트1(상면클린) : 픽업 블로우(보트) 시간")]
            Boat1PickBlowTime = 12,
            [Description("보트1(상면클린) : 플레이스 다운(피커) 시간")]
            Boat1PlaceDownTime = 13,
            [Description("보트1(상면클린) : 플레이스 블로우(피커) 시간")]
            Boat1PlaceBlowTime = 14,

            [Description("보트2(하면클린) : 픽업 진공 형성(피커) 시간")]
            Boat2PickVacOnTime = 15,
            [Description("보트2(하면클린) : 픽업 블로우(보트) 시간")]
            Boat2PickBlowTime = 16,
            [Description("보트2(하면클린) : 플레이스 다운(피커) 시간")]
            Boat2PlaceDownTime = 17,
            [Description("보트2(하면클린) : 플레이스 블로우(피커) 시간")]
            Boat2PlaceBlowTime = 18,

            [Description("보트3(언로딩) : 픽업 진공 형성(피커) 시간")]
            Boat3PickVacOnTime = 19,
            [Description("보트3(언로딩) : 픽업 블로우(보트) 시간")]
            Boat3PickBlowTime = 20,
            [Description("보트3(언로딩) : Top 스캔 피커 플레이스 다운(피커) 시간")]
            Boat3TopScanPickerPlaceDownTime = 21,
            [Description("보트3(언로딩) : Top 스캔 피커 플레이스 블로우(피커) 시간")]
            Boat3TopScanPickerPlaceBlowTime = 22,
            [Description("보트3(언로딩) : Btm 스캔 피커 플레이스 다운(피커) 시간")]
            Boat3BtmScanPickerPlaceDownTime = 23,
            [Description("보트3(언로딩) : Btm 스캔 피커 플레이스 블로우(피커) 시간")]
            Boat3BtmScanPickerPlaceBlowTime = 24,

            [Description("Top 스캔 보트 : 픽업 진공 형성(피커) 시간")]
            TopScanBoatPickVacOnTime = 25,
            [Description("Top 스캔 보트 : 픽업 블로우(보트) 시간")]
            TopScanBoatPickBlowTime = 26,
            [Description("Top 스캔 보트 : 플레이스 다운(피커) 시간")]
            TopScanBoatPlaceDownTime = 27,
            [Description("Top 스캔 보트 : 플레이스 블로우(피커) 시간")]
            TopScanBoatPlaceBlowTime = 28,

            [Description("Btm 스캔 보트 : 픽업 진공 형성(피커) 시간")]
            BtmScanBoatPickVacOnTime = 29,
            [Description("Btm 스캔 보트 : 픽업 블로우(보트) 시간")]
            BtmScanBoatPickBlowTime = 30,
            [Description("Btm 스캔 보트 : 플레이스 다운(피커) 시간")]
            BtmScanBoatPlaceDownTime = 31,
            [Description("Btm 스캔 보트 : 플레이스 블로우(피커) 시간")]
            BtmScanBoatPlaceBlowTime = 32,


            [Description("아웃 레일 : 플레이스 다운(피커) 시간")]
            OutRailPlaceDownTime = 33,
            [Description("아웃 레일 : 플레이스 블로우(피커) 시간")]
            OutRailPlaceBlowTime = 34,




            Max = 200,
        }


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


            // 900 ~ 1000 Device Use/Skip
            [Description("상면 3D검사 사용")]
            Top3DScanUse = 900,
            [Description("상면 2D검사 사용")]
            Top2DScanUse = 901,
            [Description("하면 2D검사 사용")]
            Btm2DScanUse = 902,

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
