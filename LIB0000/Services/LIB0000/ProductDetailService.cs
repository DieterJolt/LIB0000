
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection.PortableExecutable;
using Microsoft.IdentityModel.Tokens;

namespace LIB0000
{
    //public partial class ProductDetailService : ObservableObject
    //{

    //    #region Commands
    //    [RelayCommand]
    //    public void LoadFromDefaultSettings()
    //    {
    //        ErrorTextSettings = "";
    //        try
    //        {
    //            using (var context = new LocalDbContext())
    //            {
    //                foreach (ProductDetailJoinModel productDetailJoinModel in ListProductDetails)
    //                {
    //                    if (productDetailJoinModel.Value != productDetailJoinModel.DefaultValue)
    //                    {
    //                        ProductDetailValueModel? settingToSet = context.ProductDetailValueDbSet.FirstOrDefault(s => s.ProductId == LoadedProduct.Id && s.SettingNr == productDetailJoinModel.Nr && s.HardwareFunction == productDetailJoinModel.HardwareFunction && s.HardwareId == productDetailJoinModel.HardwareId);
    //                        if (settingToSet is ProductDetailValueModel)
    //                        {
    //                            settingToSet.SettingValue = productDetailJoinModel.DefaultValue;
    //                        }
    //                    }
    //                }
    //                context.SaveChanges();
    //            }

    //            GetSettings();
    //            CheckSavePossible();
    //        }
    //        catch { ErrorTextSettings = "Probleem met database tijdens laden default settings"; }
    //        FilterText = "";
    //        SetFilterProduct();
    //    }


    //    [RelayCommand]
    //    public void SaveToDefaultSettings()
    //    {
    //        try
    //        {
    //            using (var context = new LocalDbContext())
    //            {
    //                foreach (ProductDetailJoinModel productDetailJoinModel in ListProductDetails)
    //                {
    //                    if (productDetailJoinModel.DefaultValue != productDetailJoinModel.Value)
    //                    {
    //                        ProductDetailModel? settingToSet = context.ProductDetailsDbSet.FirstOrDefault(s => s.Nr == productDetailJoinModel.Nr && s.HardwareFunction == productDetailJoinModel.HardwareFunction && s.HardwareId == productDetailJoinModel.HardwareId);
    //                        if (settingToSet is ProductDetailModel)
    //                        {
    //                            settingToSet.DefaultValue = productDetailJoinModel.Value;
    //                        }
    //                    }
    //                }
    //                context.SaveChanges();
    //            }

    //            GetSettings();
    //            CheckSavePossible();
    //        }
    //        catch { ErrorTextSettings = "Probleem met database tijdens bewaren default settings"; }
    //        FilterText = "";
    //        SetFilterProduct();
    //    }

    //    [RelayCommand]
    //    public void UpdateUI()
    //    {
    //        UpdateUISettings();
    //    }

    //    #endregion
    //    #region Constructor
    //    public ProductDetailService(string databasePath)
    //    {
    //        DatabasePath = databasePath;
    //    }

    //    #endregion
    //    #region Events        
    //    #endregion
    //    #region Fields        
    //    #endregion
    //    #region Methods

    //    public bool AddDefaultSettingsToProductDetailDatabase(List<ProductDetailModel> listProductDetails, int hardwareId, string displayName)
    //    {
    //        bool result = true;

    //        if (listProductDetails != null)
    //        {
    //            using (var context = new LocalDbContext())
    //            {
    //                foreach (var pSetting in listProductDetails)
    //                {
    //                    ProductDetailModel productDetail = new ProductDetailModel();
    //                    try
    //                    // try to get the setting from the database
    //                    {
    //                        productDetail = context.ProductDetailsDbSet.FirstOrDefault(e => e.Nr == pSetting.Nr && e.HardwareFunction == pSetting.HardwareFunction && e.HardwareId == hardwareId);
    //                    }
    //                    catch { result = false; return result; }

    //                    // setting does not exist, create it
    //                    if (productDetail == null)
    //                    {
    //                        pSetting.HardwareId = hardwareId;
    //                        pSetting.HardwareFunction = pSetting.HardwareFunction;

    //                        context.ProductDetailsDbSet.Add(pSetting);
    //                    }
    //                    // setting exists, update it
    //                    else
    //                    {
    //                        productDetail.MinValue = pSetting.MinValue;
    //                        productDetail.MaxValue = pSetting.MaxValue;
    //                        productDetail.SettingName = pSetting.SettingName;
    //                        productDetail.SettingText = pSetting.SettingText;
    //                        productDetail.VariableType = pSetting.VariableType;
    //                        productDetail.ControlType = pSetting.ControlType;
    //                    }
    //                }
    //                context.SaveChanges();
    //            }
    //        }
    //        return result;
    //    }

