using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class RecipeModel : ObservableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string? _description;

        [ObservableProperty]
        private string? _barCode;

        [ObservableProperty]
        private string? _extra1;

        [ObservableProperty]
        private string? _extra2;

        [ObservableProperty]
        private string? _extra3;

        [ObservableProperty]
        private DateTime _dateCreated;

        [ObservableProperty]
        private DateTime _dateModified;

        [ObservableProperty]
        private string? _pathPicture;

    }
}
