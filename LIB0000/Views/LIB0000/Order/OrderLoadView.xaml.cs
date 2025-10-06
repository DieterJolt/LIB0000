using System.Windows.Controls;


namespace LIB0000
{
    public partial class OrderLoadView
    {

        #region Commands

        [RelayCommand]
        private void cmdNavigateToPorductListView()
        {
            //NavigationService.Navigate(typeof(RecipeLoadView));
        }

        [RelayCommand]
        private void cmAutogenerateOrderNumber()
        {
            BasicService.OrdersService.Order.Edit.OrderNr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [RelayCommand]
        private void cmdOrderLoad()
        {

            bool result = BasicService.OrdersService.Order.Load(BasicService.UsersService.Login.ActualUser.Id);
            if (result == true)
            {
                NavigationService.Navigate(typeof(OrderActualView));
            }
        }

        #endregion

        #region Constructor

        public OrderLoadView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;

            InitializeComponent();
        }

        #endregion

        #region Events

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Page page = sender as Page;
            if (page.IsVisible)
            {
                if (BasicService.OrdersService.Order.IsOrderBusy == true)
                { NavigationService.Navigate(typeof(OrderActualView)); }
            }
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
