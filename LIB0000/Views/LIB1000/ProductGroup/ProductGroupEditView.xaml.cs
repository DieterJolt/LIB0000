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
    public partial class ProductGroupsEditView
    {

        #region Commands


        [RelayCommand]
        private void cmdOk()
        {
            BasicService.ProductGroupsService.ProductGroup.AddRow();
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            BasicService.ProductGroupsService.ProductGroup.Edit.Name = "";
            BasicService.ProductGroupsService.ProductGroup.Edit.Description = "";
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdSelectBefore()
        {
            BasicService.InstructionsService.InstructionLists.ObjectToWriteSelected = new PropertyReferenceModel(BasicService.ProductGroupsService.ProductGroup.Edit, nameof(BasicService.ProductGroupsService.ProductGroup.Edit.InstructionListIdBefore));
            NavigationService.Navigate(typeof(InstructionListSelectView));
        }

        [RelayCommand]
        private void cmdSelectPeriodic()
        {
            BasicService.InstructionsService.InstructionLists.ObjectToWriteSelected = new PropertyReferenceModel(BasicService.ProductGroupsService.ProductGroup.Edit, nameof(BasicService.ProductGroupsService.ProductGroup.Edit.InstructionListPeriodicIdBefore));
            NavigationService.Navigate(typeof(InstructionListSelectView));
        }

        [RelayCommand]
        private void cmdSelectAfter()
        {
            BasicService.InstructionsService.InstructionLists.ObjectToWriteSelected = new PropertyReferenceModel(BasicService.ProductGroupsService.ProductGroup.Edit, nameof(BasicService.ProductGroupsService.ProductGroup.Edit.InstructionListIdAfter));
            NavigationService.Navigate(typeof(InstructionListSelectView));
        }

        //LL170225
        //[RelayCommand]
        //private void cmdAddRow()
        //{
        //    BasicService.ProductGroupsService.ProductGroup.AddRow();
        //}

        //[RelayCommand]
        //private void cmdDeleteRow()
        //{
        //    BasicService.ProductGroupsService.ProductGroup.DeleteRow();
        //    BasicService.ProductGroupsService.ProductGroup.UpdateRow();
        //}

        //[RelayCommand]
        //private void cmdUpdateRow()
        //{
        //    BasicService.ProductGroupsService.ProductGroup.UpdateRow();
        //}

        #endregion

        #region Constructor

        public ProductGroupsEditView(BasicService basicService, INavigationService navigationService, ImageService imageService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            ImageService = imageService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {


        }


        // Change the image of a row
        private void Change_Image(object sender, MouseButtonEventArgs e)
        {
            BasicService.ProductGroupsService.ProductGroup.AddImage();
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