    //    public HardwareType GetHardwareType(int hardwareId)
    //    {
    //        using (var context = new LocalDbContext())
    //        {
    //            return context.HardwareDbSet
    //                         .Where(hardware => hardware.Id == hardwareId)
    //                         .Select(hardware => hardware.HardwareType)
    //                         .FirstOrDefault();
    //        }
    //    }

    //    public void GetSettings()
    //    {
    //        using (var context = new LocalDbContext())
    //        {
    //            var result = from settings in context.ProductDetailsDbSet
    //                         join values in context.ProductDetailValueDbSet
    //                         on new { settings.Nr, settings.HardwareFunction, settings.HardwareId }
    //                         equals new { Nr = values.SettingNr, values.HardwareFunction, values.HardwareId }
    //                         into valueGroup
    //                         from value in valueGroup.DefaultIfEmpty() // Left join
    //                         where value != null && value.ProductId == LoadedProduct.Id
    //                         orderby settings.Nr
    //                         select new ProductDetailJoinModel
    //                         {
    //                             Id = settings.Id,
    //                             Nr = settings.Nr,
    //                             HardwareFunction = settings.HardwareFunction,
    //                             HardwareId = settings.HardwareId,
    //                             SettingName = settings.SettingName,
    //                             SettingText = settings.SettingText,
    //                             ScreenVisible = false,
    //                             UserVisible = settings.UserVisible,
    //                             MinValue = settings.MinValue,
    //                             MaxValue = settings.MaxValue,
    //                             DefaultValue = settings.DefaultValue,
    //                             PossibleComboBoxValues = settings.PossibleComboBoxValues,
    //                             VariableType = settings.VariableType,
    //                             ControlType = settings.ControlType,
    //                             ScreenValueMode = ScreenValueMode.None,

    //                             ProductId = value.ProductId,
    //                             Value = value.SettingValue,
    //                             ScreenValue = value.SettingValue
    //                         };

    //            ListProductDetails = new ObservableCollection<ProductDetailJoinModel>(result.ToList());

    //            if ((SelectedHardware == null) || (SelectedHardware.Id == 0))
    //            {
    //                HardwareModel hardware = context.HardwareDbSet.OrderBy(h => h.Id).FirstOrDefault();
    //                if (hardware != null)
    //                {
    //                    SelectedHardware = hardware;
    //                }
    //            }
    //        }
    //    }

    //    public string GetSetting(string nr, int hardwareId, HardwareFunction hardwareFunction)
    //    {
    //        ProductDetailJoinModel productDetailValueModel = ListProductDetails.FirstOrDefault(p => p.ProductId == LoadedProduct.Id && p.Nr == nr && p.HardwareId == hardwareId && p.HardwareFunction == hardwareFunction);

    //        if (productDetailValueModel is ProductDetailJoinModel)
    //        {
    //            return productDetailValueModel.Value;
    //        }
    //        return null;
    //    }

    //    public void LoadProductDetails(int productId)
    //    {
    //        using (var context = new ServerDbContext(DatabasePath))
    //        {
    //            ProductModel product = context.ProductDbSet.FirstOrDefault(p => p.Id == productId);

    //            if (product is ProductModel)
    //            {
    //                LoadedProduct = product;
    //                GetSettings();
    //            }
    //        }
    //    }

    //    public void SaveChangedSettings()
    //    {
    //        ErrorTextSettings = "";
    //        try
    //        {
    //            // Maak een lijst om alle wijzigingen op te slaan
    //            List<Tuple<string, HardwareFunction, int, string>> changesToSave = new List<Tuple<string, HardwareFunction, int, string>>();

    //            using (var context = new LocalDbContext())
    //            {
    //                IQueryable<ProductDetailValueModel> query = context.ProductDetailValueDbSet.Where(p => p.ProductId == LoadedProduct.Id);

    //                foreach (ProductDetailValueModel productDetailValue in query)
    //                {
    //                    ProductDetailJoinModel? settingChanged = ListProductDetails.FirstOrDefault(s => s.ProductId == productDetailValue.ProductId && s.HardwareId == productDetailValue.HardwareId && s.HardwareFunction == productDetailValue.HardwareFunction && s.Nr == productDetailValue.SettingNr && s.ScreenValue != productDetailValue.SettingValue);

