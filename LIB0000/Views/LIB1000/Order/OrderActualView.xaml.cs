using System.Diagnostics;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Windows.Input;
using System.Windows.Media;
using LIB0000;

namespace LIB0000
{
    public partial class OrderActualView
    {

        #region Commands

        [RelayCommand]
        private void cmdFilterTest()
        {
            BasicService.OrdersService.Order.FilterInstruction = !BasicService.OrdersService.Order.FilterInstruction;
        }
        #endregion

        #region Constructor

        public OrderActualView(BasicService basicService, INavigationService navigationService, GlobalService globalService)
        {
            NavigationService = navigationService;
            BasicService = basicService;
            GlobalService = globalService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events               
        #endregion

        #region Fields

        public BasicService BasicService { get; set; }
        public GlobalService GlobalService { get; set; }

        public INavigationService NavigationService { get; set; }

        #endregion

        #region Methods        
        #endregion

        #region Properties

        public bool ToggleView;



        #endregion

    }
}
