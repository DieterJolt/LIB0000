namespace LIB0000
{
    public partial class OrderSelectView
    {

        #region Commands

        [RelayCommand]
        private void cmdBack()
        {
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdDetail()
        {
            //if (BasicService.InstructionsService.InstructionLists.SelectedVersion != null)
            //{
            //    BasicService.InstructionsService.Instructions.GetJoinList(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id);
            NavigationService.Navigate(typeof(OrderHistoryView));
            //}
        }


        #endregion

        #region Constructor        
        public OrderSelectView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            InitializeComponent();
            BasicService.OrdersService.Order.GetList();
        }

        #endregion

        #region Events

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.OrdersService.Order.GetList();
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            //BasicService.OrdersService.Order.Filter();
        }

        //private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        //{
        //    BasicService.OrdersService.Order.GetListJoinModel();

        //    var suggestions = BasicService.OrdersService.Order.JoinList;
        //    var filteredSuggestions = suggestions.Where(p =>
        //    p.OrderNr.ToString().Contains(sender.Text) ||
        //    p.RecipeName.ToString().Contains(sender.Text) ||
        //    p.Amount.ToString().Contains(sender.Text) ||
        //    p.UserId.ToString().Contains(sender.Text)
        //    ).ToList();
        //    BasicService.OrdersService.Order.JoinList = filteredSuggestions;

        //    sender.ItemsSource = BasicService.OrdersService.Order.JoinList;

        //}

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
