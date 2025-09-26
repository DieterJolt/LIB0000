using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class InstructionJoinModel : ObservableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _instructionsListDetailModelId;

        [ObservableProperty]
        private int _instructionsListVersionId;

        [ObservableProperty]
        private int _instructionId;

        [ObservableProperty]
        private string _instructionHotspotId;

        [ObservableProperty]
        private string _instructionName;

        [ObservableProperty]
        private string _instructionTypeName;

        [ObservableProperty]
        private string _instructionIcon;

        [ObservableProperty]
        private int _sequence;

    }
}
