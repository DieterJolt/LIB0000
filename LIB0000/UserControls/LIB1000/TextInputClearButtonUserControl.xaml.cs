using System.Windows.Controls;
using System.Windows.Media;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for TextInputTextUserControl.xaml
    /// </summary>
    public partial class TextInputClearButtonUserControl : UserControl
    {
        public TextInputClearButtonUserControl()
        {
            InitializeComponent();
        }

        public string TextLeft
        {
            get { return (string)GetValue(TextLeftProperty); }
            set { SetValue(TextLeftProperty, value); }
        }

        public static readonly DependencyProperty TextLeftProperty = DependencyProperty.Register("TextLeft", typeof(string), typeof(TextInputClearButtonUserControl));

        public string TextRight
        {
            get { return (string)GetValue(TextRightProperty); }
            set { SetValue(TextRightProperty, value); }
        }

        public static readonly DependencyProperty TextRightProperty = DependencyProperty.Register("TextRight", typeof(string), typeof(TextInputClearButtonUserControl));

        public RelayCommand CommandRight
        {
            get { return (RelayCommand)GetValue(CommandRightProperty); }
            set { SetValue(CommandRightProperty, value); }
        }

        public static readonly DependencyProperty CommandRightProperty = DependencyProperty.Register("CommandRight", typeof(RelayCommand), typeof(TextInputClearButtonUserControl));

        public bool VisibilityRight
        {
            get { return (bool)GetValue(VisibilityRightProperty); }
            set { SetValue(VisibilityRightProperty, value); }
        }

        public static readonly DependencyProperty VisibilityRightProperty = DependencyProperty.Register("VisibilityRight", typeof(bool), typeof(TextInputClearButtonUserControl)
        );

        //Button 1 image
        public ImageSource ImageRight
        {
            get { return (ImageSource)GetValue(ImageRightProperty); }
            set { SetValue(ImageRightProperty, value); }
        }

        public static readonly DependencyProperty ImageRightProperty = DependencyProperty.Register("ImageRight", typeof(ImageSource),
            typeof(TextInputClearButtonUserControl)
        );


        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(string), typeof(TextInputClearButtonUserControl));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input = null;
        }
    }
}
