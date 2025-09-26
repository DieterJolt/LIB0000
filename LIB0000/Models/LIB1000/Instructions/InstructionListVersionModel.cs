using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class InstructionListVersionModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _instructionListId;

        [ObservableProperty]
        private DateTime _dateTime = DateTime.Now;

        [ObservableProperty]
        private int _version;

        [ObservableProperty]
        private bool _approved = false;

        [ObservableProperty]
        private DateTime _dateTimeApproved;

    }
}
