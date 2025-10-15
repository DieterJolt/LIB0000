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

            BasicService.HalconService[0].OcrCompleted += EventOcrCompleted;

            InitializeComponent();
        }

        #endregion

        #region Events

        private void EventOcrCompleted(object sender, bool e)
        {
            //UpdateImage();
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public int ActualLayer = 0;



        #endregion

        #region Methods

        private void UpdateImage()
        {
            //var window = WindowControl.HalconWindow;
            //window.ClearWindow();

            ////if ((ActualLayer == 0) || (ActualLayer == 1)) DVH terug enablen
            ////{ window.DispObj(BasicService.HalconService[0].Grab.Image); }

            //if ((ActualLayer == 0) || (ActualLayer == 2))
            //{
            //    foreach (var word in BasicService.HalconService[0].ResultOCRWords)
            //    {
            //        //double row = word.Row; DVH terug enablen
            //        //double col = word.Column;

            //        //HOperatorSet.SetColor(window, "red");
            //        //HOperatorSet.SetTposition(window, row, col);
            //        //HOperatorSet.WriteString(window, word.Word);
            //    }
            //}
        }

        #endregion

        #region Properties



        #endregion


        private void WindowControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActualLayer++;
            if (ActualLayer > 2) ActualLayer = 0;
            UpdateImage();
        }
    }
}
