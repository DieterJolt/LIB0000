using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LIB0000
{
    public partial class FHV7BasicSettingsUserControl : UserControl
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
        public FHV7BasicSettingsUserControl()
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

        public static readonly DependencyProperty SelectedFhServiceProperty = DependencyProperty.Register("SelectedFhService", typeof(FhService), typeof(FHV7BasicSettingsUserControl));

        #endregion
    }
}
