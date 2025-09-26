

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for ProductGroupView.xaml
    /// </summary>
    public partial class ProductGroupView
    {
        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {
            BasicService.ProductGroupsService.ProductGroup.Edit = new ProductGroupModel();
            BasicService.ProductGroupsService.ProductGroup.Selected = new ProductGroupModel();

            BasicService.ProductGroupsService.ProductGroup.Edit.Image = ImageService.ConvertImageToByteArray("pack://application:,,,/Assets/Images/VoorbeeldAfbeelding.png");
            NavigationService.Navigate(typeof(ProductGroupsEditView));
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {

            BasicService.ProductGroupsService.ProductGroup.DeleteRow();
            BasicService.ProductGroupsService.ProductGroup.UpdateRow();

        }

        [RelayCommand]
        private void cmdEditRow()
        {
            if ((BasicService.ProductGroupsService.ProductGroup.Selected != null) && (BasicService.ProductGroupsService.ProductGroup.Selected.Id > 0))
            {
                NavigationService.Navigate(typeof(ProductGroupsEditView));
            }
        }


        #endregion

        #region Constructor

        public ProductGroupView(BasicService basicService, INavigationService navigationService, ImageService imageService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            ImageService = imageService;
            DataContext = this;
            InitializeComponent();

        }

        #endregion

        #region Events

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.ProductGroupsService.ProductGroup.FilterList(sender.Text);
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.ProductGroupsService.ProductGroup.UpdateSelected();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.ProductGroupsService.ProductGroup.GetList();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService NavigationService { get; set; }
        public ImageService ImageService { get; set; }



        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
