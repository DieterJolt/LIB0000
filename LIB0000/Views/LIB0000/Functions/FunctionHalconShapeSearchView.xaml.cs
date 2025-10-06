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
            InitializeComponent();
        }

        #endregion

        #region Events
        #endregion

        #region Fields

        public BasicService BasicService { get; set; }





        #endregion

        #region Methods
        #endregion

        #region Properties



        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BasicService.ChangeHalconServiceSelected(0);
        }

        private void Button02_Click(object sender, RoutedEventArgs e)
        {
            BasicService.ChangeHalconServiceSelected(1);
        }
    }
}
