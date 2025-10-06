using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using LIB0000.Properties;
using Wpf.Ui.Controls;

namespace LIB0000
{
    public partial class MainViewModel : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public MainViewModel(BasicService basicService)
        {
            BasicService = basicService;

        }
        #endregion

        #region Events
        #endregion

        #region Fields
        public BasicService BasicService;

        #endregion

        #region Methods

        public void RefreshNavigationVisible()
        {

            foreach (var obj in MenuItems)
            {
                if (obj is NavigationViewItem item) // Controleer en cast in één stap
                {
                    // Controleer of het een NavigationViewItem is en geen subitems heeft
                    if (item.MenuItems.Count == 0)
                    {
                        UserPagesModel userPage = BasicService.UsersService.Pages.List
                            .FirstOrDefault(x => x.PageName == item.Content?.ToString() && x.SubPageName == "");

                        if (userPage != null && userPage.Level <= BasicService.UsersService.Login.ActualUser.Level)
                        {
                            item.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            item.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        // Als er subitems zijn, controleer elk subitem
                        bool anyVisibleSubItem = false;

                        foreach (var subObj in item.MenuItems)
                        {
                            if (subObj is NavigationViewItem subItem) // Controleer en cast subitem
                            {
                                // Gebruik zowel PageName als SubPageName in de query
                                UserPagesModel subUserPage = BasicService.UsersService.Pages.List
                                    .FirstOrDefault(x => x.PageName == item.Content?.ToString()
                                                      && x.SubPageName == subItem.Content?.ToString());

                                if (subUserPage != null && subUserPage.Level <= BasicService.UsersService.Login.ActualUser.Level)
                                {
                                    subItem.Visibility = Visibility.Visible;
                                    anyVisibleSubItem = true; // Er is ten minste één zichtbaar subitem
                                }
                                else
                                {
                                    subItem.Visibility = Visibility.Collapsed;
                                }
                            }
                        }

                        // Toon het hoofditem alleen als een subitem zichtbaar is
                        item.Visibility = anyVisibleSubItem ? Visibility.Visible : Visibility.Collapsed;
                    }
                }
            }


        }
        #endregion

        #region Properties

        [ObservableProperty]
        private int _height;

        [ObservableProperty]
        private string _applicationTitle = "";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem("Overzichten", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Overviews.png")), typeof(EmptyIndexView))
            {
            MenuItemsSource = new object[]
            {
                new NavigationViewItem("Actueel order", typeof(OrderActualView)),
            }
            },
            new NavigationViewItem("Order",new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Orders.png")), typeof(OrderView)),
            new NavigationViewItem("Order history",new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Orders.png")), typeof(OrderSelectView)),
            new NavigationViewItem("Meldingen", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Messages.png")), typeof(EmptyIndexView))
            {
            MenuItemsSource = new object[]
            {
                new NavigationViewItem("Actuele meldingen", typeof(MessagesActualView)),
                new NavigationViewItem("Historiek meldingen", typeof(MessagesHistoryView)),
                new NavigationViewItem("Lijst meldingen", typeof(MessagesPossibleView)),
            }
            },


            new NavigationViewItemSeparator(),


            new NavigationViewItem("Werkstations", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Workstations.png")), typeof(WorkstationView)),
            new NavigationViewItem("Product groepen", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/ProductGroups.png")), typeof(ProductGroupView)),
            new NavigationViewItem("Producten", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Products.png")), typeof(ProductView)),
            new NavigationViewItem("Gebruikers", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/User.png")), typeof(EmptyIndexView))
            {
            MenuItemsSource = new object[]
            {
                new NavigationViewItem("Lijst gebruikers", typeof(UserLijstView)),
                new NavigationViewItem("Historiek gebruikers", typeof(UserHistoriekView)),
                new NavigationViewItem("Pagina's / gebruiker", typeof(UserPagesView))
            }
            },
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItemSeparator(),

            new NavigationViewItem("Hardware", new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Hardware.png")), typeof(HardwareView)),
            new NavigationViewItem("Instellingen",new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Settings.png")),typeof(SettingsView))
            {
            },
            new NavigationViewItem()
            {
                Content = "Over deze app",
                Image = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Info.png")),
                TargetPageType = typeof(AboutView)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        #endregion

    }
}
