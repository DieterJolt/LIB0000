using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB0000
{
    public partial class OrderHistoryModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ObservableProperty]
        public int _id;
        [ObservableProperty]
        public int _orderId;
        [ObservableProperty]
        private int _relatedId;
        [ObservableProperty]
        private int _orderAmount;
        [ObservableProperty]
        OrderHistoryType _orderHistoryType;
        [ObservableProperty]
        public string _loginId;
        [ObservableProperty]
        public double _counter;
        [ObservableProperty]
        public string _info = "";
        [ObservableProperty]
        public DateTime _timeStamp;
    }
    public enum OrderHistoryType
    {
        // Deze nummers niet hernummeren, enkel toevoegen onderaan
        OrderStart = 0,
        Run = 1,
        Stop = 2,
        OrderClose = 3,

    }

}
