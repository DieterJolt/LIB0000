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
    public partial class HardwareEditView
    {

        #region Commands

        [RelayCommand]
        private void cmdok()
        {
            BasicService.HardwareService.Hardware.AddRow();
            NavigationService.Navigate(typeof(HardwareView));
        }

        [RelayCommand]
        private void cmdCancel()
        {
            NavigationService.Navigate(typeof(HardwareView));
        }

        [RelayCommand]
        private void cmdNavigateToHardwareType()
        {

            BasicService.HardwareService.ObjectToWriteSelectedProduct = new PropertyReferenceModel(BasicService.HardwareService.Hardware.Edit, nameof(BasicService.HardwareService.Hardware.Edit.HardwareType));

            NavigationService.Navigate(typeof(HardwareTypeSelectView));
        }

        #endregion

        #region Constructor

        public HardwareEditView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.HardwareService.Hardware.UpdateSelected();
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService NavigationService { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
