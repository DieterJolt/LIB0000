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
    /// Interaction logic for OrderActualCameraView.xaml
    /// </summary>
    public partial class OrderActualCameraView : Page
    {

        #region Commands
        #endregion

        #region Constructor        
        public OrderActualCameraView(BasicService basicService, INavigationService navigationService, GlobalService globalService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            GlobalService = globalService;
            DataContext = this;
            InitializeComponent();
        }
        #endregion

        #region Events
        #endregion

        #region Fields
        public BasicService BasicService { get; set; }
        public GlobalService GlobalService { get; set; }
        public INavigationService NavigationService { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion

    }
}
