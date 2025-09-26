using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public partial class ImageModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ObservableProperty]
        public string _imageName;
        [ObservableProperty]
        public string _imageDescription;
        [ObservableProperty]
        public DateTime _dateTimeCreated;
        [ObservableProperty]
        public string _status;
        [ObservableProperty]
        public int _percentage;

    }
}
