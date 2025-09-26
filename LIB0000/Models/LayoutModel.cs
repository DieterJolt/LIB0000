namespace LIB0000
{
    public partial class LayoutModel : ObservableObject
    {
        [ObservableProperty]
        public string _title;
        [ObservableProperty]
        public string _subtitle;
        [ObservableProperty]
        public string _subtitle2;
        [ObservableProperty]
        public DateTime _issueDate;
        //[ObservableProperty]
        //public AddressLayout _address;

        //Model van de lijst aanpassen naar de nodige lijst 
        //Dit moet ook gebeuren in de PrintService
        [ObservableProperty]
        public List<TestModel> _items;
        [ObservableProperty]
        public string _comments;

    }

    public partial class AddressLayout : ObservableObject
    {
        [ObservableProperty]
        public string _companyName = "JOLT";
        [ObservableProperty]
        public string _street = "Zusterparmentierstraat";
        [ObservableProperty]
        public string _houseNr = "5";
        [ObservableProperty]
        public string _zipCode = "8500";
        [ObservableProperty]
        public string _country = "België";
    }
}
