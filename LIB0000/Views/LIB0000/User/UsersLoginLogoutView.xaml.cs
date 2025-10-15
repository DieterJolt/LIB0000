using System.Windows.Input;

namespace LIB0000
{
    public sealed partial class UsersLoginLogoutView
    {
        #region Commands

        [RelayCommand]
        private void cmdInloggen()
        {
            if (BasicService.UsersService.Login.InputUser == null || BasicService.UsersService.Login.InputUser.User == null | BasicService.UsersService.Login.InputUser.Password == null)
            { return; }

            if ((!GlobalService.Hmi.Par.LoginViaActiveDirectory) || (BasicService.UsersService.Login.InputUser.User == "User"
                && BasicService.UsersService.Login.InputUser.Password == "ORG0001"))
            {
                BasicService.UsersService.LoginAndRegister();
            }
            else
            {
                BasicService.UsersService.LoginWindowsServerAndRegister();
            }

            MainViewModel.RefreshNavigationVisible();
            //if (BasicService.UsersService.Login.ActualUser.Level >2)
            //{
            //   Application.Current.MainWindow.Topmost = false;
            //}
            //else
            //{
            //    Application.Current.MainWindow.Topmost = true;  
            //}
            if (BasicService.UsersService.Login.IsLoggedIn)
            {
                NavigationService.Navigate(typeof(OrderActualView));
            }
        }

        [RelayCommand]
        private void cmdUitloggen()
        {
            BasicService.UsersService.LogoutAndRegister();
            MainViewModel.RefreshNavigationVisible();
        }

        [RelayCommand]
        private void cmdNavigate()
        {
            NavigationService.Navigate(typeof(UserSelectView));
        }

        #endregion

        #region Constructor

        public UsersLoginLogoutView(BasicService basicService, MainViewModel mainViewModel, GlobalService globalService, INavigationService navigationService)
        {
            BasicService = basicService;
            MainViewModel = mainViewModel;
            NavigationService = navigationService;
            GlobalService = globalService;
            FooterBtn1 = "Button 1";
            FooterBtn2 = "Button 2";
            FooterBtn3 = "Button 3";
            FooterBtn4 = "Button 4";
            FooterBtn5 = "Button 5";
            FooterBtn6 = "Button 6";
            FooterBtn1Icon = FontAwesome.Sharp.IconChar.FortAwesome;
            FooterBtn2Icon = FontAwesome.Sharp.IconChar.FortAwesome;
            FooterBtn3Icon = FontAwesome.Sharp.IconChar.FortAwesome;
            FooterBtn4Icon = FontAwesome.Sharp.IconChar.FortAwesome;
            FooterBtn5Icon = FontAwesome.Sharp.IconChar.FortAwesome;
            FooterBtn6Icon = FontAwesome.Sharp.IconChar.FortAwesome;
            DataContext = this;
            InitializeComponent();

        }
        #endregion

        #region Events



        #endregion

        #region Fields
        public BasicService BasicService { get; }
        public MainViewModel MainViewModel { get; }
        public INavigationService NavigationService { get; set; }
        public GlobalService GlobalService { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Properties

        public string FooterBtn1 { get; set; }
        public string FooterBtn2 { get; set; }
        public string FooterBtn3 { get; set; }
        public string FooterBtn4 { get; set; }
        public string FooterBtn5 { get; set; }
        public string FooterBtn6 { get; set; }

        public FontAwesome.Sharp.IconChar FooterBtn1Icon { get; set; }
        public FontAwesome.Sharp.IconChar FooterBtn2Icon { get; set; }
        public FontAwesome.Sharp.IconChar FooterBtn3Icon { get; set; }
        public FontAwesome.Sharp.IconChar FooterBtn4Icon { get; set; }
        public FontAwesome.Sharp.IconChar FooterBtn5Icon { get; set; }
        public FontAwesome.Sharp.IconChar FooterBtn6Icon { get; set; }



        #endregion

        private void PasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                cmdInloggen();
            }
        }


    }
}
