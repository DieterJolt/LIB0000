using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class UserPagesModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _pageName;
        [ObservableProperty]
        private string _subPageName;
        [ObservableProperty]
        private int _level;


    }
}
