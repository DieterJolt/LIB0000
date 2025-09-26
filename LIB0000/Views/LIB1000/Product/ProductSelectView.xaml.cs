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
using Wpf.Ui;

namespace LIB0000
{
    public partial class ProductSelectView
    {

        #region Commands
        [RelayCommand]
        private void cmdLoadProduct() // Deze methode niet verwijderen, deze dient nog naar een andere plaats verplaatst te worden waar er een product geladen wordt.
        {
            if (BasicService.ProductsService.Product.Selected is ProductModel)
            {
                BasicService.ProductsService.Product.Loaded = BasicService.ProductsService.Product.Selected;
                BasicService.ProductDetailService.LoadedProduct = BasicService.ProductsService.Product.Loaded;
                NavigationService.GoBack();
            }
        }

        [RelayCommand]
        private void cmdOk()
        {
            BasicService.InstructionsService.InstructionLists.ObjectToWriteSelected.SetValue(BasicService.ProductsService.Product.Selected.Id);
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            NavigationService.GoBack();
        }
        #endregion

        #region Constructor

        public ProductSelectView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        // Filtering the list on base of the AutoSugestBoxText
        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.ProductsService.Product.FilterList(sender.Text);
        }

        // Loading of the list
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.ProductsService.Product.GetList();
        }

        // clearing of the AutoSugestBoxText
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        // Load selected row
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.ProductsService.Product.UpdateSelected();
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