    //                    if (settingChanged != null)
    //                    {
    //                        if (settingChanged.VariableType == VariableType.Double)
    //                        {
    //                            settingChanged.ScreenValue = ConvertToSystemDecimal(settingChanged.ScreenValue);
    //                        }

    //                        // Voeg de wijziging toe aan de lijst
    //                        changesToSave.Add(Tuple.Create(settingChanged.Nr.ToString(), (HardwareFunction)settingChanged.HardwareFunction, settingChanged.HardwareId, settingChanged.ScreenValue));
    //                    }
    //                }
    //            }

    //            // Sla alle wijzigingen op
    //            foreach (var change in changesToSave)
    //            {
    //                SetSetting(change.Item1, change.Item2, change.Item3, change.Item4, "USERNAME");
    //            }

    //            ListProductDetails.Where(s => s.ScreenValueMode != ScreenValueMode.None)
    //                        .ToList()
    //                        .ForEach(s => s.ScreenValueMode = ScreenValueMode.None);
    //            GetSettings();
    //            CheckSavePossible();
    //        }
    //        catch { ErrorTextSettings = "Probleem met database tijdens bewaren settings"; }
    //        FilterText = "";
    //        SetFilterProduct();
    //        UpdateUISettings();
    //    }

    //    public void SetSetting(string nr, HardwareFunction hardwareFunction, int hardwareID, string value, string user)
    //    {
    //        DateTime now = DateTime.Now;
    //        using (var context = new LocalDbContext())
    //        {
    //            ProductDetailValueModel? settingToSet = context.ProductDetailValueDbSet.FirstOrDefault(e => e.ProductId == LoadedProduct.Id && e.SettingNr == nr && e.HardwareFunction == hardwareFunction && e.HardwareId == hardwareID);
    //            if (settingToSet == null)
    //            {
    //                //context.SettingsHistoryDbSet.Add(new SettingHistoryLineModel { Nr = nr, HardwareFunction = hardwareFunction, HardwareId = hardwareID, TimeStamp = now, ActualValue = value, PreviousValue = "", User = user, ExtraInfo = "Setting niet gevonden" });
    //            }
    //            else
    //            {
    //                //context.SettingsHistoryDbSet.Add(new SettingHistoryLineModel { Nr = nr, HardwareFunction = hardwareFunction, HardwareId = hardwareID, TimeStamp = now, ActualValue = value, PreviousValue = settingToSet.Value, User = user, ExtraInfo = "Setting gewijzigd" });
    //                settingToSet.SettingValue = value;
    //            }
    //            context.SaveChanges();
    //        }
    //        //GetSettingsHistory();
    //        GetSettings();
    //    }

    //    public void UpdateUISettings()
    //    {
    //        try
    //        {
    //            foreach (ProductDetailJoinModel productDetailJoin in ListProductDetails)
    //            {
    //                productDetailJoin.ScreenValueMode = ScreenValueMode.None;

    //                //Controle indien waarde gewijzigd
    //                if (productDetailJoin.ScreenValue != productDetailJoin.Value)
    //                {
    //                    productDetailJoin.ScreenValueMode = ScreenValueMode.Changed;
    //                }

    //                //Controle indien waarde kleiner dan minimumwaarde
    //                if (!string.IsNullOrEmpty((productDetailJoin.MinValue)))
    //                {
    //                    switch (productDetailJoin.VariableType)
    //                    {
    //                        case VariableType.Double:
    //                            {
    //                                if (Convert.ToDouble(ConvertToSystemDecimal(productDetailJoin.ScreenValue)) < Convert.ToDouble(ConvertToSystemDecimal(productDetailJoin.MinValue)))
    //                                    productDetailJoin.ScreenValueMode = ScreenValueMode.BelowMinValue;
    //                            }
    //                            break;
    //                        case VariableType.Int:
    //                            {
    //                                if (Convert.ToInt32(ConvertToSystemDecimal(productDetailJoin.ScreenValue)) < Convert.ToInt32(ConvertToSystemDecimal(productDetailJoin.MinValue)))
    //                                    productDetailJoin.ScreenValueMode = ScreenValueMode.BelowMinValue;
    //                            }
    //                            break;
    //                    }
    //                }

