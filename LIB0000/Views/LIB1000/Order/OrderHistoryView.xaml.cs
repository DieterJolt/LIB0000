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
    /// Interaction logic for OrderActualHistoryView.xaml
    /// </summary>
    public partial class OrderHistoryView : Page
    {

        #region Commands


        [RelayCommand]
        private void cmdBack()
        {
            NavigationService.GoBack();
        }
        #endregion

        #region Constructor
        public OrderHistoryView(BasicService basicService, INavigationService navigationService, GlobalService globalService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            GlobalService = globalService;
            BasicService.OrdersService.Order.FilterStoppedByCamera = true;
            BasicService.OrdersService.Order.FilterInstruction = true;
            BasicService.OrdersService.Order.FilterTimeouts = true;
            BasicService.OrdersService.Order.FilterInstruction = true;

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
