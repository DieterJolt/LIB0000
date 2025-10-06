using System.Windows.Controls;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for TextInputTextUserControl.xaml
    /// </summary>
    public partial class TextInputTextUserControl : UserControl
    {
        public TextInputTextUserControl()
        {
            InitializeComponent();
        }

        public string TextLeft
        {
            get { return (string)GetValue(TextLeftProperty); }
            set { SetValue(TextLeftProperty, value); }
        }

        public static readonly DependencyProperty TextLeftProperty = DependencyProperty.Register("TextLeft", typeof(string), typeof(TextInputTextUserControl));

        public string TextRight
        {
            get { return (string)GetValue(TextRightProperty); }
            set { SetValue(TextRightProperty, value); }
        }

        public static readonly DependencyProperty TextRightProperty = DependencyProperty.Register("TextRight", typeof(string), typeof(TextInputTextUserControl));

        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(string), typeof(TextInputTextUserControl));
    }
}
