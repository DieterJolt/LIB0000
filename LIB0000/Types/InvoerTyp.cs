namespace LIB0000
{
    public partial class InvoerTyp : ObservableObject
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
        InvoerCmdTyp _cmd = new InvoerCmdTyp();
        [ObservableProperty]
        InvoerParTyp _par = new InvoerParTyp();
        [ObservableProperty]
        InvoerStatTyp _stat = new InvoerStatTyp();
        [ObservableProperty]
        InvoerOutTyp _out = new InvoerOutTyp();
        [ObservableProperty]
        InvoerInTyp _in = new InvoerInTyp();
        [ObservableProperty]
        PartStepEnum _stepCase;


        #endregion


        public enum PartStepEnum
        {
            WaitCommunications,
            WaitSafety,
            WaitAirPressure,
            WaitErrors,
            WaitSettings,
            WaitRecipe,
            WaitPower,
            WaitForStart,

        }

    }
    public partial class InvoerCmdTyp : ObservableObject
    {
        [ObservableProperty]
        bool _start;
        [ObservableProperty]
        bool _stop;
        [ObservableProperty]
        bool _reset;
        [ObservableProperty]
        bool _powerOn;


    }
    public partial class InvoerParTyp : ObservableObject
    {
        [ObservableProperty]
        int _positieX1;
        [ObservableProperty]
        int _positieY1;
        [ObservableProperty]
        int _positieZ1;

    }
    public partial class InvoerStatTyp : ObservableObject
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
        bool _started = false;
        [ObservableProperty]
        bool _paused;
        [ObservableProperty]
        bool _stopped;


    }
    public partial class InvoerOutTyp : ObservableObject
    {
        [ObservableProperty]
        bool _outputExample;

    }
    public partial class InvoerInTyp : ObservableObject
    {
        [ObservableProperty]
        bool _inputExample;
    }

}
