using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class UserHistoryModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;
        [ObservableProperty]
        public DateTime _dateTime;
        [ObservableProperty]
        public string _action;
        [ObservableProperty]
        public int _userId;


    }
}
