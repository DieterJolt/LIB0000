using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class OrderModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _orderNr;
        [ObservableProperty]
        private int? _workstationId;
        [ObservableProperty]
        private int? _productGroupId;
        [ObservableProperty]
        private int _productId;
        [ObservableProperty]
        private int _instructionSequence;
        [ObservableProperty]
        private int _amount;
        [ObservableProperty]
        private int _totalProduct;
        [ObservableProperty]
        private int _goodProduct;
        [ObservableProperty]
        private int _badProduct;
        [ObservableProperty]
        private int _machineStopAmount;
        [ObservableProperty]
        private int _userId;
        [ObservableProperty]
        private string? _extra1;
        [ObservableProperty]
        private string? _extra2;
        [ObservableProperty]
        private string? _extra3;
        [ObservableProperty]
        private DateTime _dateTimeStart;
        [ObservableProperty]
        private DateTime? _dateTimeStop;
        [ObservableProperty]
        private OrderStepEnum _orderStep;
        [ObservableProperty]
        private OrderStepEnum _orderStepBefore;
    }

    public enum OrderStepEnum
    {
        WaitOrderDetails,
        WaitForMachineInstructionsBeforeOrder,
        WaitForProductGroupInstructionsBeforeOrder,
        WaitForProductInstructionsBeforeOrder,
        WaitForStart,
        WaitForStop,
        WaitForMachineInstructionsDuringOrder,
        WaitForProductGroupInstructionsDuringOrder,
        WaitForProductInstructionsDuringOrder,
        WaitForMachineInstructionsAfterOrder,
        WaitForProductGroupInstructionsAfterOrder,
        WaitForProductInstructionsAfterOrder,
    }
}
