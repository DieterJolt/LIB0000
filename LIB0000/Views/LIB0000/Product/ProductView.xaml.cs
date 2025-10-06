

using LIB0000.Services;
using LIB0000;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView
    {
        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {
            BasicService.ProductsService.Product.Edit = new ProductModel();
            BasicService.ProductsService.Product.Selected = new ProductModel();
            NavigationService.Navigate(typeof(ProductEditView));
            BasicService.ProductsService.Product.Edit.Image = ImageService.ConvertImageToByteArray("pack://application:,,,/Assets/Images/VoorbeeldAfbeelding.png");
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {
            BasicService.ProductsService.Product.DeleteRow();
            BasicService.ProductsService.Product.UpdateRow();
        }

        [RelayCommand]
        private void cmdEditRow()
        {
            if ((BasicService.ProductsService.Product.Selected != null) && (BasicService.ProductsService.Product.Selected.Id > 0))
            {
                NavigationService.Navigate(typeof(ProductEditView));
            }
        }

        [RelayCommand]
        private void cmdProductDetail()
        {
            if ((BasicService.ProductsService.Product.Selected != null) && (BasicService.ProductsService.Product.Selected.Id > 0))
            {
                BasicService.ProductsService.ProductStructure.Edit = new XmlService().DeserializeXmlToObject<ProductStructureTyp>(BasicService.ProductsService.Product.Selected.Structure);
                NavigationService.Navigate(typeof(ProductDetail01));
            }
        }


        #endregion

        #region Constructor

        public ProductView(BasicService basicService, INavigationService navigationService, ImageService imageService)
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
            BasicService.ProductsService.Product.FilterList(sender.Text);
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.ProductsService.Product.UpdateSelected();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.ProductsService.Product.GetList();
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
