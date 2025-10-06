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
    public partial class UserSelectView
    {

        #region Commands

        [RelayCommand]
        private void cmdSelectUser()
        {
            if (BasicService.UsersService.User.Selected.User != null)
            {

                BasicService.UsersService.Login.InputUser.User = BasicService.UsersService.User.Selected.User;
                BasicService.UsersService.Login.InputUser.PasswordRequired = BasicService.UsersService.User.Selected.PasswordRequired;
                NavigationService.GoBack();
            }

        }

        [RelayCommand]
        private void cmdGoBack()
        {
            NavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public UserSelectView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

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
