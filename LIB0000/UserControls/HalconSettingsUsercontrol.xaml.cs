using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LIB0000
{
    public partial class HalconSettingsUsercontrol : UserControl
    {
        #region Commands        
        [RelayCommand]
        private void ExecuteAutofocus()
        {
            //SelectedHalconService?.CmdAutoFocus(0);
        }

        [RelayCommand]
        private void Measure()
        {
            //SelectedHalconService?.Measure();
        }

        #endregion

        #region Constructor
        public HalconSettingsUsercontrol()
        {
            InitializeComponent();
            //Hier Datacontext = this nodig, om de bindings van SelectedFhService in de pagina zelf te kunnen gebruiken
            DataContext = this;
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        #endregion

        #region Properties

        public HalconService SelectedHalconService
        {
            get { return (HalconService)GetValue(SelectedHalconServiceProperty); }
            set { SetValue(SelectedHalconServiceProperty, value); }
        }

        public static readonly DependencyProperty SelectedHalconServiceProperty = DependencyProperty.Register("SelectedHalconService", typeof(HalconService), typeof(HalconSettingsUsercontrol));

        #endregion
    }
}
