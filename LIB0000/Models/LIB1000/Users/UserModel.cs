using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class UserModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;
        [ObservableProperty]
        public string _user;
        [ObservableProperty]
        public string? _loginName;
        [ObservableProperty]
        public string _password;
        [ObservableProperty]
        public int _level;
        [ObservableProperty]
        public bool _passwordRequired;
        [ObservableProperty]
        public string _group;

    }
}

