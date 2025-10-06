using System.Windows.Controls;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for TextToggleUserControl.xaml
    /// </summary>
    public partial class TextToggleUserControl : UserControl
    {
        public TextToggleUserControl()
        {
            InitializeComponent();
        }

        public string TextLeft
        {
            get { return (string)GetValue(TextLeftProperty); }
            set { SetValue(TextLeftProperty, value); }
        }

        public static readonly DependencyProperty TextLeftProperty = DependencyProperty.Register("TextLeft", typeof(string), typeof(TextToggleUserControl));

        public string TextRight
        {
            get { return (string)GetValue(TextRightProperty); }
            set { SetValue(TextRightProperty, value); }
        }

        public static readonly DependencyProperty TextRightProperty = DependencyProperty.Register("TextRight", typeof(string), typeof(TextToggleUserControl));

        public string TextToggleOn
        {
            get { return (string)GetValue(TextToggleOnProperty); }
            set { SetValue(TextToggleOnProperty, value); }
        }
        public static readonly DependencyProperty TextToggleOnProperty = DependencyProperty.Register("TextToggleOn", typeof(string), typeof(TextToggleUserControl));

        public string TextToggleOff
        {
            get { return (string)GetValue(TextToggleOffProperty); }
            set { SetValue(TextToggleOffProperty, value); }
        }
        public static readonly DependencyProperty TextToggleOffProperty = DependencyProperty.Register("TextToggleOff", typeof(string), typeof(TextToggleUserControl));

        public bool ToggleIsChecked
        {
            get { return (bool)GetValue(ToggleIsCheckedProperty); }
            set { SetValue(ToggleIsCheckedProperty, value); }
        }
        public static readonly DependencyProperty ToggleIsCheckedProperty = DependencyProperty.Register("ToggleIsChecked", typeof(string), typeof(TextToggleUserControl));





    }
}
