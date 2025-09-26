using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace LIB0000
{
    public partial class FhViewUserControl : UserControl
    {
        #region Commands

        #endregion

        #region Constructor
        public FhViewUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void cnv_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Fields        

        private bool editRegioBusy;

        private System.Windows.Point rectangleUpperLeftTemp;
        private System.Windows.Point rectangleLowerRightTemp;
        private System.Windows.Shapes.Rectangle rectangleTemp;

        #endregion

        #region Methods



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        public string Image1
        {
            get { return (string)GetValue(Image1Property); }
            set { SetValue(Image1Property, value); }
        }

        public static readonly DependencyProperty Image1Property = DependencyProperty.Register("Image1", typeof(string), typeof(FhViewUserControl));

        public double ActualImageHeight
        {
            get { return (double)GetValue(ActualImageHeightProperty); }
            set { SetValue(ActualImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ActualImageHeightProperty = DependencyProperty.Register("ActualImageHeight", typeof(double), typeof(FhViewUserControl));
        public double ActualImageWidth
        {
            get { return (double)GetValue(ActualImageWidthProperty); }
            set { SetValue(ActualImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ActualImageWidthProperty = DependencyProperty.Register("ActualImageWidth", typeof(double), typeof(FhViewUserControl));

        public ObservableCollection<FhService> ListFhService
        {
            get { return (ObservableCollection<FhService>)GetValue(ListFhServiceProperty); }
            set { SetValue(ListFhServiceProperty, value); }
        }

        public static readonly DependencyProperty ListFhServiceProperty = DependencyProperty.Register("ListFhService", typeof(ObservableCollection<FhService>), typeof(FhViewUserControl));

        public FhService SelectedFhService
        {
            get { return (FhService)GetValue(SelectedFhServiceProperty); }
            set { SetValue(SelectedFhServiceProperty, value); }
        }

        public static readonly DependencyProperty SelectedFhServiceProperty = DependencyProperty.Register(nameof(SelectedFhService), typeof(FhService), typeof(FhViewUserControl),
                new PropertyMetadata(null));
        public HardwareFunction SelectedHardwareFunction
        {
            get { return (HardwareFunction)GetValue(SelectedHardwareFunctionProperty); }
            set { SetValue(SelectedHardwareFunctionProperty, value); }
        }

        public static readonly DependencyProperty SelectedHardwareFunctionProperty = DependencyProperty.Register("SelectedHardwareFunction", typeof(HardwareFunction), typeof(FhViewUserControl), new PropertyMetadata(HardwareFunction.None));

        #endregion        
    }
}
