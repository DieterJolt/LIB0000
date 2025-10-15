using System.Collections.ObjectModel;

namespace LIB0000
{
    public partial class HmiTyp : ObservableObject
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
        HmiCmdTyp _cmd = new HmiCmdTyp();
        [ObservableProperty]
        HmiParTyp _par = new HmiParTyp();
        [ObservableProperty]
        HmiStatTyp _stat = new HmiStatTyp();
        [ObservableProperty]
        HmiOutTyp _out = new HmiOutTyp();
        [ObservableProperty]
        HmiInTyp _in = new HmiInTyp();
        [ObservableProperty]
        HmiErrorTyp _error = new HmiErrorTyp();
        [ObservableProperty]
        HmiStepEnum _stepCase;
        [ObservableProperty]
        HmiStepEnum _stepBefore;

        #endregion

        public enum HmiStepEnum
        {
            WaitCommunications = 0,
            WaitErrors = 1,
            WaitSettings = 2,
            WaitLogin = 3,
            WaitOrderLoaded = 4,
            WaitOrder = 5,
            WaitForStart = 7,
            WaitForStop = 8,
            WaitForReset = 9,

        }

    }
    public partial class HmiCmdTyp : ObservableObject
    {
        [ObservableProperty]
        bool _start;
        [ObservableProperty]
        bool _stop;
        [ObservableProperty]
        bool _reset;
        [ObservableProperty]
        bool _startOrder;
        [ObservableProperty]
        bool _closeOrder;       //Commando voor definitief afsluiten order

    }
    public partial class HmiParTyp : ObservableObject
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
    public partial class HmiStatTyp : ObservableObject
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



    }
    public partial class HmiOutTyp : ObservableObject
    {
        [ObservableProperty]
        bool _outputExample;

    }
    public partial class HmiInTyp : ObservableObject
    {
        [ObservableProperty]
        bool _start;
        [ObservableProperty]
        bool _stop;
    }
    public partial class HmiErrorTyp : ObservableObject
    {
        [ObservableProperty]
        bool _algemeen;
        [ObservableProperty]
        bool _hardwareComm;
        [ObservableProperty]
        bool _database;
    }




    
}
