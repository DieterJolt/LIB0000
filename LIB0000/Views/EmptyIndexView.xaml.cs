using System.Diagnostics;

namespace LIB0000
{
    public sealed partial class EmptyIndexView
    {
        #region Commands
        #endregion

        #region Constructor

        public EmptyIndexView(BasicService basicService)
        {
            BasicService = basicService;
            DataContext = this;
            InitializeComponent();
            

        }
        #endregion

        #region Events
        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        #endregion

        #region Methods
        #endregion

        #region Properties

        bool C0;
        bool C1;

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BasicService.TurckService[0].SetOutput(2);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            BasicService.TurckService[0].ClearOutput(2);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            BasicService.TurckService[0].SetOutput(3);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            BasicService.TurckService[0].ClearOutput(3);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            C0 = BasicService.TurckService[0].GetInput(0);
            C1 = BasicService.TurckService[0].GetInput(1);

        }
    }
}
