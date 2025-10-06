using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LIB0000;



namespace LIB0000
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Commands
        #endregion
        #region Constructor
        public App()
        {
#pragma warning disable CS8618 // warning disabled because of the use of Dependency Injection
            // Check if application already started
            string processName = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Length > 1)
            {
                foreach (Process process in processes)
                {
                    if (process.Id != Process.GetCurrentProcess().Id)
                    {
                        MessageBox.Show("Applicatie reeds gestart");
                        _host.StopAsync();
                        _host.Dispose();
                        Application.Current.Shutdown();
                    }
                }
            }

            // Add your standard colors here (Also in Startup)
            // To use in the code : Set your brush to App.ColorOrange
            // To use in the xaml : Background="{StaticResource ColorOrange }"

            ColorGreen = (Brush)new BrushConverter().ConvertFromString("#4CAF50");
            ColorRed = (Brush)new BrushConverter().ConvertFromString("#F44336");
            ColorOrange = (Brush)new BrushConverter().ConvertFromString("#FF9800");
            ColorBlue = (Brush)new BrushConverter().ConvertFromString("#80B9EE");
            ColorWhite = (Brush)new BrushConverter().ConvertFromString("#FFFFFF");
            ColorBlack = (Brush)new BrushConverter().ConvertFromString("#000000");
            ColorYellow = (Brush)new BrushConverter().ConvertFromString("#FF9800");
            ColorGray = (Brush)new BrushConverter().ConvertFromString("#343434");
            ColorDarkGray = (Brush)new BrushConverter().ConvertFromString("#272727");



        }
        #endregion
        #region Events
        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            // Huidige Cultureinfo forceren voor hele applicatie
            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            _host.Start();


        }
        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
        #region Fields





        // Add your standard colors here
        public static Brush ColorGreen { get; set; }
        public static Brush ColorRed { get; set; }
        public static Brush ColorOrange { get; set; }
        public static Brush ColorBlue { get; set; }
        public static Brush ColorWhite { get; set; }
        public static Brush ColorBlack { get; set; }
        public static Brush ColorYellow { get; set; }
        public static Brush ColorGray { get; set; }
        public static Brush ColorDarkGray { get; set; }


        #endregion
        #region Methods

        public void ChangeLanguage(string languageCode)
        {
            var dictionary = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/Dictionaries/{languageCode}Resource.xaml", UriKind.Absolute)
            };

            Resources.MergedDictionaries.Add(dictionary);

            //string languageCode = GetSetting("003", "Basis").ToString();
            //(Application.Current as App)?.ChangeLanguage(languageCode);
        }


        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                #region Services
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<GlobalService>();
                services.AddSingleton<BasicService>();
                services.AddSingleton<ImageService>();

                #endregion

                #region Views

                //Main
                services.AddSingleton<MainView>();

                //Users
                services.AddSingleton<UsersLoginLogoutView>();
                services.AddSingleton<UserLijstView>();
                services.AddSingleton<UserPagesView>();
                services.AddSingleton<UserHistoriekView>();
                services.AddSingleton<UserSelectView>();
                services.AddSingleton<UserEditView>();

                //Product Groups
                services.AddSingleton<ProductGroupSelectView>();
                services.AddSingleton<ProductGroupsEditView>();
                services.AddSingleton<ProductGroupView>();

                //Products
                services.AddSingleton<ProductEditView>();
                services.AddSingleton<ProductSelectView>();
                services.AddSingleton<ProductView>();

                //Product Details
                services.AddSingleton<ProductDetail01>();

                //Order
                services.AddSingleton<OrderSelectView>();
                services.AddSingleton<OrderActualView>();
                services.AddSingleton<OrderHistoryView>();
                services.AddSingleton<OrderView>();

                //Messages
                services.AddSingleton<MessagesActualView>();
                services.AddSingleton<MessagesHistoryView>();
                services.AddSingleton<MessagesPossibleView>();
                services.AddSingleton<MessageStepView>();

                //Workstation
                services.AddSingleton<WorkstationSelectView>();
                services.AddSingleton<WorkstationEditView>();
                services.AddSingleton<WorkstationView>();

                //Settings
                services.AddSingleton<SettingsView>();
                services.AddSingleton<SettingsHistoryView>();

                //Hardware
                services.AddSingleton<HardwareView>();
                services.AddSingleton<HardwareTypeSelectView>();
                services.AddSingleton<HardwareEditView>();

                //About
                services.AddSingleton<AboutView>();

                //VideoInfo
                services.AddSingleton<VideoInfoView>();

                //FunctionHalconShapeSearch
                services.AddSingleton<FunctionHalconShapeSearchView>();

                //EmptyView
                services.AddSingleton<EmptyIndexView>();

                #endregion

                #region Viewmodels
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<AboutViewModel>();
                services.AddSingleton<MessagesViewModel>();

                #endregion

            }).Build();



        #endregion
        #region Properties
        #endregion
    }
}
