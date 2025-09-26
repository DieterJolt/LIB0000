using System.Windows.Controls;
using System.Windows.Media;

namespace LIB0000
{
    public partial class ButtonImageUserControl : UserControl
    {
        public ButtonImageUserControl()
        {
            InitializeComponent();
        }


        #region Button 1

        //Button 1 text
        public string Button1Text
        {
            get { return (string)GetValue(Button1TextProperty); }
            set { SetValue(Button1TextProperty, value); }
        }

        public static readonly DependencyProperty Button1TextProperty = DependencyProperty.Register("Button1Text", typeof(string), typeof(ButtonImageUserControl));

        //Button 1 Visibility
        public bool Button1Visibility
        {
            get { return (bool)GetValue(Button1VisibilityProperty); }
            set { SetValue(Button1VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button1VisibilityProperty = DependencyProperty.Register("Button1Visibility", typeof(bool), typeof(ButtonImageUserControl)
        );

        //Button 1 image
        public ImageSource Button1Image
        {
            get { return (ImageSource)GetValue(Button1ImageProperty); }
            set { SetValue(Button1ImageProperty, value); }
        }

        public static readonly DependencyProperty Button1ImageProperty = DependencyProperty.Register("Button1Image", typeof(ImageSource),
            typeof(ButtonImageUserControl)
        );

        //Button 1 command
        public RelayCommand Button1Command
        {
            get { return (RelayCommand)GetValue(Button1CommandProperty); }
            set { SetValue(Button1CommandProperty, value); }
        }

        public static readonly DependencyProperty Button1CommandProperty = DependencyProperty.Register("Button1Command", typeof(RelayCommand), typeof(ButtonImageUserControl));

        #endregion



    }
}
