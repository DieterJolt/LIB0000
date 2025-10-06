using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class ProductHardwareModel : ObservableObject
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
    }
}
