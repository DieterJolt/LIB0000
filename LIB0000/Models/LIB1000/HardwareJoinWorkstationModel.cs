using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class HardwareTypeJoinHardwareModel : ObservableObject
    {

        [Key]
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _hardwareName;
        [ObservableProperty]
        private string _hardwareDescription;
        [ObservableProperty]
        private int _hardwareTypeId;
        [ObservableProperty]
        private HardwareType _hardwareType;
        [ObservableProperty]
        private string _hardwareTypeName;
        [ObservableProperty]
        private string _hardwareTypeDescription;
        [ObservableProperty]
        private string _hardwareTypeImage;

    }
}
