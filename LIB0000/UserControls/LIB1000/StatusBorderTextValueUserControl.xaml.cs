using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for StatusBorderTextValueUserControl.xaml
    /// </summary>
    public partial class StatusBorderTextValueUserControl : UserControl
    {
        public StatusBorderTextValueUserControl()
        {
            InitializeComponent();
        }

        public string TextTop
        {
            get { return (string)GetValue(TextTopProperty); }
            set { SetValue(TextTopProperty, value); }
        }

        public static readonly DependencyProperty TextTopProperty = DependencyProperty.Register("TextTop", typeof(string), typeof(StatusBorderTextValueUserControl));

        public string TextBottom
        {
            get { return (string)GetValue(TextBottomProperty); }
            set { SetValue(TextBottomProperty, value); }
        }

        public static readonly DependencyProperty TextBottomProperty = DependencyProperty.Register("TextBottom", typeof(string), typeof(StatusBorderTextValueUserControl));

        // create a color property

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(StatusBorderTextValueUserControl));

        // create color text property

        public SolidColorBrush ColorText
        {
            get { return (SolidColorBrush)GetValue(ColorTextProperty); }
            set { SetValue(ColorTextProperty, value); }
        }
        public static readonly DependencyProperty ColorTextProperty = DependencyProperty.Register("ColorText", typeof(SolidColorBrush), typeof(StatusBorderTextValueUserControl));
    }
}
