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
    public partial class HardwareTypeSelectView
    {

        #region Commands                

        [RelayCommand]
        public void CmdSelectHardwareOK()
        {
            if (BasicService.HardwareService.HardwareTypes.Selected != null)
            {
                BasicService.HardwareService.Hardware.Edit.HardwareTypeId = BasicService.HardwareService.HardwareTypes.Selected.Id;
                //BasicService.HardwareService.Hardware.Edit.HardwareTypeName = BasicService.HardwareService.HardwareTypes.Selected.Name;
                BasicService.HardwareService.ObjectToWriteSelectedProduct.SetValue(BasicService.HardwareService.HardwareTypes.Selected.HardwareType);
                BasicService.HardwareService.Hardware.GetListJoinHardwareType();
                NavigationService.Navigate(typeof(HardwareEditView));
            }
        }

        [RelayCommand]
        public void CmdSelectHardwareCancel()
        {
            BasicService.HardwareService.HardwareTypes.Selected = null;
            NavigationService.Navigate(typeof(HardwareEditView));
        }

        #endregion

        #region Constructor

        public HardwareTypeSelectView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.HardwareService.HardwareTypes.GetList();
        }

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.HardwareService.HardwareTypes.filterList(sender.Text);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
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
