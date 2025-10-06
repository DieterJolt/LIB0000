using System.Windows.Controls;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for HardwareView.xaml
    /// </summary>
    public partial class HardwareView : Page
    {
        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {
            BasicService.HardwareService.Hardware.Edit = new HardwareModel();
            BasicService.HardwareService.Hardware.Selected = new HardwareModel();
            BasicService.HardwareService.Hardware.SelectedJoin = new HardwareTypeJoinHardwareModel();
            NavigationService.Navigate(typeof(HardwareEditView));
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {

            BasicService.HardwareService.Hardware.DeleteRow();
            BasicService.HardwareService.Hardware.UpdateRow();

        }

        [RelayCommand]
        private void cmdEditRow()
        {
            if ((BasicService.HardwareService.Hardware.Selected != null) && (BasicService.HardwareService.Hardware.SelectedJoin != null) && (BasicService.HardwareService.Hardware.SelectedJoin.Id > 0))
            {
                NavigationService.Navigate(typeof(HardwareEditView));
            }
        }


        #endregion

        #region Constructor

        public HardwareView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();

        }

        #endregion

        #region Events

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.HardwareService.Hardware.FilterList(sender.Text);
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.HardwareService.Hardware.UpdateSelected();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.HardwareService.Hardware.GetList();
            BasicService.HardwareService.Hardware.GetListJoinHardwareType();

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