    //                //Controle indien waarde groter dan maximumwaarde
    //                if (!string.IsNullOrEmpty((productDetailJoin.MaxValue)))
    //                {
    //                    switch (productDetailJoin.VariableType)
    //                    {
    //                        case VariableType.Double:
    //                            {
    //                                if (Convert.ToDouble(ConvertToSystemDecimal(productDetailJoin.ScreenValue)) > Convert.ToDouble(ConvertToSystemDecimal(productDetailJoin.MaxValue)))
    //                                    productDetailJoin.ScreenValueMode = ScreenValueMode.AboveMaxValue;
    //                            }
    //                            break;
    //                        case VariableType.Int:
    //                            {
    //                                if (Convert.ToInt32(ConvertToSystemDecimal(productDetailJoin.ScreenValue)) > Convert.ToInt32(ConvertToSystemDecimal(productDetailJoin.MaxValue)))
    //                                    productDetailJoin.ScreenValueMode = ScreenValueMode.AboveMaxValue;
    //                            }
    //                            break;
    //                    }
    //                }
    //            }

    //            CheckSavePossible();
    //        }
    //        catch { }
    //    }

    //    public void CheckSavePossible()
    //    {
    //        SaveSettingsPossible = ListProductDetails.Any(x => x.ScreenValueMode == ScreenValueMode.Changed);
    //        SaveSettingsPossible = SaveSettingsPossible && !ListProductDetails.Any(x => x.ScreenValueMode == ScreenValueMode.BelowMinValue);
    //        SaveSettingsPossible = SaveSettingsPossible && !ListProductDetails.Any(x => x.ScreenValueMode == ScreenValueMode.AboveMaxValue);

    //        LoadOrSaveDefaultSettingsPossible = !ListProductDetails.Any(x => x.ScreenValueMode == ScreenValueMode.Changed);
    //        LoadOrSaveDefaultSettingsPossible = LoadOrSaveDefaultSettingsPossible && !ListProductDetails.Any(x => x.ScreenValueMode == ScreenValueMode.BelowMinValue);
    //        LoadOrSaveDefaultSettingsPossible = LoadOrSaveDefaultSettingsPossible && !ListProductDetails.Any(x => x.ScreenValueMode == ScreenValueMode.AboveMaxValue);
    //        LoadOrSaveDefaultSettingsPossible = LoadOrSaveDefaultSettingsPossible && ListProductDetails.Any(x => x.Value != x.DefaultValue);

    //    }
    //    public void SetFilterProduct()
    //    {
    //        if (string.IsNullOrEmpty(FilterText))
    //        {
    //            ListProductDetails.Where(s => s.UserVisible == true).ToList().ForEach(s => s.ScreenVisible = true);
    //        }
    //        else
    //        {
    //            ListProductDetails.ToList().ForEach(s => s.ScreenVisible = false);
    //            ListProductDetails.Where(s => s.SettingName.ToLower().Contains(FilterText.ToLower()))
    //                        .ToList()
    //                        .ForEach(s => s.ScreenVisible = true);
    //        }
    //        ListProductDetails.Where(s => s.HardwareId != SelectedHardware.Id || s.HardwareFunction != SelectedHardwareFunction)
    //                    .ToList()
    //                    .ForEach(s => s.ScreenVisible = false);
    //    }

    //    private string ConvertToSystemDecimal(string input)
    //    {
    //        // Get the decimal separator for the current culture
    //        string systemDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

    //        // Replace points and commas with the system's decimal separator
    //        string result = input.Replace(".", systemDecimalSeparator).Replace(",", systemDecimalSeparator);

    //        return result;
    //    }

    //    public void ChangeHardwareFunction()
    //    {
    //        ObservableCollection<HardwareFunction> hardwareFunctionList = HardwareFunctionMapper.GetFunctionsForHardware(SelectedHardware.HardwareType);

    //        if (hardwareFunctionList == null || hardwareFunctionList.Count == 0) return;

    //        int currentIndex = hardwareFunctionList.IndexOf(SelectedHardwareFunction);

    //        int nextIndex = (currentIndex + 1) % hardwareFunctionList.Count;

    //        HardwareFunction newHardwareFunction = hardwareFunctionList[nextIndex];

    //        using (var context = new LocalDbContext())
    //        {
    //            // Loaded function van de juiste hardware in producthardware aanpassen
    //            ProductHardwareModel? productHardwareModel = context.ProductHardwareDbSet.FirstOrDefault(pH => pH.ProductId == LoadedProduct.Id && pH.HardwareId == SelectedHardware.Id);

