using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIB0000
{
    public class MessageHistoryLineModel
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Group { get; set; }
        public string Nr { get; set; }
        public DateTime TimeStamp { get; set; }



    }
}
