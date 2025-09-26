using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class InstructionModel : ObservableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _instructionListVersionId;

        [ObservableProperty]
        private int _sequence;

        [ObservableProperty]
        private InstructionType _instructionType;

        [ObservableProperty]
        private string _instructionTypeName;

        [ObservableProperty]
        private string _hotspotId;

        [ObservableProperty]
        private string? _inputText1;

        [ObservableProperty]
        private string? _inputText2;

        [ObservableProperty]
        private string? _inputText3;

        [ObservableProperty]
        private string? _inputText4;

        [ObservableProperty]
        private string? _inputText5;

        [ObservableProperty]
        private string? _inputText6;

        [ObservableProperty]
        private string? _inputText7;

        [ObservableProperty]
        private string? _inputText8;

        [ObservableProperty]
        private string? _inputText9;

        [ObservableProperty]
        private string? _inputText10;

        [ObservableProperty]
        private bool _inputBool1;

        [ObservableProperty]
        private bool _inputBool2;

        [ObservableProperty]
        private bool _inputBool3;

        [ObservableProperty]
        private bool _inputBool4;

        [ObservableProperty]
        private bool _inputBool5;

        [ObservableProperty]
        private bool _inputBool6;

        [ObservableProperty]
        private bool _inputBool7;

        [ObservableProperty]
        private bool _inputBool8;

        [ObservableProperty]
        private bool _inputBool9;

        [ObservableProperty]
        private bool _inputBool10;

        [ObservableProperty]
        private byte[]? _inputImage1;

        [ObservableProperty]
        private string? _text1;

        [ObservableProperty]
        private string? _text2;

        [ObservableProperty]
        private string? _text3;

        [ObservableProperty]
        private string? _text4;

        [ObservableProperty]
        private string? _text5;

        [ObservableProperty]
        private string? _text6;

        [ObservableProperty]
        private string? _text7;

        [ObservableProperty]
        private string? _text8;

        [ObservableProperty]
        private string? _text9;

        [ObservableProperty]
        public string? _text10;

        [ObservableProperty]
        private byte[]? _image1;

        [ObservableProperty]
        private byte[]? _image2;

        [ObservableProperty]
        private byte[]? _image3;

        [ObservableProperty]
        private byte[]? _image4;

        [ObservableProperty]
        private byte[]? _image5;

        [ObservableProperty]
        private byte[]? _image6;

        [ObservableProperty]
        private byte[]? _image7;

        [ObservableProperty]
        private byte[]? _image8;

        [ObservableProperty]
        private byte[]? _image9;

        [ObservableProperty]
        private byte[]? _image10;
    }

    public enum InstructionListMode
    {
        ProductGroup = 0,
        Product = 1
    }
}
