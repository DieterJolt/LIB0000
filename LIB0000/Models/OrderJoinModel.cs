using System.ComponentModel.DataAnnotations;

namespace LIB0000
{
    public partial class OrderJoinModel : ObservableObject
    {
        [Key]
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private string _orderNr;
        [ObservableProperty]
        private int _amount;
        [ObservableProperty]
        public string _productGroupName;
        [ObservableProperty]
        public string _productName;


    }
}
