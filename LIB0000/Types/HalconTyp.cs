using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    class HalconTyp
    {
    }

    public class A001GenericShapeParTyp
    {
        public string Name { get; set; }
        public double Row { get; set; }
        public double Column { get; set; }
        public double Angle { get; set; }
        public double Score { get; set; }
    }
    public class A001GenericShapeStatTyp
    {
        public HObject resultImage;
        public HObject resultMatchContour;

        public Single Score { get; set; }
        public Single Angle { get; set; }

    }

    public class A002DeepOcrParTyp
    {
        public string Word { get; set; }
        public double Row { get; set; }
        public double Column { get; set; }
    }
    public class A002DeepOcrStatTyp
    {
        public string Word { get; set; }
        public double Score { get; set; }

    }
}
