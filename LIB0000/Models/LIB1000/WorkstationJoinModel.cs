using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class WorkstationJoinModel : ObservableObject
    {
        [Key]
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _workstationName;
        [ObservableProperty]
        private string _workstationDescription;
        [ObservableProperty]
        public bool _workstationInstructionListOn;
        [AllowNull]
        [ObservableProperty]
        public int? _workstationInstructionListIdBefore;
        [AllowNull]
        [ObservableProperty]
        public int? _workstationInstructionListPeriodicIdBefore;
        [AllowNull]
        [ObservableProperty]
        public int? _workstationInstructionListIdAfter;
        [ObservableProperty]
        public int _workstationInstructionListPeriodicFrequency;
        [ObservableProperty]
        public byte[]? _workstationImage;
        [ObservableProperty]
        private string _instructionListName;
    }
}
