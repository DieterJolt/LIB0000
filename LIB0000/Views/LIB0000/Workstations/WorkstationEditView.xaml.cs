
namespace LIB0000
{

    public partial class WorkstationEditView
    {

        #region Commands

        [RelayCommand]
        private void cmdOk()
        {
            BasicService.WorkstationsService.Workstations.AddRow();
            NavigationService.GoBack();
        }

        [RelayCommand]
        private void cmdCancel()
        {
            BasicService.WorkstationsService.Workstations.Edit.Name = "";
            BasicService.WorkstationsService.Workstations.Edit.Description = "";
            NavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public WorkstationEditView(BasicService basicService, INavigationService navigationService, ImageService imageService)
        {
            BasicService = basicService;
            NavigationService = navigationService;
            ImageService = imageService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Change_Image(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BasicService.WorkstationsService.Workstations.AddImage();
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
