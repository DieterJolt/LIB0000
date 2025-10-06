using LIB0000.Services;
using System.Reflection.PortableExecutable;
using System.Windows.Input;
using System.Xml.Serialization;

namespace LIB0000
{
    public partial class ProductDetail01
    {

        #region Commands


        [RelayCommand]
        private void cmdSave()
        {
            BasicService.ProductsService.Product.UpdateSelected();
            BasicService.ProductsService.Product.Edit.Structure = new XmlService().SerializeObjectToXml(BasicService.ProductsService.ProductStructure.Edit);
            BasicService.ProductsService.Product.UpdateRow();

            //Indien de productstructuur van het product dat geladen is wordt aangepast, wordt dit ook geupdated in de machine
            if (BasicService.ProductsService.Product.Loaded.Id == BasicService.ProductsService.Product.Selected.Id)
            {
                //GlobalService.ProductStructure = new XmlService().DeserializeXmlToObject<ProductStructureTyp>(BasicService.ProductsService.Product.Selected.Structure);
            }

            Footer.Button5TextColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            NavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public ProductDetail01(INavigationService navigationService, GlobalService globalService, BasicService basicService)
        {
            NavigationService = navigationService;
            GlobalService = globalService;
            BasicService = basicService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void ParameterUserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Footer.Button5TextColor = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
        }

        #endregion

        #region Fields

        public GlobalService GlobalService { get; set; }
        public BasicService BasicService { get; set; }
        public INavigationService NavigationService { get; set; }


        #endregion

        #region Methods

        #endregion

        #region Properties
        #endregion

    }
}
