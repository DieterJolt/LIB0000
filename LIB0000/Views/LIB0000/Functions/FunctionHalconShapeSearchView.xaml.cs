using HalconDotNet;
using System.Windows.Controls;
using System.Windows.Input;


namespace LIB0000
{
    /// <summary>
    /// Interaction logic for FunctionHalconShapeSearchView.xaml
    /// </summary>
    public partial class FunctionHalconShapeSearchView : Page
    {

        #region Commands

        #endregion

        #region Constructor

        public FunctionHalconShapeSearchView(BasicService basicService)
        {
            BasicService = basicService;
            DataContext = this;

            //BasicService.HalconService[0].A002DeepOcrEventHandler += A002DeepOcrCompletedEvent;
            BasicService.HalconService[0].A003CardCountingEventHandler += A003CardCountingCompletedEvent;

            InitializeComponent();
        }

        #endregion

        #region Events

        private void A002DeepOcrCompletedEvent(object sender, bool e)
        {
            updateImage();
        }

        private void A003CardCountingCompletedEvent(object sender, bool e)
        {
            updateImage();
        }
        private void WindowControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ActualLayer++;
            //if (ActualLayer > 2) ActualLayer = 0;
            //updateImage();
        }

        private void HSmartWindow01_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            updateImage();
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public int ActualLayer = 0;



        #endregion

        #region Methods

        private void updateImage()
        {
            var window = HSmartWindow01.HalconWindow;
            window.ClearWindow();
            window.DispObj(BasicService.HalconService[0].GrabImage);


            //if ((ActualLayer == 0) || (ActualLayer == 2))
            //{
            //    for (int i = 0; i < BasicService.HalconService[0].A002DeepOcrStat.ResultsWords.Count(); i++)
            //    {
            //        double row = BasicService.HalconService[0].A002DeepOcrStat.ResultsRow[i];
            //        double col = BasicService.HalconService[0].A002DeepOcrStat.ResultsColumn[i];
            //        string word = BasicService.HalconService[0].A002DeepOcrStat.ResultsWords[i];

            //        HOperatorSet.SetColor(window, "red");
            //        HOperatorSet.SetTposition(window, row, col);
            //        HOperatorSet.WriteString(window, word);
            //        //HSmartWindow01.HZoomFactor = 1;
            //    }
            //}
        }

        #endregion

        #region Properties



        #endregion





    }
}
