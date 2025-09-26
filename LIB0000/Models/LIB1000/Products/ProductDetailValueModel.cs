using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class ProductDetailValueModel : ObservableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;

        [ObservableProperty]
        public int _productId;

        [ObservableProperty]
        public int _hardwareId;

        [ObservableProperty]
        public HardwareFunction _hardwareFunction;

        [ObservableProperty]
        public string _settingNr;

        [ObservableProperty]
        public string _settingValue;

    }
}
