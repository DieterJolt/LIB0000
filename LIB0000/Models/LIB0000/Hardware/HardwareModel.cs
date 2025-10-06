using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class HardwareModel : ObservableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private int _hardwareTypeId;

        [ObservableProperty]
        private HardwareType _hardwareType;

        [ObservableProperty]
        private HardwareFunction _loadedFunction;


    }
}
