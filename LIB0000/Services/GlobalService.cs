using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using System.Windows.Interop;
using LIB0000.Lists;
using LIB0000.Properties;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;

namespace LIB0000
{
    public partial class GlobalService : ObservableObject
    {
        #region Commands



        #endregion
        #region Constructor
        public GlobalService(BasicService basicService, INavigationService navigationService)
        {
            Settings = new SettingsTyp();
            Machine = new MachineTyp();
            Hmi = new HmiTyp();
            Product = new ProductTyp();
            BasicService = basicService;
            NavigationService = navigationService;

        }
        #endregion
        #region Events
        #endregion
        #region Fields
        bool executeOnce = false;
        public BasicService BasicService { get; set; }
        public INavigationService NavigationService { get; set; }
        private DateTime toggle500msOld = DateTime.Now;
        private DateTime toggle5sOld = DateTime.Now;
        #endregion
        #region Methods
        public void MachineProgram()
        {
            init();
            getMachineInputs();
            getHmiCommands();
            getHmiSettings();
            getHmiProduct();
            
            hmiSequence();

            setHmiErrors();
            setHmiStatus();
            setToggleBits();
            setMachineOutputs();
        }

        /// <summary>
        /// Initialisation only once
        /// </summary>
        private void init()
        {
            if (!executeOnce)
            {
                //taal instelling voor opstart programma
                string languageCode = Convert.ToString(BasicService.SettingsService.GetSetting("003", 0, HardwareFunction.None));
                (Application.Current as App)?.ChangeLanguage(languageCode);

                executeOnce = true;
            }
        }

        /// <summary>
        /// Get inputs from Machine(PLC) or other hardware
        /// </summary>
        private void getMachineInputs()
        {
            if (BasicService.CommunicationService.Count > 0)
            {
                //error
                int i = 0;
                for (i = 0; i <= 99; i++)
                {
                    Machine.Error[i] = BasicService.CommunicationService[0].BoolToHmi[i];
                    Machine.ErrorNr[i] = BasicService.CommunicationService[0].SingleToHmi[i];
                }
                // Voorbeeld van data uit de PLC halen status die we willen visualiseren op het scherm
                Machine.Stat.Started = BasicService.CommunicationService[0].BoolToHmi[100];
            }
        }

