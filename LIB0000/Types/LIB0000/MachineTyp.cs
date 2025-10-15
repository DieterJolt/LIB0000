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
        MachineStatTyp _stat = new MachineStatTyp();
        [ObservableProperty]
        MachineParTyp _par = new MachineParTyp();

        [ObservableProperty]
        double _watchdogPlc;
        [ObservableProperty]
        double _watchdogHmi;
        [ObservableProperty]
        bool _communicationOk;
        [ObservableProperty]
        bool[] _error = new bool[100];
        [ObservableProperty]
        Single[] _errorNr = new Single[100];




        #endregion


        public partial class MachineCmdTyp : ObservableObject
        {
            [ObservableProperty]
            bool _reset;
            [ObservableProperty]
            DateTime _resetDateTime;
            [ObservableProperty]
            bool _start;
            [ObservableProperty]
            bool _stop;


        }
        public partial class MachineStatTyp : ObservableObject
        {
            [ObservableProperty]
            bool _started;
            [ObservableProperty]
            bool _stopped;
            [ObservableProperty]
            bool _resetNeeded;
        }
        public partial class MachineParTyp : ObservableObject
        {
            [ObservableProperty]
            Single _dummy;
        }

    }


}
