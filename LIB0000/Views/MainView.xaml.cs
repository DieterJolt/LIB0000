using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Interop;
using Wpf.Ui.Controls;

namespace LIB0000
{
    public partial class MainView
    {
        #region Commands

        [RelayCommand]
        public void cmdChangePageToUsersLoginLogoutView()
        {

            NavigationService.Navigate(typeof(UsersLoginLogoutView));
        }

        #endregion
        #region Constructor
        public MainView(
                            MainViewModel viewModel,
                            INavigationService navigationService,
                            IServiceProvider serviceProvider,
                            IContentDialogService contentDialogService,
                            BasicService basicService,
                            GlobalService globalService
                            )
        {

            bool result;

            ViewModel = viewModel;
            BasicService = basicService;
            GlobalService = globalService;
            NavigationService = navigationService;

            //PrintService = printService;

            DataContext = this;
            InitializeComponent();

            this.SizeChanged += GetResolution;
            this.LocationChanged += GetResolution;


            NavigationService.SetNavigationControl(NavigationView01);
            //contentDialogService.SetContentPresenter(RootContentDialog);
            NavigationView01.SetServiceProvider(serviceProvider);

            if ((BasicService.UsersService.Pages.List == null) || (BasicService.UsersService.Pages.List.Count == 0))
            {
                CreateListOfUsedPages();
            }

            SaveMessagesListToDatabase(AddMessagesToList());

            BasicService.UsersService.Login.AutoLoginForDefaultOperator();

#if DEBUG
            BasicService.UsersService.Login.ActualUser.User = "Operator";
            BasicService.UsersService.Login.ActualUser.Password = "Operator";
            BasicService.UsersService.Login.ActualUser.Level = 3;
            BasicService.UsersService.Login.IsLoggedIn = true;
#endif

            viewModel.RefreshNavigationVisible();

            // Start the cycle thread

            Thread t = new Thread(new ThreadStart(CycleThread));
            t.Start();
            object b = ViewModel.MenuItems;

        }

        #endregion
        #region Events
        private void EventClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closingApplication = true;
        }



        #endregion
        #region Fields
        public MainViewModel ViewModel { get; }
        public BasicService BasicService { get; }
        public GlobalService GlobalService { get; set; }


        public INavigationService NavigationService { get; set; }

        private bool closingApplication = false;

        #endregion
        #region Methods

        public void GetResolution(object sender, EventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;
            var screen = Screen.FromHandle(handle);

            int screenWidth = screen.Bounds.Width;
            int screenHeight = screen.Bounds.Height;

            //if (screenWidth >= 1920 && screenHeight >= 1080)
            //{
            //    UpdateFontSize("SmallFontSize", 12); //12
            //    UpdateFontSize("MediumFontSize", 14); //14
            //    UpdateFontSize("LargeFontSize", 15);
            //    UpdateFontSize("SmallTitleFontSize", 20); //15
            //    UpdateFontSize("MediumTitleFontSize", 25); //25
            //    UpdateFontSize("LargeTitleFontSize", 40);
            //    UpdateFontSize("SmallIconFontSize", 15);
            //    UpdateFontSize("MediumIconFontSize", 24);
            //    UpdateFontSize("LargeIconFontSize", 45);
            //    UpdateFontSize("TitlebarIconFontSize", 17);

            //}
            //else if (screenWidth < 1920 && screenHeight < 1080)
            //{
            //    UpdateFontSize("SmallFontSize", 10); //10
            //    UpdateFontSize("MediumFontSize", 12); //12
            //    UpdateFontSize("LargeFontSize", 12);
            //    UpdateFontSize("SmallTitleFontSize", 15);//10
            //    UpdateFontSize("MediumTitleFontSize", 20); //20
            //    UpdateFontSize("LargeTitleFontSize", 30);
            //    UpdateFontSize("SmallIconFontSize", 12);
            //    UpdateFontSize("MediumIconFontSize", 19);
            //    UpdateFontSize("LargeIconFontSize", 33);
            //    UpdateFontSize("TitlebarIconFontSize", 15);
            //}

        }

        public void UpdateFontSize(string key, double newValue)
        {
            if (System.Windows.Application.Current.Resources.Contains(key))
            {
                // Verander de waarde van de resource in de ResourceDictionary
                System.Windows.Application.Current.Resources[key] = newValue;
            }
        }

        public void CycleThread()
        {
            while (!closingApplication)
            {
                GlobalService.Clock = DateTime.Now;

                GlobalService.MachineProgram();
                Thread.Sleep(2);

                GlobalService.CycleTime = DateTime.Now - GlobalService.Clock;
                if (GlobalService.MaxCycleTime < GlobalService.CycleTime)
                { GlobalService.MaxCycleTime = GlobalService.CycleTime; }
            }

            foreach (FhService fhService in BasicService.FhService)
            {
                fhService.ClosingApplication = true;
            }

            foreach (CommunicationService communicationService in BasicService.CommunicationService)
            {
                communicationService.ClosingApplication = true;
            }

            foreach (HalconService halconService in BasicService.HalconService)
            {
                halconService.ClosingApplication = true;
            }

            foreach (TurckService turckService in BasicService.TurckService)
            {
                turckService.ClosingApplication = true;
            }

        }

        #region BasicService
        public List<MessageModel> AddMessagesToList()
        {
            // Add your lists of messages here from the different services
            List<MessageModel> lMessages = new List<MessageModel>();
            lMessages.AddRange(MessagesList.GetMessages());
            return lMessages;
        }
        public void SaveMessagesListToDatabase(List<MessageModel> lMessages)
        {
            // Create the list of possible messages to the database
            bool result;
            result = BasicService.MessagesService.CreateDatabase(lMessages);
            if (result == false) { System.Windows.MessageBox.Show("Probleem bij opstart van applicatie : Toevoegen van messages database niet gelukt"); }
        }
        public void CreateListOfUsedPages()
        {
            foreach (var obj in NavigationView01.MenuItems)
            {
                if (obj is NavigationViewItem item) // Controleer en cast in één stap
                {
                    if (item.MenuItems.Count == 0)
                    {
                        BasicService.UsersService.Pages.List.Add(new UserPagesModel
                        {
                            SubPageName = "",
                            PageName = item.Content?.ToString(),
                            Level = 1
                        });
                    }

                    foreach (var subObj in item.MenuItems)
                    {
                        if (subObj is NavigationViewItem subItem) // Controleer en cast
                        {
                            BasicService.UsersService.Pages.List.Add(new UserPagesModel
                            {
                                SubPageName = subItem.Content?.ToString(),
                                PageName = item.Content?.ToString(),
                                Level = 1
                            });
                        }
                    }
                }
            }
            BasicService.UsersService.Pages.SetList();

        }
        #endregion

        #endregion
        #region Properties

        #endregion

    }
}
