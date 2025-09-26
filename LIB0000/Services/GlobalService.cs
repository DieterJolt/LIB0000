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
            Recipe = new RecipeTyp();
            Settings = new SettingsTyp();
            Machine = new MachineTyp();

            Invoer = new InvoerTyp();
            Uitvoer = new UitvoerTyp();
            Recipe = new RecipeTyp();
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
            getInputs();
            getCommands();
            getSettings();

            machineSequence();

            setErrorMessages();
            setErrors();
            setStatus();
            setToggleBits();
            setOutputs();
        }

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

        private void getInputs()
        {
            //Voorbeeld:
            //Machine.In.InputExample = IoSollaeCieh14AService.Inputs[0];
            if (BasicService.CommunicationService.Count > 0)
            {
                Machine.In.Start = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[0];
                Machine.In.Stop = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[1];
                Machine.In.LightSwitch = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[2];

                Machine.Stat.BovenVerlichting = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[3];
                Machine.Stat.Started = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[4];
                Machine.Stat.Stopped = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[5];

                Machine.Error.StoppedByCamera = BasicService.CommunicationService[0].ToVsFromPlcBoolCollection[6];
            }
        }

        private void getCommands()
        {

            if (Machine.Cmd.Reset)
            {
                Machine.Cmd.Reset = false;
                // Enkel de errors resetten die niet automatisch gereset worden            
                Machine.Error.Database = false;

            }

            if (Machine.Cmd.EndOrder)
            {
                Machine.Cmd.EndOrder = false;
                BasicService.OrdersService.Order.IsOrderBusy = false;
                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForMachineInstructionsAfterOrder;
            }

            if (Machine.Cmd.CloseOrder)
            {
                Machine.Cmd.CloseOrder = false;
                BasicService.OrdersService.Order.CloseOrder();

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

        DateTime nextTimeGenerateData = DateTime.Now; // test LL, deze method mag weg na voltooien Acutal View
        int nextCounterGenerateData = 0; // test LL, deze method mag weg na voltooien Acutal View


        private void getSettings()
        {
            Machine.Par.WorkstationName = Convert.ToString(BasicService.SettingsService.GetSetting("002", 0, HardwareFunction.None));
            Machine.Par.Workstation = BasicService.WorkstationsService.Workstations.GetWorkstation(Machine.Par.WorkstationName);
            string languageCode = Convert.ToString(BasicService.SettingsService.GetSetting("003", 0, HardwareFunction.None));
            Machine.Par.LoginViaActiveDirectory = Convert.ToBoolean(BasicService.SettingsService.GetSetting("004", 0, HardwareFunction.None));
            Machine.Par.ExampleOfTextbox = Convert.ToString(BasicService.SettingsService.GetSetting("001", 0, HardwareFunction.MachineParTab1));
            Machine.Par.ExampleOfFilePicker = Convert.ToString(BasicService.SettingsService.GetSetting("002", 0, HardwareFunction.MachineParTab1));
            Machine.Par.ExampleOfFolderPicker = Convert.ToString(BasicService.SettingsService.GetSetting("003", 0, HardwareFunction.MachineParTab1));
            Machine.Par.ExampleOfComboBox = Convert.ToString(BasicService.SettingsService.GetSetting("004", 0, HardwareFunction.MachineParTab1));
            Machine.Par.ExampleOfToggleSwitch = Convert.ToBoolean(BasicService.SettingsService.GetSetting("005", 0, HardwareFunction.MachineParTab1));
            Machine.Par.ExampleOfSlider = Convert.ToDouble(BasicService.SettingsService.GetSetting("006", 0, HardwareFunction.MachineParTab1));

            if (Machine.Par.LanguageCode != languageCode)
            {
                Machine.Par.LanguageCode = languageCode;
                // Roep de taalwissel aan
                (Application.Current as App)?.ChangeLanguage(languageCode);
            }

        }

        private void machineSequence()
        {
            switch (Machine.StepCase)
            {
                case MachineTyp.MachineStepEnum.WaitCommunications:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Communicatie";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op communicatie ok";

                    if (Machine.Stat.CommunicationOk)
                    {
                        Machine.StepCase = MachineTyp.MachineStepEnum.WaitErrors;
                    }
                    break;
                case MachineTyp.MachineStepEnum.WaitErrors:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op errors ok";
                    if (!Machine.Error.Algemeen)
                    {
                        Machine.StepCase = MachineTyp.MachineStepEnum.WaitSettings;
                    }
                    break;
                case MachineTyp.MachineStepEnum.WaitSettings:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op instellingen ok";
                    if (Machine.Stat.SettingsOk)
                    {
                        Machine.StepCase = MachineTyp.MachineStepEnum.WaitLogin;
                    }
                    break;
                case MachineTyp.MachineStepEnum.WaitLogin:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Warning;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op login";

                    if (BasicService.UsersService.Login.IsLoggedIn)
                    {
                        Machine.StepCase = MachineTyp.MachineStepEnum.WaitOrderLoaded;
                    }
                    break;

                case MachineTyp.MachineStepEnum.WaitOrderLoaded:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Warning;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op order geladen";

                    BasicService.OrdersService.Order.GetLastActiveOrder(Machine.Par.Workstation.Id);
                    if (BasicService.OrdersService.Order.IsOrderBusy)
                    {

                        BasicService.ProductsService.Product.LoadProduct(BasicService.OrdersService.Order.Loaded.ProductId);
                        BasicService.ProductGroupsService.ProductGroup.LoadProductGroup(BasicService.OrdersService.Order.Loaded.ProductGroupId);
                        BasicService.ProductDetailService.LoadProductDetails(BasicService.ProductsService.Product.Loaded.Id);

                        Machine.StepCase = MachineTyp.MachineStepEnum.WaitOrder;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            NavigationService.Navigate(typeof(OrderActualView));
                        });
                    }
                    else
                    { Machine.StepCase = MachineTyp.MachineStepEnum.WaitOrder; }

                    break;


                case MachineTyp.MachineStepEnum.WaitOrder:

                    switch (BasicService.OrdersService.Order.Loaded.OrderStep)
                    {
                        case OrderStepEnum.WaitOrderDetails:
                            BasicService.MessagesService.StepMessage.StepStatus = MessageType.Warning;
                            BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                            BasicService.MessagesService.StepMessage.ActiveStep.Status = "Vul een order in om te starten";

                            if (Machine.Cmd.StartOrder)
                            {
                                Machine.Cmd.StartOrder = false;
                                if (BasicService.OrdersService.Order.Edit.OrderNr != null && BasicService.OrdersService.Order.Loaded.OrderNr != "")
                                {
                                    BasicService.OrdersService.Order.IsOrderBusy = true;
                                    BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForStart;
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
                                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForStop;
                                BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.Run, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Machine started", BasicService.OrdersService.Order.Loaded.TotalProduct);
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

                case MachineTyp.MachineStepEnum.WaitForReset:
                    BasicService.MessagesService.StepMessage.StepStatus = MessageType.Error;
                    BasicService.MessagesService.StepMessage.ActiveStep.Group = "Algemeen";
                    BasicService.MessagesService.StepMessage.ActiveStep.Status = "Wachten op reset fout";

                    break;
            }

            if (Machine.StepCase != Machine.StepBefore)
            {
                Machine.StepBefore = Machine.StepCase;
            }

            if (BasicService.OrdersService.Order.Loaded.OrderStep != BasicService.OrdersService.Order.Loaded.OrderStepBefore)
            {
                BasicService.OrdersService.Order.UpdateOrder();
                BasicService.OrdersService.Order.Loaded.OrderStepBefore = BasicService.OrdersService.Order.Loaded.OrderStep;
            }

        }

        private void setErrorMessages()
        {
            if (Machine.Error.Database)
            {
                BasicService.MessagesService.Set("Database", "001");
            }
            else
            {
                BasicService.MessagesService.Reset("Database", "001");
            }

            if (!Machine.Stat.SettingsOk)
            {
                BasicService.MessagesService.Set("Machine", "001");
            }
            else
            {
                BasicService.MessagesService.Reset("Machine", "001");
            }

            if (Machine.Error.TimeoutWorkstation)
            {
                BasicService.MessagesService.Set("Machine", "002");
            }
            else
            {
                BasicService.MessagesService.Reset("Machine", "002");
            }

            if (Machine.Error.TimeoutProductgroup)
            {
                BasicService.MessagesService.Set("Machine", "003");
            }
            else
            {
                BasicService.MessagesService.Reset("Machine", "003");
            }

            if (Machine.Error.TimeoutProduct)
            {
                BasicService.MessagesService.Set("Machine", "004");
            }
            else
            {
                BasicService.MessagesService.Reset("Machine", "004");
            }

            if (Machine.Error.StoppedByCamera)
            {
                if (BasicService.MessagesService.ActualMessage.List.Count(x => x.Type == MessageType.Error && x.Group == "Machine" && x.Nr == "005") == 0)
                {
                    BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.StoppedByCamera, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Camera fout detectie", BasicService.OrdersService.Order.Loaded.TotalProduct);
                }
                BasicService.MessagesService.Set("Machine", "005");
            }
            else
            {
                BasicService.MessagesService.Reset("Machine", "005");
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

        }
        private void setErrors()
        {
            Machine.Error.Algemeen = BasicService.MessagesService.ActualMessage.List.Any(x => x.Type == MessageType.Error);
            Machine.Error.Database = false;

            if ((Machine.Stat.TellerWorkstationInstructionStart < BasicService.OrdersService.Order.Loaded.TotalProduct) && (BasicService.OrdersService.Order.Loaded.OrderStep == OrderStepEnum.WaitForMachineInstructionsDuringOrder))
            {
                if (Machine.Error.TimeoutWorkstation == false)
                {
                    BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.Timeout, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Workstation instructionlist timeout", BasicService.OrdersService.Order.Loaded.TotalProduct);
                }
                Machine.Error.TimeoutWorkstation = true;
            }
            else
            {
                Machine.Error.TimeoutWorkstation = false;
            }

            if ((Machine.Stat.TellerProductgroupInstructionStart < BasicService.OrdersService.Order.Loaded.TotalProduct) && (BasicService.OrdersService.Order.Loaded.OrderStep == OrderStepEnum.WaitForProductGroupInstructionsDuringOrder))
            {
                if (Machine.Error.TimeoutProductgroup == false)
                {
                    BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.Timeout, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "ProductGroup instructionlist timeout", BasicService.OrdersService.Order.Loaded.TotalProduct);
                }
                Machine.Error.TimeoutProductgroup = true;
            }
            else
            {
                Machine.Error.TimeoutProductgroup = false;
            }

            if ((Machine.Stat.TellerProductInstructionStart < BasicService.OrdersService.Order.Loaded.TotalProduct) && (BasicService.OrdersService.Order.Loaded.OrderStep == OrderStepEnum.WaitForProductInstructionsDuringOrder))
            {
                if (Machine.Error.TimeoutProduct == false)
                {
                    BasicService.OrdersService.Order.AddOrderHistory(OrderHistoryType.Timeout, BasicService.UsersService.Login.ActualUser.User, BasicService.OrdersService.Order.Loaded.Amount, "Product instructionlist timeout", BasicService.OrdersService.Order.Loaded.TotalProduct);
                }
                Machine.Error.TimeoutProduct = true;
            }
            else
            {
                Machine.Error.TimeoutProduct = false;
            }
        }
        private void setStatus()
        {
            if (Machine.Error.StoppedByCamera)
            {
                Machine.Cmd.OrCameraNok = false;
            }

            if (BasicService.FhService.Count > 0)
            {
                if (BasicService.FhService[0].AlreadyDataReceivedSinceStartup) // Enkel indien er reeds data is binnengekomen mogen de counters geupdate worden, anders staan deze op nul bij opstart van een geladen order
                {
                    if (BasicService.FhService[0].CounterBad > BasicService.OrdersService.Order.Loaded.BadProduct)
                    {
                        Machine.Cmd.OrCameraNok = true;
                    }

                    if (BasicService.FhService[0].CounterTotal > BasicService.OrdersService.Order.Loaded.TotalProduct)
                    {
                        BasicService.OrdersService.Order.UpdateOrder();
                    }
                    BasicService.OrdersService.Order.Loaded.BadProduct = BasicService.FhService[0].CounterBad;
                    BasicService.OrdersService.Order.Loaded.TotalProduct = BasicService.FhService[0].CounterTotal;
                    BasicService.OrdersService.Order.Loaded.GoodProduct = BasicService.FhService[0].CounterGood;
                }
            }

            /*Machine.Stat.Started = true;*/ // altijd true
            Machine.Stat.SettingsOk = !Machine.Par.WorkstationName.IsNullOrEmpty() && !(Machine.Par.Workstation == null);
            Machine.Stat.CommunicationOk = !Machine.Stat.HardwareCom;

            Machine.Stat.ConditionsToStartOk = (BasicService.OrdersService.Order.Loaded.OrderStep > OrderStepEnum.WaitForProductInstructionsBeforeOrder) &&
                                                !Machine.Error.TimeoutWorkstation &&
                                                !Machine.Error.TimeoutProductgroup &&
                                                !Machine.Error.TimeoutProduct &&
                                                !Machine.Error.StoppedByCamera &&
                                                (Machine.StepCase >= MachineTyp.MachineStepEnum.WaitOrder);

            //opstarten tussentijdse instructieControle voor workstation
            WorkstationModel workstation = BasicService.WorkstationsService.Workstations.GetWorkstation(Machine.Par.WorkstationName);
            if ((workstation != null) && (BasicService.OrdersService.Order.Loaded.OrderStep == OrderStepEnum.WaitForStop) && (BasicService.OrdersService.Order.Loaded.TotalProduct >= Machine.Stat.NextPeriodicControlWorkstation && BasicService.OrdersService.Order.Loaded.TotalProduct != 0) &&
              BasicService.InstructionsService.Instructions.GetInstructionCount(Machine.Par.Workstation.InstructionListPeriodicIdBefore) > 0)
            {
                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForMachineInstructionsDuringOrder;

                Machine.Stat.NextPeriodicControlWorkstation += Machine.Par.Workstation.InstructionListPeriodicFrequency;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(typeof(InstructionView));
                });
            }

            //opstarten tussentijdse instructieControle voor productgroup
            ProductGroupModel productgroup = BasicService.ProductGroupsService.ProductGroup.GetProductGroup(BasicService.ProductGroupsService.ProductGroup.Loaded.Id);
            if ((productgroup != null) && (BasicService.OrdersService.Order.Loaded.OrderStep == OrderStepEnum.WaitForStop) && (productgroup.InstructionListPeriodicFrequency != 0) && (BasicService.OrdersService.Order.Loaded.TotalProduct >= Machine.Stat.NextPeriodicControlProductgroup && BasicService.OrdersService.Order.Loaded.TotalProduct != 0) &&
              BasicService.InstructionsService.Instructions.GetInstructionCount(BasicService.ProductGroupsService.ProductGroup.Loaded.InstructionListPeriodicIdBefore) > 0)
            {
                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForProductGroupInstructionsDuringOrder;

                Machine.Stat.NextPeriodicControlProductgroup += BasicService.ProductGroupsService.ProductGroup.Loaded.InstructionListPeriodicFrequency;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(typeof(InstructionView));
                });
            }

            //opstarten tussentijdse instructieControle voor product
            ProductModel product = BasicService.ProductsService.Product.GetProduct(BasicService.ProductsService.Product.Loaded.Id);
            if ((product != null) && (BasicService.OrdersService.Order.Loaded.OrderStep == OrderStepEnum.WaitForStop) && (product.InstructionListPeriodicFrequency != 0) && (BasicService.OrdersService.Order.Loaded.TotalProduct >= Machine.Stat.NextPeriodicControlProduct && BasicService.OrdersService.Order.Loaded.TotalProduct != 0) &&
              BasicService.InstructionsService.Instructions.GetInstructionCount(BasicService.ProductsService.Product.Loaded.InstructionListPeriodicIdBefore) > 0)
            {
                BasicService.OrdersService.Order.Loaded.OrderStep = OrderStepEnum.WaitForProductInstructionsDuringOrder;

                Machine.Stat.NextPeriodicControlProduct += BasicService.ProductsService.Product.Loaded.InstructionListPeriodicFrequency;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(typeof(InstructionView));
                });
            }
        }


        private void setOutputs()
        {
            //Voorbeeld:
            Machine.Out.OutputExample = ((Machine.StepCase == MachineTyp.MachineStepEnum.WaitForStart) || (Machine.StepCase == MachineTyp.MachineStepEnum.WaitForStop));

            if (Machine.Stat.Started)
            {
                Machine.Out.OutputExample = Toggle500ms;
            }

            // Voorbeeld:

            if (BasicService.CommunicationService.Count > 0)
            {
                BasicService.CommunicationService[0].FromVsToPlcBoolCollection[0] = Machine.Cmd.OrCameraNok;
                BasicService.CommunicationService[0].FromVsToPlcBoolCollection[1] = Machine.Stat.ConditionsToStartOk;
            }



        }
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

        private bool WaitForNextInstruction()
        {
            if (!BasicService.OrdersService.Order.LastInstructionOk)
                return false;

            BasicService.OrdersService.Order.LastInstructionOk = false;
            var instruction = BasicService.InstructionsService.Instructions.Selected;
            bool conditionsOk = true;

            switch (instruction.InstructionType)
            {
                case InstructionType.Hotspot:
                    if (instruction.HotspotId != instruction.InputText1)
                    {
                        conditionsOk = false;
                    }
                    break;
            }

            if (!conditionsOk)
            {
                if (instruction.InstructionType == InstructionType.Hotspot)
                {
                    BasicService.OrdersService.Order.AddOrderHistory(
                        OrderHistoryType.InstructionNok,
                        BasicService.UsersService.Login.ActualUser.User,
                        BasicService.OrdersService.Order.Loaded.Amount,
                        "extra info",
                        BasicService.OrdersService.Order.Loaded.TotalProduct,
                        instruction);
                    instruction.InputText1 = "";
                }
                return false;
            }

            BasicService.OrdersService.Order.AddOrderHistory(
                OrderHistoryType.InstructionOk,
                BasicService.UsersService.Login.ActualUser.User,
                BasicService.OrdersService.Order.Loaded.Amount,
                instruction.InstructionTypeName,
                BasicService.OrdersService.Order.Loaded.TotalProduct,
                instruction);

            var loadedOrder = BasicService.OrdersService.Order.Loaded;
            int instructionCount = 0;
            int? instructionListId = 0;

            switch (loadedOrder.OrderStep)
            {
                case OrderStepEnum.WaitForMachineInstructionsBeforeOrder:
                    instructionListId = Machine.Par.Workstation.InstructionListIdBefore;
                    break;
                case OrderStepEnum.WaitForMachineInstructionsDuringOrder:
                    instructionListId = Machine.Par.Workstation.InstructionListPeriodicIdBefore;
                    break;
                case OrderStepEnum.WaitForMachineInstructionsAfterOrder:
                    instructionListId = Machine.Par.Workstation.InstructionListIdAfter;
                    break;
                case OrderStepEnum.WaitForProductGroupInstructionsBeforeOrder:
                    instructionListId = BasicService.ProductGroupsService.ProductGroup.Loaded.InstructionListIdBefore;
                    break;
                case OrderStepEnum.WaitForProductGroupInstructionsDuringOrder:
                    instructionListId = BasicService.ProductGroupsService.ProductGroup.Loaded.InstructionListPeriodicIdBefore;
                    break;
                case OrderStepEnum.WaitForProductGroupInstructionsAfterOrder:
                    instructionListId = BasicService.ProductGroupsService.ProductGroup.Loaded.InstructionListIdAfter;
                    break;
                case OrderStepEnum.WaitForProductInstructionsBeforeOrder:
                    instructionListId = BasicService.ProductsService.Product.Loaded.InstructionListIdBefore;
                    break;
                case OrderStepEnum.WaitForProductInstructionsDuringOrder:
                    instructionListId = BasicService.ProductsService.Product.Loaded.InstructionListPeriodicIdBefore;
                    break;
                case OrderStepEnum.WaitForProductInstructionsAfterOrder:
                    instructionListId = BasicService.ProductsService.Product.Loaded.InstructionListIdAfter;
                    break;
                default:
                    return false;
            }

            instructionCount = BasicService.InstructionsService.Instructions.GetInstructionCount(instructionListId);
            if (loadedOrder.InstructionSequence < instructionCount)
            {
                loadedOrder.InstructionSequence++;
                BasicService.InstructionsService.Instructions.Selected =
                    BasicService.InstructionsService.Instructions.GetInstructionFromSequence(instructionListId, loadedOrder.InstructionSequence);
                return false;
            }

            BasicService.OrdersService.Order.Loaded.InstructionSequence = 1;
            return true;
        }


        /// <summary>
        /// Load recipe to the machine
        /// Copy the parameters from the recipe to the machine
        /// Add every machine part to this
        /// </summary>
        public void LoadRecipeToMachine()
        {
            RecipeTyp recipe = new RecipeTyp();
            recipe = BasicService.RecipesService.RecipeDetail.Get<RecipeTyp>(BasicService.RecipesService.Recipe.Selected.Id);

            if (recipe != null)
            {
                Machine.Par = recipe.MachinePar;
                Invoer.Par = recipe.InvoerPar;
                Uitvoer.Par = recipe.UitvoerPar;
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
        private InvoerTyp _invoer;
        [ObservableProperty]
        private UitvoerTyp _uitvoer;
        [ObservableProperty]
        private RecipeTyp _recipe;
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
