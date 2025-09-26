namespace LIB0000
{
    public sealed partial class UserHistoriekView
    {
        #region Commands
        #endregion

        #region Constructor

        public UserHistoriekView(BasicService basicService)
        {
            BasicService = basicService;
            DataContext = this;
            InitializeComponent();

        }
        #endregion

        #region Events
        #endregion

        #region Fields
        public BasicService BasicService { get; }
        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion
    }
}
