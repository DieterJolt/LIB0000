namespace LIB0000
{
    public partial class MessagesViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<object> _listHistory = new();
    }
}
