using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


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
            UpdateImage();
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public int ActualLayer = 0;



        #endregion

        #region Methods

        private void UpdateImage()
        {
            var window = WindowControl.HalconWindow;
            window.ClearWindow();

            if ((ActualLayer == 0) || (ActualLayer == 1))
            { window.DispObj(BasicService.HalconService[0].Grab.Image); }

            if ((ActualLayer == 0) || (ActualLayer == 2))
            {
                foreach (var word in BasicService.HalconService[0].ResultOCRWords)
                {
                    double row = word.Row;
                    double col = word.Column;

                    // 1️⃣ Bepaal exacte grootte van de tekst
                    //HTuple ascent, descent, width, height;
                    //HOperatorSet.GetStringExtents(window, word.Word, out ascent, out descent, out width, out height);

                    //// 2️⃣ Tekenen van gele achtergrondrechthoek
                    //HOperatorSet.SetColor(window, "yellow");

                    //// We maken de rechthoek iets ruimer (+ marge)
                    //double marginX = 2;
                    //double marginY = 2;

                    //HOperatorSet.DispRectangle1(
                    //    window,
                    //    row - ascent.D - marginY,        // bovenkant
                    //    col - marginX,                   // links
                    //    row + descent.D + marginY,       // onderkant
                    //    col + width.D + marginX           // rechts
                    //);

                    // 3️⃣ Tekst er bovenop
                    HOperatorSet.SetColor(window, "red");
                    HOperatorSet.SetTposition(window, row, col);
                    HOperatorSet.WriteString(window, word.Word);
                }
            }
        }

        #endregion

        #region Properties



        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void WindowControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActualLayer++;
            if (ActualLayer > 2) ActualLayer = 0;
            UpdateImage();
        }
    }
}
