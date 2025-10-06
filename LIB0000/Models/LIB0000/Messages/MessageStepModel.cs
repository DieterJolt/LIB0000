using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class MessageStepModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;
        [ObservableProperty]
        public string _group;
        [ObservableProperty]
        public string _status;
        [ObservableProperty]
        public DateTime _timeStamp;

    }
}
