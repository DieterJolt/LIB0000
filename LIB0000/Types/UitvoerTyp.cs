

using System;


namespace LIB0000
{
    public partial class UitvoerTyp : ObservableObject
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
        UitvoerCmdTyp _cmd = new UitvoerCmdTyp();
        [ObservableProperty]
        UitvoerParTyp _par = new UitvoerParTyp();
        [ObservableProperty]
        UitvoerStatTyp _stat = new UitvoerStatTyp();
        [ObservableProperty]
        UitvoerOutTyp _out = new UitvoerOutTyp();
        [ObservableProperty]
        UitvoerInTyp _in = new UitvoerInTyp();
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
    public partial class UitvoerCmdTyp : ObservableObject
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
    public partial class UitvoerParTyp : ObservableObject
    {
        [ObservableProperty]
        int _positieX1;
        [ObservableProperty]
        int _positieY1;
        [ObservableProperty]
        int _positieZ1;

    }
    public partial class UitvoerStatTyp : ObservableObject
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


    }
    public partial class UitvoerOutTyp : ObservableObject
    {
        [ObservableProperty]
        bool _outputExample;

    }
    public partial class UitvoerInTyp : ObservableObject
    {
        [ObservableProperty]
        bool _inputExample;
    }

}


