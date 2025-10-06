namespace LIB0000
{
    public partial class VideoInfoService : ObservableObject
    {

        #region Commands
        #endregion

        #region Constructor

        public VideoInfoService(string databasePath)
        {
            DatabasePath = databasePath;
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods


        public void GetSelected()
        {
            using (var context = new LocalDbContext())
            {
                var result = from InfoVideos in context.InfoVideosDbSet
                             where InfoVideos.VideoTitle == SelectedVideoTitle
                             select InfoVideos;

                Selected = result.FirstOrDefault();
            }
        }

        #endregion

        #region Properties

        [ObservableProperty]
        private string _databasePath;

        [ObservableProperty]
        private VideoInfoModel _selected = new VideoInfoModel();

        private string _selectedVideoTitle;
        public string SelectedVideoTitle
        {
            get { return _selectedVideoTitle; }
            set
            {
                _selectedVideoTitle = value;
                GetSelected();
            }
        }

        #endregion
    }
}