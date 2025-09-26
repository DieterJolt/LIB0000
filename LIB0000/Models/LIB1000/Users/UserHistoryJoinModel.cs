namespace LIB0000
{
    public partial class UserHistoryJoinModel : ObservableObject
    {

        public int Id { get; set; }

        [ObservableProperty]
        private int _userId;

        [ObservableProperty]
        private string _action;

        [ObservableProperty]
        private DateTime _dateTime;

        [ObservableProperty]
        private string _user;

        [ObservableProperty]
        private int _level;






    }
}
