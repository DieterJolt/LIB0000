using System.Windows.Controls;

namespace LIB0000
{
    public partial class InstructionListView : Page
    {
        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {
            BasicService.InstructionsService.InstructionLists.Edit = new InstructionListModel();
            BasicService.InstructionsService.InstructionLists.Selected = new InstructionListModel();
            NavigationService.Navigate(typeof(InstructionListsEditView));
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {
            BasicService.InstructionsService.InstructionLists.DeleteRow();
            BasicService.InstructionsService.InstructionLists.UpdateRow();
        }

        [RelayCommand]
        private void cmdEditRow()
        {
            if ((BasicService.InstructionsService.InstructionLists.Selected != null) && (BasicService.InstructionsService.InstructionLists.Selected.Id > 0))
            {
                NavigationService.Navigate(typeof(InstructionListsEditView));
            }
        }

        [RelayCommand]
        private void cmdDetail()
        {
            if (BasicService.InstructionsService.InstructionLists.SelectedVersion != null)
            {
                BasicService.InstructionsService.Instructions.GetJoinList(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id);
                NavigationService.Navigate(typeof(InstructionListDetailView));
            }
        }

        [RelayCommand]
        private void cmdCopy()
        {
            if ((BasicService.InstructionsService.InstructionLists.Selected != null) && (BasicService.InstructionsService.InstructionLists.Selected.Id > 0))
            {
                BasicService.InstructionsService.InstructionLists.CopyList(BasicService.InstructionsService.InstructionLists.Selected.Id, BasicService.InstructionsService.InstructionLists.Selected.CurrentVersion);
            }
        }


        #endregion

        #region Constructor

        public InstructionListView(BasicService basicService, INavigationService navigationService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            DataContext = this;
            InitializeComponent();

        }

        #endregion

        #region Events

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.InstructionsService.InstructionLists.FilterList(sender.Text);
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.InstructionsService.InstructionLists.UpdateSelected();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.InstructionsService.InstructionLists.GetList();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService NavigationService { get; set; }


        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
