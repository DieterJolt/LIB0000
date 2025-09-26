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
    public partial class LayoutEdit08SelectionlistUserControl : UserControl
    {
        #region Commands
        #endregion

        #region Constructor

        public LayoutEdit08SelectionlistUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods
        #endregion

        #region Properties

        public InstructionModel InstructionModel
        {
            get { return (InstructionModel)GetValue(InstructionModelProperty); }
            set { SetValue(InstructionModelProperty, value); }
        }

        public static readonly DependencyProperty InstructionModelProperty = DependencyProperty.Register("InstructionModel", typeof(InstructionModel), typeof(LayoutEdit08SelectionlistUserControl));

        public ICommand cmdInstructionOk
        {
            get => (ICommand)GetValue(cmdInstructionOkProperty);
            set => SetValue(cmdInstructionOkProperty, value);
        }

        public static readonly DependencyProperty cmdInstructionOkProperty = DependencyProperty.Register(nameof(cmdInstructionOk), typeof(ICommand), typeof(LayoutEdit08SelectionlistUserControl), new PropertyMetadata(null));

        public Visibility EditVisibilityMode
        {
            get { return (Visibility)GetValue(EditVisibilityModeProperty); }
            set { SetValue(EditVisibilityModeProperty, value); }
        }

        public static readonly DependencyProperty EditVisibilityModeProperty = DependencyProperty.Register("EditVisibilityMode", typeof(Visibility), typeof(LayoutEdit08SelectionlistUserControl));

        public Visibility NextButtonVisibilityMode
        {
            get { return (Visibility)GetValue(NextButtonVisibilityModeProperty); }
            set { SetValue(NextButtonVisibilityModeProperty, value); }
        }

        public static readonly DependencyProperty NextButtonVisibilityModeProperty = DependencyProperty.Register("NextButtonVisibilityMode", typeof(Visibility), typeof(LayoutEdit08SelectionlistUserControl));

        public bool EditEnabledMode
        {
            get { return (bool)GetValue(EditEnabledModeProperty); }
            set { SetValue(EditEnabledModeProperty, value); }
        }

        public static readonly DependencyProperty EditEnabledModeProperty = DependencyProperty.Register("EditEnabledMode", typeof(bool), typeof(LayoutEdit08SelectionlistUserControl));

        #endregion
    }
}
