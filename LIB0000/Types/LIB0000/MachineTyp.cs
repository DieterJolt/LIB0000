namespace LIB0000
{
    public partial class MachineTyp : ObservableObject
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
        MachineCmdTyp _cmd = new MachineCmdTyp();
        [ObservableProperty]
        MachineParTyp _par = new MachineParTyp();
        [ObservableProperty]
        MachineStatTyp _stat = new MachineStatTyp();
        [ObservableProperty]
        MachineOutTyp _out = new MachineOutTyp();
        [ObservableProperty]
        MachineInTyp _in = new MachineInTyp();
        [ObservableProperty]
        MachineErrorTyp _error = new MachineErrorTyp();
        [ObservableProperty]
        MachineProductPar _productPar = new MachineProductPar();
        [ObservableProperty]
        MachineStepEnum _stepCase;
        [ObservableProperty]
        MachineStepEnum _stepBefore;

        #endregion

        public enum MachineStepEnum
        {
            WaitCommunications = 0,
            WaitErrors = 1,
            WaitSettings = 2,
            WaitLogin = 3,
            WaitOrderLoaded = 4,
            WaitOrder = 5,
            WaitLineClearance = 6,
            WaitForStart = 7,
            WaitForStop = 8,
            WaitForReset = 9,

        }

    }
    public partial class MachineCmdTyp : ObservableObject
    {
        [ObservableProperty]
        bool _start;
        [ObservableProperty]
        bool _stop;
        [ObservableProperty]
        bool _reset;
        [ObservableProperty]
        bool _powerOn;
        [ObservableProperty]
        bool _startOrder;
        [ObservableProperty]
        bool _closeOrder;       //Commando voor definitief afsluiten order
        [ObservableProperty]
        bool _orCameraNok;


    }
    public partial class MachineParTyp : ObservableObject
    {
        [ObservableProperty]
        int _timeout = 500;
        [ObservableProperty]
        int _waitTime = 1000;
        [ObservableProperty]
        string _workstationName;
        [ObservableProperty]
        string _languageCode;
        [ObservableProperty]
        WorkstationModel _workstation;
        [ObservableProperty]
        bool _loginViaActiveDirectory;

        //Examples of settings 
        [ObservableProperty]
        string _exampleOfTextbox;
        [ObservableProperty]
        string _exampleOfFilePicker;
        [ObservableProperty]
        string _exampleOfFolderPicker;
        [ObservableProperty]
        string _exampleOfComboBox;
        [ObservableProperty]
        bool _exampleOfToggleSwitch;
        [ObservableProperty]
        double _exampleOfSlider;

    }
    public partial class MachineStatTyp : ObservableObject
    {
        [ObservableProperty]
        bool _conditionsToStartOk;
        [ObservableProperty]
        DateTime _timeStarted;
        [ObservableProperty]
        bool _safetyOk;
        [ObservableProperty]
        bool _airPressureOk;
        [ObservableProperty]
        bool _errorOk;
        [ObservableProperty]
        bool _powerOk;
        [ObservableProperty]
        bool _communicationOk;
        [ObservableProperty]
        bool _settingsOk;
        [ObservableProperty]
        bool _recipeOk;
        [ObservableProperty]
        bool _started;
        [ObservableProperty]
        bool _paused;
        [ObservableProperty]
        bool _stopped;
        [ObservableProperty]
        bool _hardwareCom;
        [ObservableProperty]
        bool _bovenVerlichting;


    }
    public partial class MachineOutTyp : ObservableObject
    {
        [ObservableProperty]
        bool _outputExample;

    }
    public partial class MachineInTyp : ObservableObject
    {
        [ObservableProperty]
        bool _start;
        [ObservableProperty]
        bool _stop;
        [ObservableProperty]
        bool _lightSwitch;
    }

    public partial class MachineErrorTyp : ObservableObject
    {
        [ObservableProperty]
        bool _algemeen;
        [ObservableProperty]
        bool _hardwareComm;
        [ObservableProperty]
        bool _database;
        [ObservableProperty]
        bool _timeoutWorkstation;
        [ObservableProperty]
        bool _timeoutProductgroup;
        [ObservableProperty]
        bool _timeoutProduct;
        [ObservableProperty]
        bool stoppedByCamera;
    }

    public partial class MachineProductPar : ObservableObject
    {
        [ObservableProperty]
        Single _minimum;
        [ObservableProperty]
        Single _maximum;
    }

}
