using System.Windows.Controls;
using System.Windows.Media;


namespace LIB0000
{
    public partial class Footer5ButtonsUserControl : UserControl
    {
        public Footer5ButtonsUserControl()
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

        public static readonly DependencyProperty Button1TextProperty = DependencyProperty.Register("Button1Text", typeof(string), typeof(Footer5ButtonsUserControl));

        //Button 1 Textcolor
        public SolidColorBrush Button1TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button1TextColorProperty); }
            set { SetValue(Button1TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button1TextColorProperty = DependencyProperty.Register("Button1TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(Footer5ButtonsUserControl),
        new PropertyMetadata(new SolidColorBrush(Colors.White)) // ← standaardwaarde
    );

        //Button 1 Visibility
        public bool Button1Visibility
        {
            get { return (bool)GetValue(Button1VisibilityProperty); }
            set { SetValue(Button1VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button1VisibilityProperty = DependencyProperty.Register("Button1Visibility", typeof(bool), typeof(Footer5ButtonsUserControl)
        );

        //Button 1 image
        public ImageSource Button1Image
        {
            get { return (ImageSource)GetValue(Button1ImageProperty); }
            set { SetValue(Button1ImageProperty, value); }
        }

        public static readonly DependencyProperty Button1ImageProperty = DependencyProperty.Register("Button1Image", typeof(ImageSource),
            typeof(Footer5ButtonsUserControl)
        );

        //Button 1 command
        public RelayCommand Button1Command
        {
            get { return (RelayCommand)GetValue(Button1CommandProperty); }
            set { SetValue(Button1CommandProperty, value); }
        }

        public static readonly DependencyProperty Button1CommandProperty = DependencyProperty.Register("Button1Command", typeof(RelayCommand), typeof(Footer5ButtonsUserControl));

        #endregion

        #region Button 2

        //Button 2 text
        public string Button2Text
        {
            get { return (string)GetValue(Button2TextProperty); }
            set { SetValue(Button2TextProperty, value); }
        }

        public static readonly DependencyProperty Button2TextProperty = DependencyProperty.Register("Button2Text", typeof(string), typeof(Footer5ButtonsUserControl));

        //Button 2 Textcolor
        public SolidColorBrush Button2TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button2TextColorProperty); }
            set { SetValue(Button2TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button2TextColorProperty = DependencyProperty.Register("Button2TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(Footer5ButtonsUserControl),
        new PropertyMetadata(new SolidColorBrush(Colors.White)) // ← standaardwaarde
    );

        //Button 2 Visibility
        public bool Button2Visibility
        {
            get { return (bool)GetValue(Button2VisibilityProperty); }
            set { SetValue(Button2VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button2VisibilityProperty = DependencyProperty.Register("Button2Visibility", typeof(bool), typeof(Footer5ButtonsUserControl)
        );

        //Button 2 image
        public ImageSource Button2Image
        {
            get { return (ImageSource)GetValue(Button2ImageProperty); }
            set { SetValue(Button2ImageProperty, value); }
        }

        public static readonly DependencyProperty Button2ImageProperty = DependencyProperty.Register(
            "Button2Image",
            typeof(ImageSource),
            typeof(Footer5ButtonsUserControl)
        );

        //Button 2 command
        public RelayCommand Button2Command
        {
            get { return (RelayCommand)GetValue(Button2CommandProperty); }
            set { SetValue(Button2CommandProperty, value); }
        }

        public static readonly DependencyProperty Button2CommandProperty = DependencyProperty.Register("Button2Command", typeof(RelayCommand), typeof(Footer5ButtonsUserControl));

        #endregion

        #region Button 3

        //Button 3 text
        public string Button3Text
        {
            get { return (string)GetValue(Button3TextProperty); }
            set { SetValue(Button3TextProperty, value); }
        }

        public static readonly DependencyProperty Button3TextProperty = DependencyProperty.Register("Button3Text", typeof(string), typeof(Footer5ButtonsUserControl));

        //Button 3 Textcolor
        public SolidColorBrush Button3TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button3TextColorProperty); }
            set { SetValue(Button3TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button3TextColorProperty = DependencyProperty.Register("Button3TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(Footer5ButtonsUserControl),
        new PropertyMetadata(new SolidColorBrush(Colors.White)) // ← standaardwaarde
    );

        //Button 3 Visibility
        public bool Button3Visibility
        {
            get { return (bool)GetValue(Button3VisibilityProperty); }
            set { SetValue(Button3VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button3VisibilityProperty = DependencyProperty.Register("Button3Visibility", typeof(bool), typeof(Footer5ButtonsUserControl)
        );

        //Button 3 image
        public ImageSource Button3Image
        {
            get { return (ImageSource)GetValue(Button3ImageProperty); }
            set { SetValue(Button3ImageProperty, value); }
        }

        public static readonly DependencyProperty Button3ImageProperty = DependencyProperty.Register(
            "Button3Image",
            typeof(ImageSource),
            typeof(Footer5ButtonsUserControl)
        );

        //Button 3 command
        public RelayCommand Button3Command
        {
            get { return (RelayCommand)GetValue(Button3CommandProperty); }
            set { SetValue(Button3CommandProperty, value); }
        }

        public static readonly DependencyProperty Button3CommandProperty = DependencyProperty.Register("Button3Command", typeof(RelayCommand), typeof(Footer5ButtonsUserControl));

        #endregion

        #region Button 4

        //Button 4 text
        public string Button4Text
        {
            get { return (string)GetValue(Button4TextProperty); }
            set { SetValue(Button4TextProperty, value); }
        }

        public static readonly DependencyProperty Button4TextProperty = DependencyProperty.Register("Button4Text", typeof(string), typeof(Footer5ButtonsUserControl));

        //Button 1 Textcolor
        public SolidColorBrush Button4TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button4TextColorProperty); }
            set { SetValue(Button4TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button4TextColorProperty = DependencyProperty.Register("Button4TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(Footer5ButtonsUserControl),
        new PropertyMetadata(new SolidColorBrush(Colors.White)) // ← standaardwaarde
    );

        //Button 4 Visibility
        public bool Button4Visibility
        {
            get { return (bool)GetValue(Button4VisibilityProperty); }
            set { SetValue(Button4VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button4VisibilityProperty = DependencyProperty.Register("Button4Visibility", typeof(bool), typeof(Footer5ButtonsUserControl)
        );

        //Button 4 image
        public ImageSource Button4Image
        {
            get { return (ImageSource)GetValue(Button4ImageProperty); }
            set { SetValue(Button4ImageProperty, value); }
        }

        public static readonly DependencyProperty Button4ImageProperty = DependencyProperty.Register(
            "Button4Image",
            typeof(ImageSource),
            typeof(Footer5ButtonsUserControl)
        );

        //Button 4 command
        public RelayCommand Button4Command
        {
            get { return (RelayCommand)GetValue(Button4CommandProperty); }
            set { SetValue(Button4CommandProperty, value); }
        }

        public static readonly DependencyProperty Button4CommandProperty = DependencyProperty.Register("Button4Command", typeof(RelayCommand), typeof(Footer5ButtonsUserControl));

        #endregion

        #region Button 5

        //Button 5 text
        public string Button5Text
        {
            get { return (string)GetValue(Button5TextProperty); }
            set { SetValue(Button5TextProperty, value); }
        }

        public static readonly DependencyProperty Button5TextProperty = DependencyProperty.Register("Button5Text", typeof(string), typeof(Footer5ButtonsUserControl));

        //Button 5 Textcolor
        public SolidColorBrush Button5TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button5TextColorProperty); }
            set { SetValue(Button5TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button5TextColorProperty = DependencyProperty.Register("Button5TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(Footer5ButtonsUserControl),
        new PropertyMetadata(new SolidColorBrush(Colors.White)) // ← standaardwaarde
    );

        //Button 5 Visibility
        public bool Button5Visibility
        {
            get { return (bool)GetValue(Button5VisibilityProperty); }
            set { SetValue(Button5VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button5VisibilityProperty = DependencyProperty.Register("Button5Visibility", typeof(bool), typeof(Footer5ButtonsUserControl)
        );

        //Button 5 image
        public ImageSource Button5Image
        {
            get { return (ImageSource)GetValue(Button5ImageProperty); }
            set { SetValue(Button5ImageProperty, value); }
        }

        public static readonly DependencyProperty Button5ImageProperty = DependencyProperty.Register(
            "Button5Image",
            typeof(ImageSource),
            typeof(Footer5ButtonsUserControl)
        );

        //Button 5 command
        public RelayCommand Button5Command
        {
            get { return (RelayCommand)GetValue(Button5CommandProperty); }
            set { SetValue(Button5CommandProperty, value); }
        }

        public static readonly DependencyProperty Button5CommandProperty = DependencyProperty.Register("Button5Command", typeof(RelayCommand), typeof(Footer5ButtonsUserControl));

        #endregion

    }
}
