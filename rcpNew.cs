using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace rcpChange.New
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
    public class MtRecipeIndex
    {
        [XmlAttribute("Number")]
        public int Number { get; set; }

        /// <summary>
        /// 0(Device), 1(Group), 2(Global)
        /// </summary>
        [XmlAttribute("Mode")]
        public int Mode { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Pos")]
        public IndexPos Pos { get; set; } = new IndexPos();

        [XmlElement("Speed")]
        public IndexSpeed Speed { get; set; } = new IndexSpeed();

        [XmlElement("Acc")]
        public IndexAcc Acc { get; set; } = new IndexAcc();

    }

    /// <summary>
    /// 모터 인덱스(Pos,Vel,Acc..) 배열(50개)
    /// </summary>
    [Serializable]
    [XmlRoot("Indexes")]
    public class MtRecipeIndexes
    {
        [XmlElement("Index")]
        public MtRecipeIndex[] Idx { get; set; } = new MtRecipeIndex[50];

        public MtRecipeIndexes()
        {
            for (int i = 0; i < Idx.Length; i++)
            {
                this.Idx[i] = new MtRecipeIndex();
                this.Idx[i].Number = i;
                this.Idx[i].Mode = 0;
            }
        }

        public void Set(int idx, MtRecipeIndex src)
        {
            this.Idx[idx].Number = src.Number;
            this.Idx[idx].Mode = src.Mode;
            this.Idx[idx].Name = src.Name;

            this.Idx[idx].Pos.Val = src.Pos.Val;
            this.Idx[idx].Pos.Min = src.Pos.Min;
            this.Idx[idx].Pos.Max = src.Pos.Max;

            this.Idx[idx].Speed.Val = src.Speed.Val;
            this.Idx[idx].Speed.Min = src.Speed.Min;
            this.Idx[idx].Speed.Max = src.Speed.Max;

            this.Idx[idx].Acc.Val = src.Acc.Val;
            this.Idx[idx].Acc.MAT = src.Acc.MAT;
        }
    }
}
