using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.InteropServices;
using LIB0000;

namespace LIB0000
{
    public partial class BasicService : ObservableObject
    {
        #region Commands

        #endregion

        #region Constructor

        public BasicService(INavigationService navigationService)
        {

            NavigationService = navigationService;

            List<int> buttons = new List<int>();
            buttons.Add(0);

            // Lokale database aanmaken 
            CreateLocalDatabase();
            // MessageService wordt aangemaakt, in constructor worden messages afgevraagd
            MessagesService = new MessageService();
            // HardwareService wordt aangemaakt, in constructor wordt hardware afgevraagd
            HardwareService = new HardwareService();
            // Settings database aanmaken indien deze niet bestaat
            SettingsService = new SettingService();

            // Algemene settings worden aangemaakt indien nog dit nog niet bestaat, deze staan in SettingsList.cs
            // 0 = machine settings
            bool result = SettingsService.AddToSettingsDatabase(SettingsList.GetSettings(HardwareType.None, HardwareFunction.None), 0, "Algemeen");
            result = SettingsService.AddToSettingsDatabase(SettingsList.GetSettings(HardwareType.None, HardwareFunction.MachineParTab1), 0, "MachineTab1");


            if (result == false) { System.Windows.MessageBox.Show("Probleem bij opstart van applicatie : Toevoegen van settings database niet gelukt"); }

            // Server path ophalen
            ServerPath = SettingsService.GetSetting("001", 0, HardwareFunction.None);

            //ServerPath = "";
            //ServerDbContext.Local = true;

            ServerDbContext.Local = true;

            CreateServerDatabase(ServerPath);


            // lokaal

            // InfoVideos.db
            // Trend.db
            // Recipes.db

            // if server path == null or broken
            // Users.db lokaal


            // ophalen van het server path en werkstation in settings.db in het lokale path


            // server
            // Users.db
            // Recipes.db
            //

            ProductDetailService = new ProductDetailService(ServerPath);
            ProductsService = new ProductsService(ServerPath);
            UsersService = new UserService(ServerPath);
            OrdersService = new OrderService(ServerPath);
            RecipesService = new RecipeService(ServerPath);
            InstructionsService = new InstructionService(ServerPath);
            WorkstationsService = new WorkstationService(ServerPath);
            ProductGroupsService = new ProductGroupService(ServerPath);



            //Hier worden de default settings voor alle gekoppelde hardware toegevoegd aan de productdetaildatabase

            foreach (HardwareModel hardware in HardwareService.Hardware.List)
            {
                result = SettingsService.AddToSettingsDatabase(SettingsList.GetSettings(hardware.HardwareType, HardwareFunction.None), hardware.Id, hardware.Name); // Toevoegen basissettings


                ObservableCollection<HardwareFunction> hardwareFunctionsList = HardwareFunctionMapper.GetFunctionsForHardware(hardware.HardwareType);

                foreach (HardwareFunction hardwareFunction in hardwareFunctionsList)    // Toevoegen hardwarefunctions
                {
                    result = ProductDetailService.AddDefaultSettingsToProductDetailDatabase(ProductDetailList.GetSettings(hardware.HardwareType, hardwareFunction), hardware.Id, hardware.Name);

                }

                //Aanmaak services
                if (hardware.HardwareType == HardwareType.PLC)
                {
                    string ipAddress = SettingsService.GetSetting("001", hardware.Id, HardwareFunction.None); // Algemene setting IP-adres ophalen
                    CommunicationService.Add(new CommunicationService { IpAddress = ipAddress, HardwareId = hardware.Id });
                }

                if (hardware.HardwareType == HardwareType.FHV7)
                {
                    string ipAddress = SettingsService.GetSetting("001", hardware.Id, HardwareFunction.None); // Algemene setting IP-adres ophalen
                    FhService.Add(new FhService { IpAddress = ipAddress, HardwareId = hardware.Id });
                }
                if (hardware.HardwareType == HardwareType.GigeCam)
                {
                    string ipAddress = SettingsService.GetSetting("001", hardware.Id, HardwareFunction.None); // Algemene setting IP-adres ophalen
                    HalconService.Add(new HalconService { IpAddress = ipAddress, HardwareId = hardware.Id });
                }

            }
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        public INavigationService NavigationService { get; set; }
        #endregion

        #region Methods
        public void CreateLocalDatabase()
        {
            using (var context = new LocalDbContext())
            {
                bool created = context.Database.EnsureCreated();
                SettingsService = new SettingService();
            }

        }
        public void CreateServerDatabase(string databasePath)
        {
            using (var context = new ServerDbContext(databasePath))
            {
                bool created = context.Database.EnsureCreated();
                ProductDetailService = new ProductDetailService(databasePath);
            }
        }

        public void ChangeHalconServiceSelected(int i)
        {
            HalconServiceSelected = HalconService[i];
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private UserService _usersService;

        [ObservableProperty]
        private MessageService _messagesService;

        [ObservableProperty]
        private RecipeService _recipesService;

        [ObservableProperty]
        private InstructionService _instructionsService;

        [ObservableProperty]
        private WorkstationService _workstationsService;

        [ObservableProperty]
        private ProductGroupService _productGroupsService;

        [ObservableProperty]
        private ProductDetailService _productDetailService;

        [ObservableProperty]
        private HardwareService _hardwareService;

        [ObservableProperty]
        private ProductsService _productsService;

        [ObservableProperty]
        private VideoInfoService _infoVideosService;

        [ObservableProperty]
        private ImageService _imagesService;

        [ObservableProperty]
        private SettingService _settingsService;


        [ObservableProperty]
        private OrderService _ordersService;

        [ObservableProperty]
        private string _localPath;

        [ObservableProperty]
        private string _serverPath;

        [ObservableProperty]
        private ObservableCollection<CommunicationService> _communicationService = new();

        [ObservableProperty]
        private ObservableCollection<FhService> _fhService = new();

        [ObservableProperty]
        private ObservableCollection<HalconService> _halconService = new();

        [ObservableProperty]
        private HalconService _halconServiceSelected;

        #endregion


    }
}