        /// <summary>
        /// Commandos only on HMI
        /// </summary>
        private void getHmiCommands()
        {           
            if (Hmi.Cmd.Reset)
            {
                Hmi.Cmd.Reset = false;
                // Enkel de errors resetten die niet automatisch gereset worden            
                Hmi.Error.Database = false;

            }
            if (Hmi.Cmd.CloseOrder)
            {
                Hmi.Cmd.CloseOrder = false;
                BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.OrderClose, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Order Closed", BasicService.OrdersService.Order.Loaded.TotalProduct);

                BasicService.OrdersService.Order.CloseOrder();
                BasicService.OrdersService.Order.IsOrderBusy = false;

                BasicService.OrdersService.Order.Loaded = new OrderModel();
                BasicService.ProductGroupsService.ProductGroup.Loaded = new ProductGroupModel();
                BasicService.ProductsService.Product.Loaded = new ProductModel();
                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitOrderDetails;

                foreach (FhService fhService in BasicService.FhService)
                {
                    Task.Run(() => fhService.CmdResetCounters());
                }

                Application.Current.Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(typeof(OrderView));
                });
            }
        }

        /// <summary>
        /// Fix settings for the hmi ( not product dependent )
        /// These are settings that are used on the hmi and not on the machine
        /// Also parameters for FH Service or Halcon Service will be set here
        /// This gets parameters from the list SettingsList
        /// </summary>
        private void getHmiSettings()
        {
            // General standard settings ( always in this application )
            Hmi.Par.WorkstationName = Convert.ToString(BasicService.SettingsService.GetSetting("002", 0, HardwareFunction.None));
            Hmi.Par.Workstation = BasicService.WorkstationsService.Workstations.GetWorkstation(Hmi.Par.WorkstationName);
            string languageCode = Convert.ToString(BasicService.SettingsService.GetSetting("003", 0, HardwareFunction.None));
            Hmi.Par.LoginViaActiveDirectory = Convert.ToBoolean(BasicService.SettingsService.GetSetting("004", 0, HardwareFunction.None));
            // Additional settings for the hmi in a new tab HmiParTab1
            Hmi.Par.ExampleOfTextbox = Convert.ToString(BasicService.SettingsService.GetSetting("001", 0, HardwareFunction.HmiParTab1));
            Hmi.Par.ExampleOfFilePicker = Convert.ToString(BasicService.SettingsService.GetSetting("002", 0, HardwareFunction.HmiParTab1));
            Hmi.Par.ExampleOfFolderPicker = Convert.ToString(BasicService.SettingsService.GetSetting("003", 0, HardwareFunction.HmiParTab1));
            Hmi.Par.ExampleOfComboBox = Convert.ToString(BasicService.SettingsService.GetSetting("004", 0, HardwareFunction.HmiParTab1));
            Hmi.Par.ExampleOfToggleSwitch = Convert.ToBoolean(BasicService.SettingsService.GetSetting("005", 0, HardwareFunction.HmiParTab1));
            Hmi.Par.ExampleOfSlider = Convert.ToDouble(BasicService.SettingsService.GetSetting("006", 0, HardwareFunction.HmiParTab1));     
            // Example of a parameter send to the Halcon Service
            //BasicService.HalconService[0].A001Par = Convert.ToString(BasicService.SettingsService.GetSetting("001", 0, HardwareFunction.HalconParTab1));
            //BasicService.HardwareService[1].A002Par = Convert.ToString(BasicService.SettingsService.GetSetting("001", 0, HardwareFunction.HalconParTab1));
            

            if (Hmi.Par.LanguageCode != languageCode)
            {
                Hmi.Par.LanguageCode = languageCode;
                // Roep de taalwissel aan
                (Application.Current as App)?.ChangeLanguage(languageCode);
            }

        }

        /// <summary>
        /// Connect the product parameters to the service that needs it
        /// </summary>
        private void getHmiProduct()
        {
            // Example of a product parameter send to the Halcon Service
            //BasicService.HalconService[0].A001Par.MinScore = Product.ShapeSearchMinScore;

        }

        /// <summary>
        /// Sequence of the HMI
        /// The sequence is build up in steps
        /// </summary>
        private void hmiSequence()
        {
            switch (Hmi.StepCase)
            {
                case HmiTyp.HmiStepEnum.WaitCommunications:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Communicatie";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op communicatie ok";

                    if (Hmi.Stat.CommunicationOk)
                    {
                        Hmi.StepCase = HmiTyp.HmiStepEnum.WaitErrors;
                    }
                    break;
                case HmiTyp.HmiStepEnum.WaitErrors:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op errors ok";
                    if (!Hmi.Error.Algemeen)
                    {
                        Hmi.StepCase = HmiTyp.HmiStepEnum.WaitSettings;
                    }
                    break;
                case HmiTyp.HmiStepEnum.WaitSettings:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op instellingen ok";
                    if (Hmi.Stat.SettingsOk)
                    {
                        Hmi.StepCase = HmiTyp.HmiStepEnum.WaitLogin;
                    }
                    break;
                case HmiTyp.HmiStepEnum.WaitLogin:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Warning;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op login";

                    if (BasicService.UsersService.Login.IsLoggedIn)
                    {
                        Hmi.StepCase = HmiTyp.HmiStepEnum.WaitOrderLoaded;
                    }
                    break;

                case HmiTyp.HmiStepEnum.WaitOrderLoaded:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Warning;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op order geladen";

                    BasicService.OrdersService.Order.GetLastActiveOrder(Hmi.Par.Workstation.Id);
                    if (BasicService.OrdersService.Order.IsOrderBusy)
                    {

                        BasicService.ProductsService.Product.LoadProduct(BasicService.OrdersService.Order.Loaded.ProductId);
                        BasicService.ProductGroupsService.ProductGroup.LoadProductGroup(BasicService.OrdersService.Order.Loaded.ProductGroupId);
                        //BasicService.ProductDetailService.LoadProductDetails(BasicService.ProductsService.Product.Loaded.Id);

                        Hmi.StepCase = HmiTyp.HmiStepEnum.WaitOrder;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            NavigationService.Navigate(typeof(OrderActualView));
                        });
                    }
                    else
                    { Hmi.StepCase = HmiTyp.HmiStepEnum.WaitOrder; }

                    break;


                case HmiTyp.HmiStepEnum.WaitOrder:

                    switch (BasicService.OrdersService.Order.Loaded.OrderStep)
                    {
                        case OrderStepEnum.WaitOrderDetails:
                            BasicService.MessagesService.StepMessage.StepStatus = MessageType.Warning;
                            BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                            BasicService.MessagesService.StepMessage.ActiveStep.Status = "Vul een order in om te starten";

                            if (Hmi.Cmd.StartOrder)
                            {
                                Hmi.Cmd.StartOrder = false;
                                if (BasicService.OrdersService.Order.Loaded.OrderNr != "")
                                {
                                    BasicService.OrdersService.Order.IsOrderBusy = true;
                                    BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForStart;
                                    BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.OrderStart, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Order start", BasicService.OrdersService.Order.Loaded.TotalProduct);
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        NavigationService.Navigate(typeof(OrderActualView));
                                    });
                                }
                            }
                            break;

                        case OrderStepEnum.WaitForStart:
                            BasicService.MessagesService.StepMessage.StepStatus = MessageType.Wait;
                            BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                            BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op start";

                            if (Machine.Stat.Started)
                            {
                                BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.Run, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Machine Started", BasicService.OrdersService.Order.Loaded.TotalProduct);
                                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForStop;
                            }

                            break;

                        case OrderStepEnum.WaitForStop:
                            if (!Machine.Stat.Started)
                            {
                                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForStart;
                                BasicService.OrdersService.Order.Loaded.MachineStopAmount++;
                                BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.Stop, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Machine stopped", BasicService.OrdersService.Order.Loaded.TotalProduct);
                            }

                            BasicService.MessagesService.StepMessage.StepStatus = MessageType.Running;
                            BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                            BasicService.MessagesService.StepMessage.ActiveStep.Status = "Machine draait, wachten op stop";
                            break;

                    }
                    break;

                case HmiTyp.HmiStepEnum.WaitForReset:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op reset fout";

                    break;
            }

            if (Hmi.StepCase != Hmi.StepBefore)
            {
                Hmi.StepBefore = Hmi.StepCase;
            }

            if (BasicService.OrdersService.Order.Loaded.OrderStep != BasicService.OrdersService.Order.Loaded.OrderStepBefore)
            {
                // update status of the order when the step is changed
                BasicService.OrdersService.Order.UpdateOrder();
                BasicService.OrdersService.Order.Loaded.OrderStepBefore = BasicService.OrdersService.Order.Loaded.OrderStep;
            }

        }
        /// <summary>
        /// Set errors on the HMI
        /// These are all the errors that need to be shown on the HMI
        /// Errors can come from the PLC or internally on the HMI
        /// List of this is in MessagesList
        /// </summary>
        private void setHmiErrors()
        {

            // Errors set internal on the HMI

            Hmi.Error.Algemeen = BasicService.MessagesService.ActualMessage.List.Any(x => x.Type == MessageType.Error);
            Hmi.Error.Database = false;

            if (Hmi.Error.Database)
            {
                BasicService.MessagesService.Set("Database", "001");
            }
            else
            {
                BasicService.MessagesService.Reset("Database", "001");
            }

            if (!Hmi.Stat.SettingsOk)
            {
                BasicService.MessagesService.Set("Machine", "001");
            }
            else
            {
                BasicService.MessagesService.Reset("Machine", "001");
            }

            if (BasicService.UsersService.Login.LoginError == 1 && !BasicService.UsersService.Login.IsLoggedIn)
            {
                BasicService.MessagesService.Set("Login", "001");
            }
            else
            {
                BasicService.MessagesService.Reset("Login", "001");
            }

            if (BasicService.UsersService.Login.LoginError == 2 && !BasicService.UsersService.Login.IsLoggedIn)
            {
                BasicService.MessagesService.Set("Login", "002");
            }
            else
            {
                BasicService.MessagesService.Reset("Login", "002");
            }

            if (BasicService.UsersService.Login.LoginError == 3 && !BasicService.UsersService.Login.IsLoggedIn)
            {
                BasicService.MessagesService.Set("Login", "003");
            }
            else
            {
                BasicService.MessagesService.Reset("Login", "003");
            }

            // Errors from PLC to HMI

            for (int i = 0; i <= 99; i++)
            {
                string errorCode = i.ToString("D3"); // Zorgt voor nul-prefix zoals "002"

                if (Machine.Error[i])
                {
                    BasicService.MessagesService.Set("PLC", errorCode);
                }
                else
                {
                    BasicService.MessagesService.Reset("PLC", errorCode);
                }
            }


        }
        /// <summary>
        /// Set of hmi status to show on the HMI
        /// </summary>
        private void setHmiStatus()
        {
            /*Machine.Stat.Started = true;*/ // altijd true
            Hmi.Stat.SettingsOk = !Hmi.Par.WorkstationName.IsNullOrEmpty() && !(Hmi.Par.Workstation == null);
            Hmi.Stat.CommunicationOk = !Hmi.Stat.HardwareCom;
            Hmi.Stat.ConditionsToStartOk =  (Hmi.StepCase >= HmiTyp.HmiStepEnum.WaitOrder);

        }

        /// <summary>
        /// Set outputs to the machine (PLC) or other hardware
        /// </summary>
        private void setMachineOutputs()
        {
            //Voorbeeld:
            Hmi.Out.OutputExample = ((Hmi.StepCase == HmiTyp.HmiStepEnum.WaitForStart) || (Hmi.StepCase == HmiTyp.HmiStepEnum.WaitForStop));

            if (Hmi.Stat.Started)
            {
                Hmi.Out.OutputExample = Toggle500ms;
            }

            // Voorbeeld:

            if (BasicService.CommunicationService.Count > 0)
            {
                BasicService.CommunicationService[0].BoolToHmi[0] = Machine.Cmd.Start;
                BasicService.CommunicationService[0].BoolToHmi[1] = Hmi.Stat.ConditionsToStartOk;

                BasicService.CommunicationService[0].SingleToHmi[0] = Convert.ToSingle(BasicService.SettingsService.GetSetting("001", 0, HardwareFunction.MachineParTab1));
            }



            


        }
        /// <summary>
        /// Standard toggle bits for blinking
        /// </summary>
        private void setToggleBits()
        {
            TimeSpan tS = DateTime.Now - toggle500msOld;
            if (tS.TotalMilliseconds > 500)
            {
                toggle500msOld = DateTime.Now;
                Toggle500ms = !Toggle500ms;
            }

            tS = DateTime.Now - toggle500msOld;
            if (tS.TotalSeconds > 5)
            {
                toggle5sOld = DateTime.Now;
                Toggle5s = !Toggle5s;
            }
        }

        #endregion
        #region Properties
        [ObservableProperty]
        private DateTime _clock;
        [ObservableProperty]
        private TimeSpan _cycleTime;
        [ObservableProperty]
        private TimeSpan _maxCycleTime;
        [ObservableProperty]
        private MachineTyp _machine;
        [ObservableProperty]
        private HmiTyp _hmi;
        [ObservableProperty]
        private ProductTyp _product;
        [ObservableProperty]
        private SettingsTyp _settings;
        [ObservableProperty]
        private string _applicatieTitel = "LIB0000 : Standard JOLT applicatie";
        [ObservableProperty]
        private string _applicatieNummer = "LIB0000";
        [ObservableProperty]
        private bool _toggle500ms;
        [ObservableProperty]
        private bool _toggle5s;

        #endregion
    }
}
