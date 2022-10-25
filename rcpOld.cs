using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace rcpChange.Old
{
    [Serializable]
    [XmlRoot("Pos")]
    public class IndexPos
    {
        [XmlAttribute("Value")]
        public double Val { get; set; }

        [XmlAttribute("Min")]
        public double Min { get; set; }

        [XmlAttribute("Max")]
        public double Max { get; set; }

        public IndexPos()
        {
            Val = 0;
            Min = -99999;
            Max = 99999;
        }
    }

    [Serializable]
    [XmlRoot("Speed")]
    public class IndexSpeed
    {
        [XmlAttribute("Value")]
        public double Val { get; set; }

        [XmlAttribute("Min")]
        public double Min { get; set; }

        [XmlAttribute("Max")]
        public double Max { get; set; }

        public IndexSpeed()
        {
            Val = 0;
            Min = -99999;
            Max = 99999;
        }
    }

    [Serializable]
    [XmlRoot("Acc")]
    public class IndexAcc
    {
        [XmlAttribute("Accel")]
        public double Val { get; set; }

        [XmlAttribute("MAT")]
        public double MAT { get; set; }

        public IndexAcc()
        {
            Val = 100; // unit/sec^2
            MAT = 30;  // msec
        }
    }

    [Serializable]
    [XmlRoot("Index")]
    public class PosIndex
    {
        [XmlAttribute("Number")]
        public int Number { get; set; }

        [XmlAttribute("Common")]
        public bool IsCommon { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Pos")]
        public IndexPos Pos { get; set; } = new IndexPos();

        [XmlElement("Speed")]
        public IndexSpeed Speed { get; set; } = new IndexSpeed();

        [XmlElement("Acc")]
        public IndexAcc Acc { get; set; } = new IndexAcc();

    }

    #region => Position index array & Position table
    /// <summary>
    /// 모터 인덱스(Pos,Vel,Acc..) 배열(50개)
    /// </summary>
    [Serializable]
    [XmlRoot("MotorData")]
    public class MotorPosIndexArray
    {
        [XmlElement("Index")]
        public PosIndex[] Idx { get; set; } = new PosIndex[50];

        public MotorPosIndexArray()
        {
            for (int i = 0; i < Idx.Length; i++)
            {
                this.Idx[i] = new PosIndex();
                this.Idx[i].Number = i;
            }
        }
    }



    /// <summary>
    /// 모터 인덱스 배열을 Device별 데이터와, 공통 데이터 2가지를 가지고 있음.
    /// Device, Common이 모여서 1축의 모터 포지션 레시피가 됨
    /// </summary>
    public class MotorPosTable : IDisposable
    {
        private bool disposed = false;

        public MotorPosTable()
        {

        }
        ~MotorPosTable()
        {
            if (this.disposed)
                return;
            Dispose(false);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
            {
                ///disposing managed resource
            }
            ///disposing un-managed resource     
            this.disposed = true;
        }

        public MotorPosIndexArray Device { get; set; } = new MotorPosIndexArray();
        public MotorPosIndexArray Common { get; set; } = new MotorPosIndexArray();


        public PosIndex this[int idx]
        {
            get => this.Device.Idx[idx];
            set => this.Device.Idx[idx] = value;
        }

        public void SetAccAll(double val)
        {
            for (int i = 0; i < Device.Idx.Length; i++)
            {
                this.Device.Idx[i].Acc.Val = val;
                this.Common.Idx[i].Acc.Val = val;
            }
        }

        public void SetMatAll(double val)
        {
            for (int i = 0; i < Device.Idx.Length; i++)
            {
                this.Device.Idx[i].Acc.MAT = val;
                this.Common.Idx[i].Acc.MAT = val;
            }
        }
    }
    #endregion









}
