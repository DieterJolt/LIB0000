namespace LIB0000
{
    public partial class MessageStepView
    {
        #region Commands

        [RelayCommand]
        private void cmdNavigate()
        {
            NavigationService.Navigate(typeof(MessagesActualView));
        }

        #endregion

        #region Constructor

        public MessageStepView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            InitializeComponent();
        }

        #endregion

        #region Events
        #endregion

        #region Fields

        public BasicService BasicService { get; }

        public INavigationService NavigationService { get; set; }

        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion
    }
}
