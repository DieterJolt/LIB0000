using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class HardwareTypesModel : ObservableObject
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
        private string _image;

        [ObservableProperty]
        HardwareType hardwareType;

    }
}
