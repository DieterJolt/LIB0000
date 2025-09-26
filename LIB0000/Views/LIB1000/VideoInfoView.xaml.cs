namespace LIB0000
{
    public partial class VideoInfoView
    {
        #region Commands

        [RelayCommand]
        private void cmdPlay()
        {
            Video1.Play();
            Video2.Play();
        }

        [RelayCommand]
        private void cmdPause()
        {
            Video1.Pause();
            Video2.Pause();
        }

        [RelayCommand]
        private void cmdStop()
        {
            Video1.Stop();
            Video2.Stop();
        }

        [RelayCommand]
        private void cmdReplay()
        {
            Video1.Stop();
            Video1.Play();
            Video2.Stop();
            Video2.Play();
        }

        [RelayCommand]

        private void cmdBack()
        {
            NavigationService.GoBack();
        }

        #endregion

        #region Constructor

        public VideoInfoView(VideoInfoService videoInfoService, INavigationService iNavigationService)
        {
            VideoInfoService = videoInfoService;
            NavigationService = iNavigationService;
            DataContext = this;
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckVisibility();
        }

        #endregion

        #region Fields

        public VideoInfoService VideoInfoService { get; set; }
        public INavigationService NavigationService { get; set; }

        #endregion

        #region Methods

        private void CheckVisibility()
        {
            //Video1 controle
            if (VideoInfoService.Selected.VideoSource == null)
            {
                Video1.Visibility = Visibility.Hidden;
                Video1Border.Visibility = Visibility.Hidden;

            }
            else
            {
                Video2.Visibility = Visibility.Visible;
                Video2Border.Visibility = Visibility.Visible;
                Video1.Play();
                Video1.Pause();
            }

            //Video2 controle
            if (VideoInfoService.Selected.VideoSource2 == null)
            {
                Video2.Visibility = Visibility.Hidden;
                Video2Border.Visibility = Visibility.Hidden;

            }
            else
            {
                Video2.Visibility = Visibility.Visible;
                Video2Border.Visibility = Visibility.Visible;
                Video2.Play();
                Video2.Pause();
            }

            //Image1 controle
            if (VideoInfoService.Selected.ImageSource == null)
            {
                Image1.Visibility = Visibility.Hidden;
                Image1Border.Visibility = Visibility.Hidden;

            }
            else
            {
                Image1.Visibility = Visibility.Visible;
                Image1Border.Visibility = Visibility.Visible;
            }

            //Image2 controle
            if (VideoInfoService.Selected.ImageSource2 == null)
            {
                Image2.Visibility = Visibility.Hidden;
                Image2Border.Visibility = Visibility.Hidden;

            }
            else
            {
                Image2.Visibility = Visibility.Visible;
                Image2Border.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region Properties
        #endregion


    }
}
