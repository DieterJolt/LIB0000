using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class A002DeepOcrParTyp : ObservableObject
    {
        [ObservableProperty]
        public string _dummy;

    }
    public partial class A002DeepOcrStatTyp : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<string> _resultsWords = new ObservableCollection<string>(Enumerable.Repeat(string.Empty, 500));
        [ObservableProperty]
        private ObservableCollection<double> _resultsRow = new ObservableCollection<double>(Enumerable.Repeat(0.0, 500));
        [ObservableProperty]
        private ObservableCollection<double> _resultsColumn = new ObservableCollection<double>(Enumerable.Repeat(0.0, 500));
    }
}
