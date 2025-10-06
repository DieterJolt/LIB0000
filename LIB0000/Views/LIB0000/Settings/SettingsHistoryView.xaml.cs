using System.Windows.Controls;

namespace LIB0000
{
    public partial class SettingsHistoryView
    {

        #region Commands
        #endregion

        #region Constructor

        public SettingsHistoryView(BasicService basicService, INavigationService navigationService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(SettingsView));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.SettingsService.GetSettingsHistory();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.SettingsService.GetSettingsHistory();
        }

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.SettingsService.GetSettingsHistory();
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; }

        public INavigationService NavigationService { get; set; }





        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
