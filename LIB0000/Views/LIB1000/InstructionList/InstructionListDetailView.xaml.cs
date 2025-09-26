namespace LIB0000
{

    public partial class InstructionListDetailView
    {

        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {

            if (BasicService.InstructionsService.InstructionLists.CheckIfVersionListisApproved(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id))
            {
                CustomMessageBox.Show("Een approved lijst kan niet bewerkt worden!", "Warning", MessageBoxImage.Warning);
            }
            else
            {

                BasicService.InstructionsService.Instructions.Edit.InstructionListVersionId = BasicService.InstructionsService.InstructionLists.SelectedVersion.Id;
                BasicService.InstructionsService.Instructions.Edit = new InstructionModel();
                BasicService.InstructionsService.Instructions.Selected = new InstructionModel();

                NavigationService.Navigate(typeof(InstructionTypeSelectView));
            }

        }

        [RelayCommand]
        private void cmdApprove()
        {
            // CountInstructionsInVersionList kijkt of er al instructies in de DB staan.
            // JoinList.Count kijkt of er instructies in een nieuwe lijst staat die nog niet in de DB staat.
            if ((BasicService.InstructionsService.Instructions.CountInstructionsInVersionList(BasicService.InstructionsService.InstructionLists.Selected.Id, BasicService.InstructionsService.InstructionLists.SelectedVersion.Id) > 0) || (BasicService.InstructionsService.Instructions.JoinList.Count != 0))
            {
                BasicService.InstructionsService.InstructionLists.Approve();
            }
            else
            {
                CustomMessageBox.Show("Nog geen instructies in lijst!", "Warning", MessageBoxImage.Warning);
            }

        }

        [RelayCommand]
        private void cmdDeleteRow()
        {

            if (BasicService.InstructionsService.InstructionLists.CheckIfVersionListisApproved(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id))
            {
                CustomMessageBox.Show("Een approved lijst kan niet bewerkt worden!", "Warning", MessageBoxImage.Warning);
            }
            else
            {
                if (BasicService.InstructionsService.Instructions.Edit != null)
                {
                    BasicService.InstructionsService.Instructions.DeleteRow();
                    BasicService.InstructionsService.Instructions.Edit = new InstructionModel();
                }
            }

        }

        [RelayCommand]
        public void cmdSelectInstructionListVersion()
        {
            NavigationService.Navigate(typeof(InstructionListVersionSelectView));
        }

        [RelayCommand]
        private void cmdNewVersion()
        {
            BasicService.InstructionsService.InstructionLists.NewVersion();
        }

        [RelayCommand]
        private void cmdUpdateRow()
        {
            if (BasicService.InstructionsService.InstructionLists.CheckIfVersionListisApproved(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id))
            {
                CustomMessageBox.Show("Een approved lijst kan niet bewerkt worden!", "Warning", MessageBoxImage.Warning);
            }
            else
            {
                if (BasicService.InstructionsService.Instructions.Edit.InstructionTypeName != null)
                {
                    BasicService.InstructionsService.Instructions.UpdateRow();
                }
            }
        }

        [RelayCommand]
        private void cmdBack()
        {
            NavigationService.Navigate(typeof(InstructionListView));
        }

        [RelayCommand]
        private void cmdMakeCurrent()
        {
            BasicService.InstructionsService.InstructionLists.MakeCurrent();
        }

        [RelayCommand]
        private void cmdUp()
        {
            if (BasicService.InstructionsService.InstructionLists.CheckIfVersionListisApproved(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id))
            {
                CustomMessageBox.Show("Een approved lijst kan niet bewerkt worden!", "Warning", MessageBoxImage.Warning);
            }
            else
            {
                if (BasicService.InstructionsService.Instructions.SelectedJoin != null && BasicService.InstructionsService.Instructions.SelectedJoin.Sequence > 1)
                {
                    int newSequence = BasicService.InstructionsService.Instructions.SelectedJoin.Sequence - 1;
                    BasicService.InstructionsService.Instructions.MoveOneUp(BasicService.InstructionsService.Instructions.SelectedJoin.InstructionsListVersionId, BasicService.InstructionsService.Instructions.SelectedJoin.InstructionId);

                    // Update selection efficiently
                    BasicService.InstructionsService.Instructions.SelectedJoin = BasicService.InstructionsService.Instructions.JoinList.FirstOrDefault(e => e.Sequence == newSequence);
                }
            }


        }

        [RelayCommand]
        private void cmdDown()
        {
            if (BasicService.InstructionsService.InstructionLists.CheckIfVersionListisApproved(BasicService.InstructionsService.InstructionLists.SelectedVersion.Id))
            {
                CustomMessageBox.Show("Een approved lijst kan niet bewerkt worden!", "Warning", MessageBoxImage.Warning);
            }
            else
            {
                if (BasicService.InstructionsService.Instructions.SelectedJoin != null)
                {
                    int listVersionId = BasicService.InstructionsService.Instructions.SelectedJoin.InstructionsListVersionId;
                    int selectedSequence = BasicService.InstructionsService.Instructions.SelectedJoin.Sequence;

                    // Get max sequence from the last sorted item instead of querying the database
                    int maxSequence = BasicService.InstructionsService.Instructions.JoinList.OrderByDescending(e => e.Sequence).FirstOrDefault()?.Sequence ?? 0;

                    if (selectedSequence < maxSequence)
                    {
                        int newSequence = selectedSequence + 1;
                        BasicService.InstructionsService.Instructions.MoveOneDown(listVersionId, BasicService.InstructionsService.Instructions.SelectedJoin.InstructionId);

                        // Update selection efficiently
                        BasicService.InstructionsService.Instructions.SelectedJoin = BasicService.InstructionsService.Instructions.JoinList.FirstOrDefault(e => e.Sequence == newSequence);
                    }
                }
            }
        }

        #endregion

        #region Constructor

        public InstructionListDetailView(BasicService basicService, INavigationService iNavigationService)
        {
            BasicService = basicService;
            NavigationService = iNavigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BasicService.InstructionsService.Instructions.SelectedJoin != null)
            {
                BasicService.InstructionsService.Instructions.Edit = BasicService.InstructionsService.Instructions.GetInstruction(BasicService.InstructionsService.Instructions.SelectedJoin.InstructionId);
            }
            else { BasicService.InstructionsService.Instructions.Edit = new InstructionModel(); }
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
