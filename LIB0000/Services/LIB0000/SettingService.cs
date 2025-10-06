using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection.PortableExecutable;
using Microsoft.IdentityModel.Tokens;

namespace LIB0000
{
    public partial class SettingService : ObservableObject
    {

        #region Commands
        [RelayCommand]
        public void LoadFromDefaultSettings()
        {
            ErrorTextSettings = "";
            try
            {
                using (var context = new LocalDbContext())
                {

                    IQueryable<SettingModel> query = context.SettingsDbSet.AsQueryable();

                    foreach (SettingModel setting in query)
                    {
                        if (setting.Value != setting.DefaultValue)
                        {
                            setting.Value = setting.DefaultValue;
                            setting.ScreenValue = setting.DefaultValue;
                        }
                    }
                    context.SaveChanges();
                }

                GetSettings();
                CheckSavePossible();
            }
            catch { ErrorTextSettings = "Probleem met database tijdens laden default settings"; }
            FilterText = "";
            SetFilter();
        }

        [RelayCommand]
        public void SaveChangedSettings()
        {
            ErrorTextSettings = "";
            try
            {
                // Maak een lijst om alle wijzigingen op te slaan
                List<Tuple<string, HardwareFunction, int, string>> changesToSave = new List<Tuple<string, HardwareFunction, int, string>>();

                using (var context = new LocalDbContext())
                {
                    IQueryable<SettingModel> query = context.SettingsDbSet.AsQueryable();

                    foreach (SettingModel setting in query)
                    {
                        SettingModel? settingChanged = ListSettings.FirstOrDefault(s => s.Nr == setting.Nr && s.HardwareId == setting.HardwareId && s.HardwareFunction == setting.HardwareFunction && s.ScreenValue != setting.Value);

                        if (settingChanged != null)
                        {
                            if (setting.VariableType == VariableType.Double)
                            {
                                settingChanged.ScreenValue = ConvertToSystemDecimal(settingChanged.ScreenValue);
                            }

                            // Voeg de wijziging toe aan de lijst
                            changesToSave.Add(Tuple.Create(settingChanged.Nr.ToString(), (HardwareFunction)settingChanged.HardwareFunction, settingChanged.HardwareId, settingChanged.ScreenValue));

                        }
                    }
                }

                // Sla alle wijzigingen op
                foreach (var change in changesToSave)
                {
                    SetSetting(change.Item1, change.Item2, change.Item3, change.Item4, "USERNAME");
                }

                ListSettings.Where(s => s.ScreenValueMode != ScreenValueMode.None)
                            .ToList()
                            .ForEach(s => s.ScreenValueMode = ScreenValueMode.None);
                GetSettings();
                CheckSavePossible();
            }
            catch { ErrorTextSettings = "Probleem met database tijdens bewaren settings"; }
            FilterText = "";
            SetFilter();

        }


        [RelayCommand]
        public void SaveToDefaultSettings()
        {
            try
            {
                using (var context = new LocalDbContext())
                {

                    IQueryable<SettingModel> query = context.SettingsDbSet.AsQueryable();

                    foreach (SettingModel setting in query)
                    {
                        if (setting.DefaultValue != setting.Value)
                        {
                            setting.DefaultValue = setting.Value;
                        }
                    }
                    context.SaveChanges();
                }

                GetSettings();
                CheckSavePossible();
            }
            catch { ErrorTextSettings = "Probleem met database tijdens bewaren default settings"; }
            FilterText = "";
            SetFilter();
        }

        #endregion
        #region Constructor
        public SettingService()
        {

        }

        #endregion
        #region Events        
        #endregion
        #region Fields

        public BasicService BasicService { get; set; }

