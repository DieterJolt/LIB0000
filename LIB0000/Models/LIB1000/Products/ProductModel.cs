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
    public partial class ProductModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;

        [ObservableProperty]
        public string _name;

        [ObservableProperty]
        public string? _description;

        [AllowNull]
        [ObservableProperty]
        public int _productGroupId;

        [ObservableProperty]
        public byte[]? _image;

    }
}
