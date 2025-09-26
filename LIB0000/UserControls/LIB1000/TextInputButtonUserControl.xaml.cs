using System.Windows.Controls;
using System.Windows.Media;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for TextInputTextUserControl.xaml
    /// </summary>
    public partial class TextInputButtonUserControl : UserControl
    {
        public TextInputButtonUserControl()
        {
            InitializeComponent();
        }

        public string TextLeft
        {
            get { return (string)GetValue(TextLeftProperty); }
            set { SetValue(TextLeftProperty, value); }
        }

        public static readonly DependencyProperty TextLeftProperty = DependencyProperty.Register("TextLeft", typeof(string), typeof(TextInputButtonUserControl));

        public string TextRight
        {
            get { return (string)GetValue(TextRightProperty); }
            set { SetValue(TextRightProperty, value); }
        }

        public static readonly DependencyProperty TextRightProperty = DependencyProperty.Register("TextRight", typeof(string), typeof(TextInputButtonUserControl));

        public RelayCommand CommandRight
        {
            get { return (RelayCommand)GetValue(CommandRightProperty); }
            set { SetValue(CommandRightProperty, value); }
        }

        public static readonly DependencyProperty CommandRightProperty = DependencyProperty.Register("CommandRight", typeof(RelayCommand), typeof(TextInputButtonUserControl));

        public bool VisibilityRight
        {
            get { return (bool)GetValue(VisibilityRightProperty); }
            set { SetValue(VisibilityRightProperty, value); }
        }

        public static readonly DependencyProperty VisibilityRightProperty = DependencyProperty.Register("VisibilityRight", typeof(bool), typeof(TextInputButtonUserControl)
        );

        //Button 1 image
        public ImageSource ImageRight
        {
            get { return (ImageSource)GetValue(ImageRightProperty); }
            set { SetValue(ImageRightProperty, value); }
        }

        public static readonly DependencyProperty ImageRightProperty = DependencyProperty.Register("ImageRight", typeof(ImageSource),
            typeof(TextInputButtonUserControl)
        );


        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(string), typeof(TextInputButtonUserControl));
    }
}
