using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LIB0000
{
    public partial class FhBasicSettingsUsercontrol : UserControl
    {
        #region Commands        
        [RelayCommand]
        private void ExecuteAutofocus()
        {
            SelectedFhService?.CmdAutoFocus(0);
        }

        [RelayCommand]
        private void Measure()
        {
            SelectedFhService?.Measure();
        }

        #endregion

        #region Constructor
        public FhBasicSettingsUsercontrol()
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

        public FhService SelectedFhService
        {
            get { return (FhService)GetValue(SelectedFhServiceProperty); }
            set { SetValue(SelectedFhServiceProperty, value); }
        }

        public static readonly DependencyProperty SelectedFhServiceProperty = DependencyProperty.Register("SelectedFhService", typeof(FhService), typeof(FhBasicSettingsUsercontrol));

        #endregion
    }
}
