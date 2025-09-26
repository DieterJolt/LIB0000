
namespace LIB0000
{

    public partial class WorkstationView
    {

        #region Commands

        [RelayCommand]
        private void cmdAddRow()
        {

            BasicService.WorkstationsService.Workstations.Edit = new WorkstationModel();
            BasicService.WorkstationsService.Workstations.Selected = new WorkstationModel();


            BasicService.WorkstationsService.Workstations.Edit.Image = ImageService.ConvertImageToByteArray("pack://application:,,,/Assets/Images/VoorbeeldAfbeelding.png");

            NavigationService.Navigate(typeof(WorkstationEditView));
        }

        [RelayCommand]
        private void cmdDeleteRow()
        {

            BasicService.WorkstationsService.Workstations.DeleteRow();
            BasicService.WorkstationsService.Workstations.UpdateRow();

        }

        [RelayCommand]
        private void cmdEditRow()
        {
            if ((BasicService.WorkstationsService.Workstations.Selected != null) && (BasicService.WorkstationsService.Workstations.Selected.Id > 0))
            {
                NavigationService.Navigate(typeof(WorkstationEditView));
            }
        }


        #endregion

        #region Constructor

        public WorkstationView(BasicService basicService, INavigationService navigationService, ImageService imageService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            ImageService = imageService;
            DataContext = this;
            InitializeComponent();

        }

        #endregion

        #region Events

        private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender, Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            BasicService.WorkstationsService.Workstations.FilterList(sender.Text);
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            BasicService.WorkstationsService.Workstations.UpdateSelected();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BasicService.WorkstationsService.Workstations.GetList();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            AutoSuggestBox.Text = "";
        }

        #endregion

        #region Fields

        public BasicService BasicService { get; set; }

        public INavigationService NavigationService { get; set; }

        public ImageService ImageService { get; set; }



        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion


    }
}