        #endregion
        #region Methods
        public bool AddToSettingsDatabase(List<SettingModel> lSettings, int hardwareId, string displayName)
        {
            bool result = true;

            using (var context = new LocalDbContext())
            {

                foreach (var setting in lSettings)
                {
                    SettingModel set = new SettingModel();
                    try
                    // try to get the setting from the database
                    {
                        set = context.SettingsDbSet.FirstOrDefault(e => e.Nr == setting.Nr && e.HardwareFunction == setting.HardwareFunction && e.HardwareId == hardwareId);
                    }
                    catch { result = false; return result; }

                    // setting does not exist, create it
                    if (set == null)
                    {
                        setting.Value = setting.DefaultValue;
                        setting.ScreenValue = setting.DefaultValue;
                        setting.HardwareId = hardwareId;
                        setting.HardwareFunction = setting.HardwareFunction;

                        context.SettingsDbSet.Add(setting);
                    }
                    // setting exists, update it
                    else
                    {
                        set.MinValue = setting.MinValue;
                        set.MaxValue = setting.MaxValue;
                        set.SettingName = setting.SettingName;
                        set.SettingText = setting.SettingText;
                        set.VariableType = setting.VariableType;
                        set.ControlType = setting.ControlType;
                    }
                }
                RemoveHistorySettings(max: 1000);
                context.SaveChanges();
            }
            CreateListOfGroups(lSettings.FirstOrDefault(), hardwareId, displayName);
            GetSettingsHistory();
            GetSettings();
            return result;
        }


        public void CreateListOfGroups(SettingModel settingModel, int hardwareId, string displayName)
        {
            if (!HardwareFunctionBasicListSettings.Any(s => s.HardwareFunction == settingModel.HardwareFunction && s.HardwareId == hardwareId))
            {
                SettingHardwareFunctionModel settingHardwareFunctionModel = new SettingHardwareFunctionModel
                {
                    HardwareFunction = settingModel.HardwareFunction,
                    HardwareId = hardwareId,
                    DisplayName = displayName,
                };

                //Indien er nog geen is toegevoegd, selecteer deze om te tonen bij de eerste keer navigeren naar pagina
                if (HardwareFunctionBasicListSettings.Count == 0)
                {
                    SelectedHardwareFunctionBasicSettingFilter = settingHardwareFunctionModel;
                }
                HardwareFunctionBasicListSettings.Add(settingHardwareFunctionModel);
            }
        }

        public HardwareType GetHardwareType(int hardwareId)
        {
            using (var context = new LocalDbContext())
            {
                return context.HardwareDbSet
                             .Where(hardware => hardware.Id == hardwareId)
                             .Select(hardware => hardware.HardwareType)
                             .FirstOrDefault();
            }
        }

        public void GetSettings()
        {
            using (var context = new LocalDbContext())
            {
                var result = from settings in context.SettingsDbSet
                             orderby settings.HardwareFunction
                             orderby settings.Nr
                             select settings;

                ListSettings = new ObservableCollection<SettingModel>(result);
            }
        }
        public void GetSettingsHistory()
        {
            ObservableCollection<SettingHistoryJoinModel> listCollection = new ObservableCollection<SettingHistoryJoinModel>();

            if (FilterHistory == "")
            {
                using (var context = new LocalDbContext())
                {
                    var result = from settingHistoryLine in context.SettingsHistoryDbSet
                                 join setting in context.SettingsDbSet
                                 on new { Nr = settingHistoryLine.Nr, HardwareFunction = settingHistoryLine.HardwareFunction } equals new { Nr = setting.Nr, HardwareFunction = setting.HardwareFunction }
                                 orderby settingHistoryLine.TimeStamp descending
                                 select new SettingHistoryJoinModel
                                 {
                                     Id = settingHistoryLine.Id,
                                     Nr = setting.Nr,
                                     HardwareFunction = setting.HardwareFunction,
                                     SettingName = setting.SettingName,
                                     PreviousValue = settingHistoryLine.PreviousValue,
                                     ActualValue = settingHistoryLine.ActualValue,
                                     User = settingHistoryLine.User,
                                     ExtraInfo = settingHistoryLine.ExtraInfo,
                                     TimeStamp = settingHistoryLine.TimeStamp
                                 };

                    listCollection = new ObservableCollection<SettingHistoryJoinModel>(result.Cast<SettingHistoryJoinModel>());


                }
            }
            else
            {
                using (var context = new LocalDbContext())
                {
                    var result = from settingHistoryLine in context.SettingsHistoryDbSet
                                 join setting in context.SettingsDbSet
                                 on new { Nr = settingHistoryLine.Nr, HardwareFunction = settingHistoryLine.HardwareFunction } equals new { Nr = setting.Nr, HardwareFunction = setting.HardwareFunction }
                                 where setting.SettingName.Contains(FilterHistory) ||
                                       setting.SettingText.Contains(FilterHistory) ||
                                       setting.Nr.Contains(FilterHistory)
                                 orderby settingHistoryLine.TimeStamp descending
                                 select new SettingHistoryJoinModel
                                 {
                                     Id = settingHistoryLine.Id,
                                     Nr = setting.Nr,
                                     HardwareFunction = setting.HardwareFunction,
                                     SettingName = setting.SettingName,
                                     PreviousValue = settingHistoryLine.PreviousValue,
                                     ActualValue = settingHistoryLine.ActualValue,
                                     User = settingHistoryLine.User,
                                     ExtraInfo = settingHistoryLine.ExtraInfo,
                                     TimeStamp = settingHistoryLine.TimeStamp
                                 };

                    listCollection = new ObservableCollection<SettingHistoryJoinModel>(result.Cast<SettingHistoryJoinModel>());


                }
            }
            listCollection = FilterSettingsHistory(listCollection);
            ListHistorySettings = listCollection;
        }

