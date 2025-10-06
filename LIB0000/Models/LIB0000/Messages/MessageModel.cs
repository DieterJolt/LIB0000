using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public class MessageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Group { get; set; }
        public string Nr { get; set; }
        public string MessageText { get; set; }
        public string Help { get; set; }

        // Error / Warning / Info
        public MessageType Type { get; set; }
    }

    public enum MessageType
    {
        Error,
        Warning,
        Info,
        Wait,
        Running
    }


}
