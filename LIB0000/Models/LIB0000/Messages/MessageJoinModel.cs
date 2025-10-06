

namespace LIB0000
{
    public class MessageJoinModel
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Nr { get; set; }
        public string MessageText { get; set; }
        public string Help { get; set; }
        public MessageType Type { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