    //            if (productHardwareModel is ProductHardwareModel)
    //            {
    //                productHardwareModel.HardwareFunction = newHardwareFunction;
    //                context.SaveChanges();
    //                SelectedHardware.LoadedFunction = newHardwareFunction;
    //            }

    //            // Loaded function van de juiste hardware aanpassen                                            
    //            HardwareModel? hardwareModel = context.HardwareDbSet.FirstOrDefault(h => h.Id == SelectedHardware.Id);
    //            if (hardwareModel is HardwareModel)
    //            {
    //                hardwareModel.LoadedFunction = newHardwareFunction;
    //                context.SaveChanges();
    //                SelectedHardware.LoadedFunction = newHardwareFunction;
    //            }
    //        }

    //        SelectedHardwareFunction = newHardwareFunction;

    //        GetSettings();
    //        SetFilterProduct();

    //        UpdateUISettings();
    //    }

    //    public void ChangeSelectedHardware(HardwareModel selectedHardware)
    //    {
    //        if (selectedHardware != null)
    //        {
    //            SelectedHardware = selectedHardware;
    //            SetFilterProduct();
    //            UpdateUISettings();
    //        }
    //    }

    //    public void setHardwareFunctions()
    //    {
    //        using (var context = new LocalDbContext())
    //        {
    //            foreach (ProductHardwareModel productHardware in context.ProductHardwareDbSet.Where(p => p.ProductId == LoadedProduct.Id))
    //            {
    //                HardwareModel hardwareModel = context.HardwareDbSet.Where(h => h.Id == productHardware.HardwareId).FirstOrDefault();
    //                if (hardwareModel is HardwareModel)
    //                {
    //                    hardwareModel.LoadedFunction = productHardware.HardwareFunction;
    //                }
    //            }
    //            SelectedHardware = context.HardwareDbSet.OrderBy(h => h.Id).FirstOrDefault();
    //            context.SaveChanges();
    //        }
    //        GetSettings();
    //    }

    //    public void updateSelectedHardwareFunction()
    //    {
    //        if (SelectedHardware != null)
    //        {
    //            HardwareFunctionsList = HardwareFunctionMapper.GetFunctionsForHardware(SelectedHardware.HardwareType);

    //            using (var context = new LocalDbContext())
    //            {
    //                HardwareModel hardwareModel = context.HardwareDbSet.Where(h => h.Id == SelectedHardware.Id).FirstOrDefault();

    //                if (hardwareModel is HardwareModel)
    //                {
    //                    SelectedHardwareFunction = hardwareModel.LoadedFunction;
    //                }
    //            }
    //            GetSettings();
    //        }

    //    }

    //    #endregion
    //    #region Properties

    //    [ObservableProperty]
    //    private string _databasePath;

    //    private ProductModel _loadedProduct = new();

    //    public ProductModel LoadedProduct
    //    {
    //        get { return _loadedProduct; }
    //        set
    //        {
    //            _loadedProduct = value;
    //            OnPropertyChanged(nameof(LoadedProduct));
    //            setHardwareFunctions();

    //        }
    //    }

    //    [ObservableProperty]
    //    private ObservableCollection<ProductDetailJoinModel> _listProductDetails = new();

    //    [ObservableProperty]
    //    private string _filterText = "";

    //    [ObservableProperty]
    //    private bool _saveSettingsPossible = false;

    //    [ObservableProperty]
    //    private string _errorTextSettings = "";

    //    [ObservableProperty]
    //    private bool _loadOrSaveDefaultSettingsPossible = true;

    //    [ObservableProperty]
    //    private ObservableCollection<HardwareFunction> _hardwareFunctionsList = new ObservableCollection<HardwareFunction>();

    //    private HardwareModel _selectedHardware = new();
    //    public HardwareModel SelectedHardware
    //    {
    //        get { return _selectedHardware; }
    //        set
    //        {
    //            if (_selectedHardware != value)
    //            {
    //                _selectedHardware = value;
    //                OnPropertyChanged(nameof(SelectedHardware));
    //                updateSelectedHardwareFunction();
    //            }
    //        }
    //    }

    //    private HardwareFunction _selectedHardwareFunction = HardwareFunction.None;

    //    public HardwareFunction SelectedHardwareFunction
    //    {
    //        get { return _selectedHardwareFunction; }
    //        set
    //        {
    //            if (_selectedHardwareFunction != value)
    //            {
    //                _selectedHardwareFunction = value;
    //                OnPropertyChanged(nameof(SelectedHardwareFunction));
    //            }
    //        }
    //    }

    //    #endregion
    //}
}
