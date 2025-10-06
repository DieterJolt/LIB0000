
using System.Windows.Controls;
using System.Windows.Input;

namespace LIB0000
{
    public sealed partial class SettingsView
    {
        #region Commands              
        [RelayCommand]
        private async void SaveChangedSettings()
        {
            BasicService.SettingsService.SaveChangedSettings();

            HardwareType hardwareType = BasicService.SettingsService.GetHardwareType(BasicService.SettingsService.SelectedHardwareFunctionBasicSettingFilter.HardwareId);
            List<SettingModel> listSettings = BasicService.SettingsService.ListSettings.Where(s => s.HardwareId == BasicService.SettingsService.SelectedHardwareFunctionBasicSettingFilter.HardwareId).ToList();

            switch (hardwareType)
            {
                case HardwareType.None:
                    break;
                case HardwareType.FHV7:
                    await BasicService.FhService.Where(f => f.HardwareId == BasicService.SettingsService.SelectedHardwareFunctionBasicSettingFilter.HardwareId).FirstOrDefault()?.UpdateBasicSettings(listSettings);
                    break;
                case HardwareType.GigeCam:
                    break;
                default:
                    break;
            }
        }

        #endregion
        #region Constructor
        public SettingsView(BasicService basicService, INavigationService navigationService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            DataContext = this;
            this.InitializeComponent();
            ButtonCommand = new RelayCommand<SettingHardwareFunctionModel>(changeSettingsGroup);

        }

        #endregion
        #region Events

        private void TextBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.SettingsService.SetFilter();
            BasicService.SettingsService.SetFilterHistory();
        }

        private void EventSettingChangedTextBox(object sender, TextChangedEventArgs e)
        {
            BasicService.SettingsService.UpdateUISettings();
        }

        private void EventSettingChangedSlider(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BasicService.SettingsService.UpdateUISettings();
        }

        private void EventSettingChangedToggleSwitch(object sender, RoutedEventArgs e)
        {
            BasicService.SettingsService.UpdateUISettings();
        }

        private void EventSettingClosedComboBox(object sender, EventArgs e)
        {
            BasicService.SettingsService.UpdateUISettings();
        }

        private void EventClickOpenFile(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
                {
                    openFileDialog.Title = "Kies bestand";
                    openFileDialog.Filter = "Alle bestanden (*.*)|*.*";

                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        b.Tag = openFileDialog.FileName;
                    }
                }
            }
        }

        private void EventClickOpenFolder(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                using (System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    openFileDialog.Description = "Kies map";

                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        b.Tag = openFileDialog.SelectedPath;
                    }
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.SettingsService.SetFilter();
            BasicService.SettingsService.UpdateUISettings();
        }


        #endregion
        #region Fields
        public BasicService BasicService { get; }
        public INavigationService NavigationService { get; set; }
        public ICommand ButtonCommand { get; set; }

        #endregion

        #region Methods

        private void changeSettingsGroup(SettingHardwareFunctionModel selectedHardwareFunction)
        {
            BasicService.SettingsService.SelectedHardwareFunctionBasicSettingFilter = selectedHardwareFunction;
            BasicService.SettingsService.SetFilter();

            HardwareType hardwareType = BasicService.SettingsService.GetHardwareType(selectedHardwareFunction.HardwareId);

            switch (hardwareType)
            {
                case HardwareType.FHV7:
                    FhBasicSettingsUsercontrol fhBasicSettingsUserControl = new FhBasicSettingsUsercontrol();
                    fhBasicSettingsUserControl.SelectedFhService = BasicService.FhService.Where(f => f.HardwareId == selectedHardwareFunction.HardwareId).FirstOrDefault();
                    TopUserControl.Content = fhBasicSettingsUserControl;
                    break;
                case HardwareType.GigeCam:
                    HalconSettingsUsercontrol halconSettingsUsercontrol = new HalconSettingsUsercontrol();
                    halconSettingsUsercontrol.SelectedHalconService = BasicService.HalconService.Where(h => h.HardwareId == selectedHardwareFunction.HardwareId).FirstOrDefault();
                    TopUserControl.Content = halconSettingsUsercontrol;
                    break;

                default:
                    TopUserControl.Content = null;
                    break;
            }


            BasicService.SettingsService.UpdateUISettings();
        }


        #endregion

        #region Properties
        #endregion

    }
}
