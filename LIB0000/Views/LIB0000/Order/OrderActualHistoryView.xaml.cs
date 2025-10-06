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
    public partial class OrderActualHistoryView : Page
    {

        #region Commands

        #endregion

        #region Constructor
        public OrderActualHistoryView(BasicService basicService, INavigationService navigationService, GlobalService globalService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            GlobalService = globalService;
            BasicService.OrdersService.Order.FilterStoppedByCamera = true;
            BasicService.OrdersService.Order.FilterTimeouts = true;

            DataContext = this;
            InitializeComponent();
        }
        #endregion

        #region Events

        private void FilterStoppedByCamera(object sender, RoutedEventArgs e)
        {
            if (BasicService.OrdersService.Order.FilterStoppedByCamera)
            {
                BasicService.OrdersService.Order.FilterStoppedByCamera = false;
            }
            else
            {
                BasicService.OrdersService.Order.FilterStoppedByCamera = true;
            }

        }

        private void FilterTimeOut(object sender, RoutedEventArgs e)
        {
            if (BasicService.OrdersService.Order.FilterTimeouts)
            {
                BasicService.OrdersService.Order.FilterTimeouts = false;
            }
            else
            {
                BasicService.OrdersService.Order.FilterTimeouts = true;
            }
        }

        private void FilterStartStop(object sender, RoutedEventArgs e)
        {
            if (BasicService.OrdersService.Order.FilterStartStop)
            {
                BasicService.OrdersService.Order.FilterStartStop = false;
            }
            else
            {
                BasicService.OrdersService.Order.FilterStartStop = true;
            }
        }
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
