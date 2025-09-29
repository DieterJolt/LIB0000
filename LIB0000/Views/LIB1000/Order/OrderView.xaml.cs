using System.Reflection.PortableExecutable;

namespace LIB0000
{
    public partial class OrderView
    {

        #region Commands
        [RelayCommand]
        public async void cmdSaveOrder()
        {
            if (BasicService.OrdersService.Order.Edit.ProductId > 0 || BasicService.OrdersService.Order.Edit.ProductGroupId > 0)
            {
                BasicService.OrdersService.Order.Edit.WorkstationId = GlobalService.Machine.Par.Workstation.Id;
                BasicService.ProductsService.Product.LoadProduct(BasicService.OrdersService.Order.Edit.ProductId);
                BasicService.ProductGroupsService.ProductGroup.LoadProductGroup(BasicService.OrdersService.Order.Edit.ProductGroupId);
                BasicService.ProductDetailService.LoadProductDetails(BasicService.ProductsService.Product.Loaded.Id);
                BasicService.OrdersService.Order.Load(BasicService.UsersService.Login.ActualUser.Id);

                foreach (HardwareModel hardware in BasicService.HardwareService.Hardware.List)
                {
                    switch (hardware.HardwareType)
                    {
                        case HardwareType.FHV7:
                            await BasicService.FhService.Where(f => f.HardwareId == hardware.Id).FirstOrDefault()?.UpdateFunctionSettings(BasicService.ProductDetailService.ListProductDetails.ToList(), hardware.LoadedFunction);
                            break;
                        case HardwareType.GigeCam:
                            break;
                    }
                }
                var list = BasicService.ProductDetailService.ListProductDetails;

                GlobalService.Machine.Cmd.StartOrder = true;

                NavigationService.Navigate(typeof(OrderActualView));
            }
            else
            {
                CustomMessageBox.Show("Geen productgroep of product gekozen", "Warning", MessageBoxImage.Warning);
            }
        }

        [RelayCommand]
        public void cmdCloseOrder()
        {
            GlobalService.Machine.Cmd.CloseOrder = true;
        }

        [RelayCommand]
        public void cmdSelectProductGroup()
        {
            BasicService.OrdersService.ObjectToWriteSelected = new PropertyReferenceModel(BasicService.OrdersService.Order.Edit, nameof(BasicService.OrdersService.Order.Edit.ProductGroupId));            
            
            NavigationService.Navigate(typeof(ProductGroupSelectView));
        }

        [RelayCommand]
        public void cmdSelectProduct()
        {
            BasicService.OrdersService.ObjectToWriteSelected = new PropertyReferenceModel(BasicService.OrdersService.Order.Edit, nameof(BasicService.OrdersService.Order.Edit.ProductId));

            NavigationService.Navigate(typeof(ProductSelectView));
        }

        [RelayCommand]
        public void cmdStartStopMachine()
        {
            GlobalService.Machine.Stat.Started = !GlobalService.Machine.Stat.Started;
        }

        #endregion

        #region Constructor

        public OrderView(BasicService basicService, INavigationService navigationService, GlobalService globalService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            GlobalService = globalService;
            DataContext = this;
            InitializeComponent();

        }

        #endregion

        #region Events

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }


        #endregion

        #region Fields

        public BasicService BasicService { get; set; }
        public GlobalService GlobalService { get; set; }
        public INavigationService NavigationService { get; set; }




        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
