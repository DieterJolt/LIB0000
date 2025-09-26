using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public class SettingHistoryLineModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nr { get; set; }
        public HardwareFunction HardwareFunction { get; set; }
        public int HardwareId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousValue { get; set; }
        public string ActualValue { get; set; }
        public string User { get; set; }
        // Bvb "Setting ABC01 niet gevonden"
        public string ExtraInfo { get; set; }

    }
}
