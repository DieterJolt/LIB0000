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
    public partial class ProductEditView
    {

        #region Commands

        [RelayCommand]
        private void cmdOk()
        {
            BasicService.ProductsService.Product.AddRow();
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            BasicService.ProductsService.Product.Edit.Name = "";
            BasicService.ProductsService.Product.Edit.Description = "";
            NavigationService.GoBack();
        }
             

        #endregion

        #region Constructor

        public ProductEditView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        // Load selected row
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.ProductsService.Product.UpdateSelected();
        }

        // Loading of the list
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.ProductsService.Product.GetList();
        }

        // Change the image of a row
        private void Change_Image(object sender, MouseButtonEventArgs e)
        {
            BasicService.ProductsService.Product.AddImage();
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
