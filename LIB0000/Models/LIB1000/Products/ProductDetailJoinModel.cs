using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CommunityToolkit.Mvvm.ComponentModel;

namespace LIB0000
{
    public partial class ProductDetailJoinModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _productId;

        [ObservableProperty]
        private string _nr;

        [ObservableProperty]
        private HardwareFunction _hardwareFunction;

        [ObservableProperty]
        private int _hardwareId;

        [ObservableProperty]
        private string _settingName;

        [ObservableProperty]
        private string _settingText;

        [ObservableProperty]
        private string _value;

        [ObservableProperty]
        private string _screenValue;

        [ObservableProperty]
        private bool _screenVisible;

        [ObservableProperty]
        private bool _userVisible;

        [ObservableProperty]
        private string _minValue;

        [ObservableProperty]
        private string _maxValue;

        [ObservableProperty]
        private string _defaultValue;

        // BVB "KEUZE1|KEUZE2|KEUZE3
        [ObservableProperty]
        private string _possibleComboBoxValues;

        [ObservableProperty]
        private VariableType _variableType;

        [ObservableProperty]
        public ControlType _controlType;

        [ObservableProperty]
        public ScreenValueMode _screenValueMode = ScreenValueMode.None;
    }

}