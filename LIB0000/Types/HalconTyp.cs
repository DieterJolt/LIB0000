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

    public partial class A003CardCountingParTyp : ObservableObject
    {
        public HObject teachImage; // teach image

        [ObservableProperty]
        public int _numbersOfCardsToTeach;

        [ObservableProperty]
        public int _roiWidth = 150; // roi width for each line

        [ObservableProperty]
        public int[] _lineRowsStart = new int[5] { 1200, 1400, 1600, 1800, 2000 }; // start row for each line

        [ObservableProperty]
        public int[] _lineRowsEnd = new int[5] { 1200, 1400, 1600, 1800, 2000 }; // end row for each line

        [ObservableProperty]
        public int _lineColumnsStart = 60; // start column for all lines

        [ObservableProperty]
        public int _lineColumnsEnd = 2300; // end column for all lines

        [ObservableProperty]
        private double _sigma;

        [ObservableProperty]
        private double _amplitudeThreshold;        

    }

    public partial class A003CardCountingStatTyp : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<A003CardCountingTeachResultTyp> _teachResult = new();
        
        [ObservableProperty]
        private ObservableCollection<int> _cardsCounted = new();
        [ObservableProperty]
        private int _cardsCountedResult;
    }

    public partial class A003CardCountingTeachResultTyp : ObservableObject
    {
        [ObservableProperty]
        private double _sigma;

        [ObservableProperty]
        private double _amplitudeThreshold;

        [ObservableProperty]
        private int _correctLines;
    }
}