        public string GetSetting(string nr, int hardwareId, HardwareFunction hardwareFunction)
        {
            SettingModel s = ListSettings.FirstOrDefault(s => s.Nr == nr && s.HardwareFunction == hardwareFunction && s.HardwareId == hardwareId);

            if (s is SettingModel)
            {
                return s.Value;
            }
            return null;
        }

        public void SetSetting(string nr, HardwareFunction hardwareFunction, int hardwareID, string value, string user)
        {
            DateTime now = DateTime.Now;
            using (var context = new LocalDbContext())
            {
                SettingModel? settingToSet = context.SettingsDbSet.FirstOrDefault(e => e.Nr == nr && e.HardwareFunction == hardwareFunction && e.HardwareId == hardwareID);
                if (settingToSet == null)
                {
                    context.SettingsHistoryDbSet.Add(new SettingHistoryLineModel { Nr = nr, HardwareFunction = hardwareFunction, HardwareId = hardwareID, TimeStamp = now, ActualValue = value, PreviousValue = "", User = user, ExtraInfo = "Setting niet gevonden" });
                }
                else
                {
                    context.SettingsHistoryDbSet.Add(new SettingHistoryLineModel { Nr = nr, HardwareFunction = hardwareFunction, HardwareId = hardwareID, TimeStamp = now, ActualValue = value, PreviousValue = settingToSet.Value, User = user, ExtraInfo = "Setting gewijzigd" });
                    settingToSet.Value = value;
                    settingToSet.ScreenValue = value;
                }
                context.SaveChanges();
            }

            GetSettingsHistory();
            GetSettings();
            string languageCode = GetSetting("003", 0, HardwareFunction.None).ToString();
            (Application.Current as App)?.ChangeLanguage(languageCode);
        }


        public void UpdateUISettings()
        {
            try
            {
                foreach (SettingModel setting in ListSettings)
                {
                    setting.ScreenValueMode = ScreenValueMode.None;

                    //Controle indien waarde gewijzigd
                    if (setting.ScreenValue != setting.Value)
                    {
                        setting.ScreenValueMode = ScreenValueMode.Changed;
                    }

                    //Controle indien waarde kleiner dan minimumwaarde
                    if (!string.IsNullOrEmpty((setting.MinValue)))
                    {
                        switch (setting.VariableType)
                        {
                            case VariableType.Double:
                                {
                                    if (Convert.ToDouble(ConvertToSystemDecimal(setting.ScreenValue)) < Convert.ToDouble(ConvertToSystemDecimal(setting.MinValue)))
                                        setting.ScreenValueMode = ScreenValueMode.BelowMinValue;
                                }
                                break;
                            case VariableType.Int:
                                {
                                    if ((Convert.ToDouble(ConvertToSystemDecimal(setting.ScreenValue)) < -2147483648) || (Convert.ToInt32(ConvertToSystemDecimal(setting.ScreenValue)) < Convert.ToInt32(ConvertToSystemDecimal(setting.MinValue))))
                                        setting.ScreenValueMode = ScreenValueMode.BelowMinValue;
                                }
                                break;
                        }
                    }

                    //Controle indien waarde groter dan maximumwaarde
                    if (!string.IsNullOrEmpty((setting.MaxValue)))
                    {
                        switch (setting.VariableType)
                        {
                            case VariableType.Double:
                                {
                                    if (Convert.ToDouble(ConvertToSystemDecimal(setting.ScreenValue)) > Convert.ToDouble(ConvertToSystemDecimal(setting.MaxValue)))
                                        setting.ScreenValueMode = ScreenValueMode.AboveMaxValue;
                                }
                                break;
                            case VariableType.Int:
                                {

                                    if ((Convert.ToDouble(ConvertToSystemDecimal(setting.ScreenValue)) > 2147483647) || Convert.ToInt32(ConvertToSystemDecimal(setting.ScreenValue)) > Convert.ToInt32(ConvertToSystemDecimal(setting.MaxValue)))
                                        setting.ScreenValueMode = ScreenValueMode.AboveMaxValue;
                                }
                                break;
                        }
                    }
                }

                CheckSavePossible();
            }
            catch { }

        }

