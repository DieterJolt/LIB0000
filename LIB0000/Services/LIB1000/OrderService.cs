using System.Windows.Controls;
using Syncfusion.Windows.Shared;

namespace LIB0000
{
    public partial class OrderService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public OrderService(string databasePath)
        {
            DatabasePath = databasePath;
            Order = new Order(DatabasePath);
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        [ObservableProperty]
        private Order _order;

        #endregion

        #region Methods

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        #endregion

    }

    public partial class Order : ObservableObject
    {

        #region Commands
        [RelayCommand]
        public void cmdInstructionOk()
        {
            LastInstructionOk = true;
        }



        #endregion

        #region Constructor
        public Order(string databasePath)
        {
            DatabasePath = databasePath;
        }

        #endregion

        #region Events
        #endregion

        #region Fields

        #endregion

        #region Methods
        public void AddRow()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                OrderModel row = context.OrderDbSet.FirstOrDefault(e => e.Id == Edit.Id);
                Edit.DateTimeStart = DateTime.Now;

                if ((row == null) && (!string.IsNullOrEmpty(Edit.OrderNr)))
                {
                    row = new OrderModel();
                    row.OrderNr = Edit.OrderNr;
                    row.ProductId = Edit.ProductId;
                    row.ProductGroupId = Edit.ProductGroupId;
                    row.Amount = Edit.Amount;
                    row.UserId = Edit.UserId;
                    row.Extra1 = Edit.Extra1;
                    row.Extra2 = Edit.Extra2;
                    row.Extra3 = Edit.Extra3;
                    row.DateTimeStart = Edit.DateTimeStart;
                    row.WorkstationId = Loaded.WorkstationId;
                    context.OrderDbSet.Add(row);
                    context.SaveChanges();
                    Edit.Id = row.Id;
                    UpdateSelected();
                }
            }
            GetList();
            GetJoinModel();
        }
        public void AddOrderHistory(OrderHistoryType orderHistoryType, string login, int orderAmount, string info, int counter, InstructionModel? instruction = null)
        {

            using (var context = new ServerDbContext(DatabasePath))
            {
                OrderHistoryModel orderHistory = new OrderHistoryModel();

                orderHistory.LoginId = login;
                orderHistory.OrderId = Loaded.Id;
                orderHistory.OrderHistoryType = orderHistoryType;
                orderHistory.Counter = counter;
                orderHistory.Info = info;
                orderHistory.TimeStamp = DateTime.Now;
                orderHistory.OrderAmount = orderAmount;

                switch (orderHistoryType)
                {
                    case OrderHistoryType.InstructionOk:
                    case OrderHistoryType.InstructionNok:
                        InstructionHistoryModel instructionHistory = new();
                        instructionHistory.InstructionType = instruction.InstructionType;
                        instructionHistory.InstructionId = instruction.Id;
                        instructionHistory.InputText1 = instruction.InputText1;
                        instructionHistory.InputText2 = instruction.InputText2;
                        instructionHistory.InputText3 = instruction.InputText3;
                        instructionHistory.InputText4 = instruction.InputText4;
                        instructionHistory.InputText5 = instruction.InputText5;
                        instructionHistory.InputText6 = instruction.InputText6;
                        instructionHistory.InputText7 = instruction.InputText7;
                        instructionHistory.InputText8 = instruction.InputText8;
                        instructionHistory.InputText9 = instruction.InputText9;
                        instructionHistory.InputText10 = instruction.InputText10;
                        instructionHistory.InputBool1 = instruction.InputBool1;
                        instructionHistory.InputBool2 = instruction.InputBool2;
                        instructionHistory.InputBool3 = instruction.InputBool3;
                        instructionHistory.InputBool4 = instruction.InputBool4;
                        instructionHistory.InputBool5 = instruction.InputBool5;
                        instructionHistory.InputBool6 = instruction.InputBool6;
                        instructionHistory.InputBool7 = instruction.InputBool7;
                        instructionHistory.InputBool8 = instruction.InputBool8;
                        instructionHistory.InputBool9 = instruction.InputBool9;
                        instructionHistory.InputBool10 = instruction.InputBool10;
                        instructionHistory.InputImage1 = instruction.InputImage1;

                        context.InstructionHistoryDbSet.Add(instructionHistory);
                        context.SaveChanges();
                        orderHistory.RelatedId = instructionHistory.Id;

                        break;

                    default:
                        orderHistory.RelatedId = 0;
                        break;
                }

                context.OrderHistoryDbSet.Add(orderHistory);
                context.SaveChanges();
                GetOrderLoadedHistoryList(Loaded.Id);
            }
        }
        public void UpdateSelected()
        {
            Loaded = Edit;
        }
        public void GetList()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                var result = from Orders in context.OrderDbSet
                             orderby Orders.Id
                             select Orders;

                List = result.ToList();
            }
        }
        public void GetJoinModel()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                DatabaseConnectionOk = context.Database.CanConnect();
                if (!DatabaseConnectionOk)
                {
                    return;
                }

                var result =
                    (from order in context.OrderDbSet
                     join productGroup in context.ProductGroupDbSet
                         on order.ProductGroupId equals productGroup.Id
                     join product in context.ProductDbSet
                         on order.ProductId equals product.Id
                     where order.OrderNr == Edit.OrderNr
                     orderby order.Id
                     select new OrderJoinModel
                     {
                         OrderNr = order.OrderNr,
                         Amount = order.Amount,
                         ProductGroupName = productGroup.Name,
                         ProductName = product.Name,
                     }).FirstOrDefault();

                SelectedJoin = result;
            }
        }
        public void GetOrderLoadedHistoryList(int orderId)
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                OrderHistoryList = context.OrderHistoryDbSet.Where(od => od.OrderId == orderId).OrderByDescending(od => od.TimeStamp).ToList();
            }
            FilterOrderLoadedHistoryList();
        }
        private void FilterOrderLoadedHistoryList()
        {
            var listLoadedFiltered = OrderHistoryList.Where(ojm =>
                                (FilterInstruction && (ojm.OrderHistoryType == OrderHistoryType.InstructionOk || ojm.OrderHistoryType == OrderHistoryType.InstructionNok)) ||
                                (FilterStoppedByCamera && ojm.OrderHistoryType == OrderHistoryType.StoppedByCamera) ||
                                (FilterStartStop && (ojm.OrderHistoryType == OrderHistoryType.Run || ojm.OrderHistoryType == OrderHistoryType.Stop)) ||
                                (FilterTimeouts && ojm.OrderHistoryType == OrderHistoryType.Timeout)
                                ).ToList();


            ListLoadedFiltered = listLoadedFiltered.OrderByDescending(orderHistoryitem => orderHistoryitem.TimeStamp).ToList(); ;
        }

        private void FilterOrderSelectedHistoryList()
        {
            if (Selected == null) { return; }

            using (var context = new ServerDbContext(DatabasePath))
            {
                var orderHistoryList = context.OrderHistoryDbSet.Where(od => od.OrderId == Selected.Id).OrderByDescending(od => od.TimeStamp).ToList();
                var listSelectedFiltered = orderHistoryList.Where(ojm =>
                                    (FilterInstruction && (ojm.OrderHistoryType == OrderHistoryType.InstructionOk || ojm.OrderHistoryType == OrderHistoryType.InstructionNok)) ||
                                    (FilterStoppedByCamera && ojm.OrderHistoryType == OrderHistoryType.StoppedByCamera) ||
                                    (FilterStartStop && (ojm.OrderHistoryType == OrderHistoryType.Run || ojm.OrderHistoryType == OrderHistoryType.Stop)) ||
                                    (FilterTimeouts && ojm.OrderHistoryType == OrderHistoryType.Timeout)
                                    ).ToList();
                ListSelectedFiltered = listSelectedFiltered.OrderByDescending(orderHistoryitem => orderHistoryitem.TimeStamp).ToList(); ;
            }
        }

        public void GetLastActiveOrder(int? workstationId)
        {
            GetList();
            OrderModel row = List.FirstOrDefault(e => e.DateTimeStop == null);
            if (row != null)
            {
                IsOrderBusy = true;
                Loaded = row;
            }
            else
            {
                IsOrderBusy = false;
            }
        }
        public bool Load(int recipeId, int userId)
        {
            bool result = false;

            Edit.ProductId = recipeId;
            Edit.UserId = userId;


            if ((Edit is OrderModel) && (Edit.OrderNr.Length > 1) && (Edit.Amount > 0) && (Edit.ProductId > 0) && (Edit.UserId > 0))
            {
                Loaded.Id = Edit.Id;
                Loaded.OrderNr = Edit.OrderNr;
                Loaded.ProductId = Edit.ProductId;
                Loaded.UserId = Edit.UserId;
                Loaded.Amount = Edit.Amount;
                Loaded.Extra1 = Edit.Extra1;
                Loaded.Extra2 = Edit.Extra2;
                Loaded.Extra3 = Edit.Extra3;
                Loaded.DateTimeStart = DateTime.Now;
                result = true;
                IsOrderBusy = true;
                AddRow();
                Edit.OrderNr = "";
                Edit.Amount = 0;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public void UpdateOrder()
        {
            using (var context = new ServerDbContext(DatabasePath))
            {
                OrderModel row = List.FirstOrDefault(e => e.Id == Loaded.Id);
                if (row != null)
                {
                    row.TotalProduct = Loaded.TotalProduct;
                    row.BadProduct = Loaded.BadProduct;
                    row.GoodProduct = Loaded.GoodProduct;
                    row.MachineStopAmount = Loaded.MachineStopAmount;
                    row.OrderStep = Loaded.OrderStep;
                    context.OrderDbSet.Update(row);
                    context.SaveChanges();
                    GetList();
                    GetJoinModel();
                }
            }
        }
        public void CloseOrder()
        {
            IsOrderBusy = false;
            using (var context = new ServerDbContext(DatabasePath))
            {
                OrderModel row = List.FirstOrDefault(e => e.Id == Loaded.Id);

                if (row != null)
                {
                    row.DateTimeStop = DateTime.Now;
                    row.TotalProduct = Loaded.TotalProduct;
                    row.BadProduct = Loaded.BadProduct;
                    row.GoodProduct = Loaded.GoodProduct;
                    row.MachineStopAmount = Loaded.MachineStopAmount;
                    context.OrderDbSet.Update(row);
                    context.SaveChanges();
                    GetList();
                    GetJoinModel();
                    Loaded.Id = 0;
                    Loaded.OrderNr = "";
                    Loaded.ProductId = 0;
                    Loaded.UserId = 0;
                    Loaded.Amount = 0;
                    Loaded.BadProduct = 0;
                    Loaded.GoodProduct = 0;
                    Loaded.TotalProduct = 0;
                    Loaded.MachineStopAmount = 0;
                    Loaded.Extra1 = "";
                    Loaded.Extra2 = "";
                    Loaded.Extra3 = "";
                    Loaded.Extra3 = "";
                    Edit = new OrderModel();
                    ListLoadedFiltered = new List<OrderHistoryModel>();
                    UpdateSelected();
                }
            }
        }

        //public void Filter()
        //{
        //    GetListJoinModel();

        //    if (FilterStartDate != null)
        //    {
        //        JoinList = JoinList
        //            .Where(e => ((DateTime)e.DateTimeStart >= FilterStartDate) &&
        //            ((DateTime)e.DateTimeStart <= FilterEndDate))
        //            .ToList();
        //    }


        //}

        public void DeleteHistoryRow()
        {
            using (var orderContext = new ServerDbContext(DatabasePath))
            {

                OrderModel order = orderContext.OrderDbSet.Find(SelectedHistoryModel.Id);
                if (order != null)
                {
                    orderContext.OrderDbSet.Remove(order);
                    orderContext.SaveChanges();
                }
            }
            GetList();
            GetJoinModel();
        }

        #endregion

        #region Properties
        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private List<OrderModel> _list = new List<OrderModel>();

        [ObservableProperty]
        private List<OrderHistoryModel> _orderHistoryList = new List<OrderHistoryModel>();

        [ObservableProperty]
        private OrderModel _edit = new OrderModel();

        [ObservableProperty]
        private OrderModel _loaded = new OrderModel();

        //Filter properties
        private OrderModel _selected = new OrderModel();
        public OrderModel Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    OnPropertyChanged(nameof(Selected));
                    FilterOrderSelectedHistoryList();
                }
            }
        }


        private DateTime _filterEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
        public DateTime FilterEndDate
        {
            get { return _filterEndDate; }
            set
            {
                if (_filterEndDate != value)
                {
                    if (_filterEndDate > FilterStartDate)
                    {
                        _filterEndDate = value;
                    }
                    else
                    {
                        _filterEndDate = FilterStartDate.AddDays(1);
                    }
                    _filterEndDate = new DateTime(_filterEndDate.Year, _filterEndDate.Month, _filterEndDate.Day, 23, 59, 59);
                    OnPropertyChanged(nameof(FilterEndDate));

                }
            }
        }
        // observable property
        private DateTime _filterStartDate = DateTime.Today.AddDays(-30);
        public DateTime FilterStartDate
        {
            get { return _filterStartDate; }
            set
            {

                if (_filterStartDate != value)
                {
                    _filterStartDate = value;
                    _filterStartDate = new DateTime(_filterStartDate.Year, _filterStartDate.Month, _filterStartDate.Day, 0, 0, 0);
                    OnPropertyChanged(nameof(FilterStartDate));

                }

            }
        }

        [ObservableProperty]
        private List<OrderJoinModel> _joinList = new List<OrderJoinModel>();

        [ObservableProperty]
        private List<OrderHistoryModel> _listLoadedFiltered = new List<OrderHistoryModel>();

        [ObservableProperty]
        private List<OrderHistoryModel> _listSelectedFiltered = new List<OrderHistoryModel>();

        [ObservableProperty]
        private OrderJoinModel _selectedJoin = new OrderJoinModel();

        [ObservableProperty]
        private OrderJoinModel _selectedHistoryModel = new OrderJoinModel();

        [ObservableProperty]
        private bool _isOrderBusy = false;

        [ObservableProperty]
        private bool _databaseConnectionOk = true;

        [ObservableProperty]
        private bool _lastInstructionOk = false;


        //Filter properties
        private bool _filterInstruction = true;
        public bool FilterInstruction
        {
            get { return _filterInstruction; }
            set
            {
                if (_filterInstruction != value)
                {
                    _filterInstruction = value;
                    OnPropertyChanged(nameof(FilterInstruction));
                    GetOrderLoadedHistoryList(Loaded.Id);
                    FilterOrderSelectedHistoryList();
                }
            }
        }

        private bool _filterStoppedByCamera = true;
        public bool FilterStoppedByCamera
        {
            get { return _filterStoppedByCamera; }
            set
            {
                if (_filterStoppedByCamera != value)
                {
                    _filterStoppedByCamera = value;
                    OnPropertyChanged(nameof(FilterStoppedByCamera));
                    GetOrderLoadedHistoryList(Loaded.Id);
                    FilterOrderSelectedHistoryList();
                }
            }
        }

        private bool _filterStartStop = true;
        public bool FilterStartStop
        {
            get { return _filterStartStop; }
            set
            {
                if (_filterStartStop != value)
                {
                    _filterStartStop = value;
                    OnPropertyChanged(nameof(FilterStartStop));
                    GetOrderLoadedHistoryList(Loaded.Id);
                    FilterOrderSelectedHistoryList();
                }
            }
        }

        private bool _filterTimeouts = true;
        public bool FilterTimeouts
        {
            get { return _filterTimeouts; }
            set
            {
                if (_filterTimeouts != value)
                {
                    _filterTimeouts = value;
                    OnPropertyChanged(nameof(FilterTimeouts));
                    GetOrderLoadedHistoryList(Loaded.Id);
                    FilterOrderSelectedHistoryList();
                }
            }
        }

        [ObservableProperty]
        private int _productDuringControlNeeded = 0;


        #endregion


    }




}
