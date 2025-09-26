namespace LIB0000
{
    public partial class SettingHistoryJoinModel : ObservableObject
    {

        [ObservableProperty]
        string _nr;

        [ObservableProperty]
        HardwareFunction _hardwareFunction;

        [ObservableProperty]
        string _settingName;

        [ObservableProperty]
        int _id;

        [ObservableProperty]
        DateTime _timeStamp;

        [ObservableProperty]
        string _previousValue;

        [ObservableProperty]
        string _actualValue;

        [ObservableProperty]
        string _user;

        [ObservableProperty]
        string _extraInfo;
    }
}
