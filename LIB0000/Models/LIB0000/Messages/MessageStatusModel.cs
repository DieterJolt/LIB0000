using CommunityToolkit.Mvvm.ComponentModel;

namespace LIB0000
{
    public partial class MessageStatusModel : ObservableObject
    {
        [ObservableProperty]
        public MessageType _type;
        [ObservableProperty]
        public string _group;
        [ObservableProperty]
        public string _nr;
        [ObservableProperty]
        public string _messageText;
        [ObservableProperty]
        public string _help;
    }
}