        public void CheckSavePossible()
        {
            SaveSettingsPossible = ListSettings.Any(x => x.ScreenValueMode == ScreenValueMode.Changed);
            SaveSettingsPossible = SaveSettingsPossible && !ListSettings.Any(x => x.ScreenValueMode == ScreenValueMode.BelowMinValue);
            SaveSettingsPossible = SaveSettingsPossible && !ListSettings.Any(x => x.ScreenValueMode == ScreenValueMode.AboveMaxValue);

            LoadOrSaveDefaultSettingsPossible = !ListSettings.Any(x => x.ScreenValueMode == ScreenValueMode.Changed);
            LoadOrSaveDefaultSettingsPossible = LoadOrSaveDefaultSettingsPossible && !ListSettings.Any(x => x.ScreenValueMode == ScreenValueMode.BelowMinValue);
            LoadOrSaveDefaultSettingsPossible = LoadOrSaveDefaultSettingsPossible && !ListSettings.Any(x => x.ScreenValueMode == ScreenValueMode.AboveMaxValue);
            LoadOrSaveDefaultSettingsPossible = LoadOrSaveDefaultSettingsPossible && ListSettings.Any(x => x.Value != x.DefaultValue);

        }

        public void SetFilterHistory()
        {
            FilterHistory = FilterText;
        }

