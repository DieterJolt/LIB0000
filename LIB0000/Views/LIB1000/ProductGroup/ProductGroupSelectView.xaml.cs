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
    public partial class ProductGroupSelectView
    {

        #region Commands

        [RelayCommand]
        private void cmdOk()
        {
            BasicService.InstructionsService.InstructionLists.ObjectToWriteSelected.SetValue(BasicService.ProductGroupsService.ProductGroup.Selected.Id);
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            NavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public ProductGroupSelectView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events


        // Loading of the list
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.ProductGroupsService.ProductGroup.GetList();
        }

        // clearing of the AutoSugestBoxText
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        // Filtering the list on base of the AutoSugestBoxText
        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.ProductGroupsService.ProductGroup.FilterList(sender.Text);
        }

        // Load selected row
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasicService.ProductGroupsService.ProductGroup.UpdateSelected();
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
