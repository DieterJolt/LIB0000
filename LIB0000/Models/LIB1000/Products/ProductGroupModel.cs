using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LIB0000
{
    public partial class ProductGroupModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;

        [ObservableProperty]
        public string _name;

        [ObservableProperty]
        public string? _description;

        [ObservableProperty]
        public double _productCode;

        [ObservableProperty]
        public byte[]? _image;

    }
}
