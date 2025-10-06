using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class VideoInfoModel : ObservableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ObservableProperty]
        public string _videoTitle;
        [ObservableProperty]
        public string? _videoSource;
        [ObservableProperty]
        public int? _videoRotation;
        [ObservableProperty]
        public string? _imageSource;
        [ObservableProperty]
        public int? _imageRotation;
        [ObservableProperty]
        public string? _videoSource2;
        [ObservableProperty]
        public int? _videoRotation2;
        [ObservableProperty]
        public string? _imageSource2;
        [ObservableProperty]
        public int? _imageRotation2;
        [ObservableProperty]
        public int? _splitScreen;

    }
}
