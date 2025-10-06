namespace LIB0000
{
    public sealed partial class UserPagesView
    {
        #region Commands

        [RelayCommand]
        private void cmdSaveUserpageToUsersPagesList()
        {
            BasicService.UsersService.Pages.UpdateRow();
        }

        #endregion

        #region Constructor

        public UserPagesView(BasicService basicService)
        {
            BasicService = basicService;
            DataContext = this;
            InitializeComponent();

        }
        #endregion

        #region Events

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.UsersService.Pages.UpdateSelected();
        }

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
