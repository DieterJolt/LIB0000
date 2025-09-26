using System.Windows.Controls;
using System.Windows.Input;

namespace LIB0000
{
    public sealed partial class ProductDetailView
    {
        #region Commands        

        [RelayCommand]
        private async void SaveChangedSettings()
        {
            BasicService.ProductDetailService.SaveChangedSettings();

            HardwareType hardwareType = BasicService.ProductDetailService.GetHardwareType(BasicService.ProductDetailService.SelectedHardware.Id);
            List<ProductDetailJoinModel> listSettings = BasicService.ProductDetailService.ListProductDetails.Where(s => s.HardwareId == BasicService.ProductDetailService.SelectedHardware.Id).ToList();

            switch (hardwareType)
            {
                case HardwareType.None:
                    break;
                case HardwareType.FHV7:
                    await BasicService.FhService.Where(f => f.HardwareId == BasicService.ProductDetailService.SelectedHardware.Id).FirstOrDefault()?.UpdateFunctionSettings(listSettings, BasicService.ProductDetailService.SelectedHardwareFunction);
                    break;
                case HardwareType.GigeCam:
                    break;
                default:
                    break;
            }
        }

        #endregion
        #region Constructor
        public ProductDetailView(BasicService basicService, INavigationService navigationService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            ChangeHardwareCommand = new RelayCommand<HardwareModel>(changeHardware);
            ChangeHardwareFunctionCommand = new RelayCommand(changeHardwareFunction);
            DataContext = this;
            this.InitializeComponent();

        }

        #endregion
        #region Events

        private void TextBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.ProductDetailService.SetFilterProduct();
        }

        private void EventSettingChangedTextBox(object sender, TextChangedEventArgs e)
        {
            BasicService.ProductDetailService.UpdateUISettings();
        }

        private void EventSettingChangedToggleSwitch(object sender, RoutedEventArgs e)
        {
            BasicService.ProductDetailService.UpdateUISettings();
        }

        private void EventSettingClosedComboBox(object sender, EventArgs e)
        {
            BasicService.ProductDetailService.UpdateUISettings();
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
            BasicService.ProductDetailService.SetFilterProduct();
            BasicService.ProductDetailService.UpdateUISettings();
        }


        #endregion
        #region Fields
        public BasicService BasicService { get; }
        public INavigationService NavigationService { get; set; }
        public ICommand ChangeHardwareCommand { get; set; }
        public ICommand ChangeHardwareFunctionCommand { get; set; }

        #endregion

        #region Methods

        private void changeHardware(HardwareModel selectedHardware)
        {
            BasicService.ProductDetailService.ChangeSelectedHardware(selectedHardware);
        }

        private async void changeHardwareFunction()
        {
            BasicService.ProductDetailService.ChangeHardwareFunction();
            HardwareType hardwareType = BasicService.ProductDetailService.GetHardwareType(BasicService.ProductDetailService.SelectedHardware.Id);
            List<ProductDetailJoinModel> listSettings = BasicService.ProductDetailService.ListProductDetails.Where(s => s.HardwareId == BasicService.ProductDetailService.SelectedHardware.Id).ToList();

            switch (hardwareType)
            {
                case HardwareType.None:
                    break;
                case HardwareType.FHV7:
                    await BasicService.FhService.Where(f => f.HardwareId == BasicService.ProductDetailService.SelectedHardware.Id).FirstOrDefault()?.UpdateFunctionSettings(listSettings, BasicService.ProductDetailService.SelectedHardwareFunction);
                    break;
                case HardwareType.GigeCam:
                    break;
                default:
                    break;
            }

            // Lijst moet geupdated worden na veranderen van hardwarefunction (dient voor update van de view usercontrols zodat deze kunnen wijzigen afhankelijk van de gekozen functie)            
            BasicService.HardwareService.Hardware.GetList();
        }

        #endregion

        #region Properties
        #endregion


    }
}