        public void SetFilter()
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                ListSettings.Where(s => s.UserVisible == true).ToList().ForEach(s => s.ScreenVisible = true);
            }
            else
            {
                ListSettings.ToList().ForEach(s => s.ScreenVisible = false);
                ListSettings.Where(s => s.SettingName.ToLower().Contains(FilterText.ToLower()))
                            .ToList()
                            .ForEach(s => s.ScreenVisible = true);
            }
            ListSettings.Where(s => s.HardwareFunction != SelectedHardwareFunctionBasicSettingFilter.HardwareFunction || s.HardwareId != SelectedHardwareFunctionBasicSettingFilter.HardwareId)
                        .ToList()
                        .ForEach(s => s.ScreenVisible = false);
        }

        private ObservableCollection<SettingHistoryJoinModel> FilterSettingsHistory(ObservableCollection<SettingHistoryJoinModel> list)
        {
            ObservableCollection<SettingHistoryJoinModel> result = new ObservableCollection<SettingHistoryJoinModel>();

            result = list;


            result = new ObservableCollection<SettingHistoryJoinModel>(result
               .Where(e => e.HardwareFunction == SelectedGroupFilter));



            if (FilterStartDate != null && FilterEndDate != null)
            {
                result = new ObservableCollection<SettingHistoryJoinModel>(result
                    .Where(e => e.TimeStamp >= FilterStartDate && e.TimeStamp <= FilterEndDate));
            }

            return result;
        }

        private string ConvertToSystemDecimal(string input)
        {
            // Get the decimal separator for the current culture
            string systemDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            // Replace points and commas with the system's decimal separator
            string result = input.Replace(".", systemDecimalSeparator).Replace(",", systemDecimalSeparator);

            return result;
        }


        public void RemoveHistorySettings(int max)
        {
            using (var context = new LocalDbContext())
            {
                context.SettingsHistoryDbSet.OrderBy(e => e.TimeStamp)
                                     .Take(context.SettingsHistoryDbSet.Count() - max)
                                     .ToList()
                                     .ForEach(e => context.SettingsHistoryDbSet.Remove(e));
            }
        }

        #endregion
        #region Properties
        //Aanmaak of update error database
        [ObservableProperty]
        private ObservableCollection<SettingHistoryJoinModel> _listHistorySettings = new ObservableCollection<SettingHistoryJoinModel>();

        [ObservableProperty]
        private ObservableCollection<SettingHistoryJoinModel> _listHistorySettingsFiltered = new ObservableCollection<SettingHistoryJoinModel>();

        [ObservableProperty]
        private ObservableCollection<SettingModel> _listSettings = new ObservableCollection<SettingModel>();

        [ObservableProperty]
        private List<SettingModel> _listSettingsFiltered = new List<SettingModel>();

        [ObservableProperty]
        private string _filterHistory = "";

        [ObservableProperty]
        private string _filterText = "";

        private DateTime _filterStartDate = DateTime.Today.AddDays(-30);
        public DateTime FilterStartDate
        {
            get { return _filterStartDate; }
            set
            {

                if (_filterStartDate != value)
                {
                    _filterStartDate = value;
                    _filterStartDate = new DateTime(_filterStartDate.Year, _filterStartDate.Month, _filterStartDate.Day, 0, 0, 0);
                    OnPropertyChanged(nameof(FilterStartDate));
                    ListHistorySettingsFiltered = FilterSettingsHistory(ListHistorySettings);
                }

            }
        }

        private DateTime _filterEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        public DateTime FilterEndDate
        {
            get { return _filterEndDate; }
            set
            {
                if (_filterEndDate != value)
                {
                    if (_filterEndDate > FilterStartDate)
                    {
                        _filterEndDate = value;
                    }
                    else
                    {
                        _filterEndDate = FilterStartDate.AddDays(1);
                    }
                    _filterEndDate = new DateTime(_filterEndDate.Year, _filterEndDate.Month, _filterEndDate.Day, 23, 59, 59);
                    OnPropertyChanged(nameof(FilterEndDate));
                    ListHistorySettingsFiltered = FilterSettingsHistory(ListHistorySettings);
                }
            }
        }

        [ObservableProperty]
        private bool _saveSettingsPossible = false;

        [ObservableProperty]
        private string _errorTextSettings = "";

        [ObservableProperty]
        private bool _loadOrSaveDefaultSettingsPossible = true;

        // observable property
        private int _selectionOfGroupIndex = 0;
        public int SelectionOfGroupIndex
        {
            get { return _selectionOfGroupIndex; }
            set
            {
                if (_selectionOfGroupIndex != value)
                {
                    _selectionOfGroupIndex = value;
                    OnPropertyChanged(nameof(SelectionOfGroupIndex));
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<SettingModel> _selectionOfGroupOfSettings = new();

        [ObservableProperty]
        private ObservableCollection<SettingHardwareFunctionModel> _hardwareFunctionBasicListSettings = new();

        [ObservableProperty]
        private SettingHardwareFunctionModel _selectedHardwareFunctionBasicSettingFilter = new();


        private HardwareFunction _selectedGroupFilter;
        public HardwareFunction SelectedGroupFilter
        {
            get { return _selectedGroupFilter; }
            set
            {
                _selectedGroupFilter = value;

                GetSettingsHistory();
            }
        }
        #endregion

    }


    public partial class SettingHardwareFunctionModel : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor
        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        #endregion

        #region Properties        
        [ObservableProperty]
        private HardwareFunction _hardwareFunction;

        [ObservableProperty]
        private int _hardwareId;

        [ObservableProperty]
        private string _displayName;

        #endregion

    }

    public enum HardwareType
    {
        None = 0,
        GigeCam = 1,
        FHV7 = 2,
        V430 = 3,
        PLC = 4,
    }

    public enum HardwareFunction
    {
        None = 0,
        MachineParTab1 = 1,
        MachineParTab2 = 2,
        MachineParTab3 = 3,


        // GigeCam functions
        GigeCamShapeSearch = 100,
        GigeCamCounting = 101,

        // FHV7 functions
        Fhv7ShapeSearch = 200,
        Fhv7Barcode = 201,

        // V430 functions
        V430Barcode = 300,

        // PLC functions
        // geen functies
    }
}
