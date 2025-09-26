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
    public partial class WorkstationModel : ObservableObject
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
        public bool _instructionListOn;

        [AllowNull]
        [ObservableProperty]
        public int? _instructionListIdBefore;

        [AllowNull]
        [ObservableProperty]
        public int? _instructionListPeriodicIdBefore;

        [AllowNull]
        [ObservableProperty]
        public int? _instructionListIdAfter;

        [ObservableProperty]
        public int _instructionListPeriodicFrequency;

        [ObservableProperty]
        public byte[]? _image;


    }
}
