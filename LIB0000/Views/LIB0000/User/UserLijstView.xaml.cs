namespace LIB0000
{
    public sealed partial class UserLijstView
    {
        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {
            BasicService.UsersService.User.Edit = new UserModel();
            BasicService.UsersService.User.Selected = new UserModel();

            NavigationService.Navigate(typeof(UserEditView));
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {
            if ((BasicService.UsersService.User.Selected != null) && (BasicService.UsersService.User.Selected.Id > 0))
            {
                BasicService.UsersService.User.DeleteRow();
                BasicService.UsersService.User.UpdateRow();
            }
        }

        [RelayCommand]
        private void cmdEditRow()
        {
            if ((BasicService.UsersService.User.Selected != null) && (BasicService.UsersService.User.Selected.Id > 0))
            {
                NavigationService.Navigate(typeof(UserEditView));
            }
        }


        #endregion

        #region Constructor

        public UserLijstView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;

            InitializeComponent();
        }
        #endregion

        #region Events
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.UsersService.User.UpdateSelected();
        }

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.UsersService.User.FilterList(sender.Text);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        #endregion

        #region Fields
        public BasicService BasicService { get; }
        public INavigationService NavigationService { get; set; }


        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
