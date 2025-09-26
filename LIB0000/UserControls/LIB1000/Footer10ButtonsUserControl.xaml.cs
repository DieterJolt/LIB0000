using System.Windows.Controls;
using System.Windows.Media;

namespace LIB0000
{
    public partial class Footer10ButtonsUserControl : UserControl
    {
        public Footer10ButtonsUserControl()
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

        public static readonly DependencyProperty Button1TextProperty = DependencyProperty.Register("Button1Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 1 Visibility
        public bool Button1Visibility
        {
            get { return (bool)GetValue(Button1VisibilityProperty); }
            set { SetValue(Button1VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button1VisibilityProperty = DependencyProperty.Register("Button1Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
        );

        //Button 1 image
        public ImageSource Button1Image
        {
            get { return (ImageSource)GetValue(Button1ImageProperty); }
            set { SetValue(Button1ImageProperty, value); }
        }

        public static readonly DependencyProperty Button1ImageProperty = DependencyProperty.Register("Button1Image", typeof(ImageSource),
            typeof(Footer10ButtonsUserControl)
        );

        //Button 1 command
        public RelayCommand Button1Command
        {
            get { return (RelayCommand)GetValue(Button1CommandProperty); }
            set { SetValue(Button1CommandProperty, value); }
        }

        public static readonly DependencyProperty Button1CommandProperty = DependencyProperty.Register("Button1Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button1BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button1BackgroundColorProperty); }
            set { SetValue(Button1BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button1BackgroundColorProperty = DependencyProperty.Register("Button1BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 2

        //Button 2 text
        public string Button2Text
        {
            get { return (string)GetValue(Button2TextProperty); }
            set { SetValue(Button2TextProperty, value); }
        }

        public static readonly DependencyProperty Button2TextProperty = DependencyProperty.Register("Button2Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 2 Visibility
        public bool Button2Visibility
        {
            get { return (bool)GetValue(Button2VisibilityProperty); }
            set { SetValue(Button2VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button2VisibilityProperty = DependencyProperty.Register("Button2Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
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
            typeof(Footer10ButtonsUserControl)
        );

        //Button 2 command
        public RelayCommand Button2Command
        {
            get { return (RelayCommand)GetValue(Button2CommandProperty); }
            set { SetValue(Button2CommandProperty, value); }
        }

        public static readonly DependencyProperty Button2CommandProperty = DependencyProperty.Register("Button2Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button2BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button2BackgroundColorProperty); }
            set { SetValue(Button2BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button2BackgroundColorProperty = DependencyProperty.Register("Button2BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 3

        //Button 3 text
        public string Button3Text
        {
            get { return (string)GetValue(Button3TextProperty); }
            set { SetValue(Button3TextProperty, value); }
        }

        public static readonly DependencyProperty Button3TextProperty = DependencyProperty.Register("Button3Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 3 Visibility
        public bool Button3Visibility
        {
            get { return (bool)GetValue(Button3VisibilityProperty); }
            set { SetValue(Button3VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button3VisibilityProperty = DependencyProperty.Register("Button3Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
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
            typeof(Footer10ButtonsUserControl)
        );

        //Button 3 command
        public RelayCommand Button3Command
        {
            get { return (RelayCommand)GetValue(Button3CommandProperty); }
            set { SetValue(Button3CommandProperty, value); }
        }

        public static readonly DependencyProperty Button3CommandProperty = DependencyProperty.Register("Button3Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button3BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button3BackgroundColorProperty); }
            set { SetValue(Button3BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button3BackgroundColorProperty = DependencyProperty.Register("Button3BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 4

        //Button 4 text
        public string Button4Text
        {
            get { return (string)GetValue(Button4TextProperty); }
            set { SetValue(Button4TextProperty, value); }
        }

        public static readonly DependencyProperty Button4TextProperty = DependencyProperty.Register("Button4Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 4 Visibility
        public bool Button4Visibility
        {
            get { return (bool)GetValue(Button4VisibilityProperty); }
            set { SetValue(Button4VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button4VisibilityProperty = DependencyProperty.Register("Button4Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
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
            typeof(Footer10ButtonsUserControl)
        );

        //Button 4 command
        public RelayCommand Button4Command
        {
            get { return (RelayCommand)GetValue(Button4CommandProperty); }
            set { SetValue(Button4CommandProperty, value); }
        }

        public static readonly DependencyProperty Button4CommandProperty = DependencyProperty.Register("Button4Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button4BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button4BackgroundColorProperty); }
            set { SetValue(Button4BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button4BackgroundColorProperty = DependencyProperty.Register("Button4BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 5

        //Button 5 text
        public string Button5Text
        {
            get { return (string)GetValue(Button5TextProperty); }
            set { SetValue(Button5TextProperty, value); }
        }

        public static readonly DependencyProperty Button5TextProperty = DependencyProperty.Register("Button5Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 5 Visibility
        public bool Button5Visibility
        {
            get { return (bool)GetValue(Button5VisibilityProperty); }
            set { SetValue(Button5VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button5VisibilityProperty = DependencyProperty.Register("Button5Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
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
            typeof(Footer10ButtonsUserControl)
        );

        //Button 5 command
        public RelayCommand Button5Command
        {
            get { return (RelayCommand)GetValue(Button5CommandProperty); }
            set { SetValue(Button5CommandProperty, value); }
        }

        public static readonly DependencyProperty Button5CommandProperty = DependencyProperty.Register("Button5Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button5BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button5BackgroundColorProperty); }
            set { SetValue(Button5BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button5BackgroundColorProperty = DependencyProperty.Register("Button5BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 6

        //Button 6 text
        public string Button6Text
        {
            get { return (string)GetValue(Button6TextProperty); }
            set { SetValue(Button6TextProperty, value); }
        }

        public static readonly DependencyProperty Button6TextProperty = DependencyProperty.Register("Button6Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 6 Visibility
        public bool Button6Visibility
        {
            get { return (bool)GetValue(Button6VisibilityProperty); }
            set { SetValue(Button6VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button6VisibilityProperty = DependencyProperty.Register("Button6Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
        );

        //Button 6 image
        public ImageSource Button6Image
        {
            get { return (ImageSource)GetValue(Button6ImageProperty); }
            set { SetValue(Button6ImageProperty, value); }
        }

        public static readonly DependencyProperty Button6ImageProperty = DependencyProperty.Register(
            "Button6Image",
            typeof(ImageSource),
            typeof(Footer10ButtonsUserControl)
        );

        //Button 6 command
        public RelayCommand Button6Command
        {
            get { return (RelayCommand)GetValue(Button6CommandProperty); }
            set { SetValue(Button6CommandProperty, value); }
        }

        public static readonly DependencyProperty Button6CommandProperty = DependencyProperty.Register("Button6Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button6BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button6BackgroundColorProperty); }
            set { SetValue(Button6BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button6BackgroundColorProperty = DependencyProperty.Register("Button6BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 7

        //Button 7 text
        public string Button7Text
        {
            get { return (string)GetValue(Button7TextProperty); }
            set { SetValue(Button7TextProperty, value); }
        }

        public static readonly DependencyProperty Button7TextProperty = DependencyProperty.Register("Button7Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 7 Visibility
        public bool Button7Visibility
        {
            get { return (bool)GetValue(Button7VisibilityProperty); }
            set { SetValue(Button7VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button7VisibilityProperty = DependencyProperty.Register("Button7Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
        );

        //Button 7 image
        public ImageSource Button7Image
        {
            get { return (ImageSource)GetValue(Button7ImageProperty); }
            set { SetValue(Button7ImageProperty, value); }
        }

        public static readonly DependencyProperty Button7ImageProperty = DependencyProperty.Register(
            "Button7Image",
            typeof(ImageSource),
            typeof(Footer10ButtonsUserControl)
        );

        //Button 7 command
        public RelayCommand Button7Command
        {
            get { return (RelayCommand)GetValue(Button7CommandProperty); }
            set { SetValue(Button7CommandProperty, value); }
        }

        public static readonly DependencyProperty Button7CommandProperty = DependencyProperty.Register("Button7Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button7BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button7BackgroundColorProperty); }
            set { SetValue(Button7BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button7BackgroundColorProperty = DependencyProperty.Register("Button7BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 8

        //Button 2 text
        public string Button8Text
        {
            get { return (string)GetValue(Button8TextProperty); }
            set { SetValue(Button8TextProperty, value); }
        }

        public static readonly DependencyProperty Button8TextProperty = DependencyProperty.Register("Button8Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 8 Visibility
        public bool Button8Visibility
        {
            get { return (bool)GetValue(Button8VisibilityProperty); }
            set { SetValue(Button8VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button8VisibilityProperty = DependencyProperty.Register("Button8Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
        );

        //Button 8 image
        public ImageSource Button8Image
        {
            get { return (ImageSource)GetValue(Button8ImageProperty); }
            set { SetValue(Button8ImageProperty, value); }
        }

        public static readonly DependencyProperty Button8ImageProperty = DependencyProperty.Register(
            "Button8Image",
            typeof(ImageSource),
            typeof(Footer10ButtonsUserControl)
        );

        //Button 8 command
        public RelayCommand Button8Command
        {
            get { return (RelayCommand)GetValue(Button8CommandProperty); }
            set { SetValue(Button8CommandProperty, value); }
        }

        public static readonly DependencyProperty Button8CommandProperty = DependencyProperty.Register("Button8Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button8BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button8BackgroundColorProperty); }
            set { SetValue(Button8BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button8BackgroundColorProperty = DependencyProperty.Register("Button8BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 9

        //Button 9 text
        public string Button9Text
        {
            get { return (string)GetValue(Button9TextProperty); }
            set { SetValue(Button9TextProperty, value); }
        }

        public static readonly DependencyProperty Button9TextProperty = DependencyProperty.Register("Button9Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 9 Visibility
        public bool Button9Visibility
        {
            get { return (bool)GetValue(Button9VisibilityProperty); }
            set { SetValue(Button9VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button9VisibilityProperty = DependencyProperty.Register("Button9Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
        );

        //Button 9 image
        public ImageSource Button9Image
        {
            get { return (ImageSource)GetValue(Button9ImageProperty); }
            set { SetValue(Button9ImageProperty, value); }
        }

        public static readonly DependencyProperty Button9ImageProperty = DependencyProperty.Register(
            "Button9Image",
            typeof(ImageSource),
            typeof(Footer10ButtonsUserControl)
        );

        //Button 9 command
        public RelayCommand Button9Command
        {
            get { return (RelayCommand)GetValue(Button9CommandProperty); }
            set { SetValue(Button9CommandProperty, value); }
        }

        public static readonly DependencyProperty Button9CommandProperty = DependencyProperty.Register("Button9Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button9BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button9BackgroundColorProperty); }
            set { SetValue(Button9BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button9BackgroundColorProperty = DependencyProperty.Register("Button9BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 10

        //Button 10 text
        public string Button10Text
        {
            get { return (string)GetValue(Button10TextProperty); }
            set { SetValue(Button10TextProperty, value); }
        }

        public static readonly DependencyProperty Button10TextProperty = DependencyProperty.Register("Button10Text", typeof(string), typeof(Footer10ButtonsUserControl));

        //Button 10 Visibility
        public bool Button10Visibility
        {
            get { return (bool)GetValue(Button10VisibilityProperty); }
            set { SetValue(Button10VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button10VisibilityProperty = DependencyProperty.Register("Button10Visibility", typeof(bool), typeof(Footer10ButtonsUserControl)
        );

        //Button 10 image
        public ImageSource Button10Image
        {
            get { return (ImageSource)GetValue(Button10ImageProperty); }
            set { SetValue(Button10ImageProperty, value); }
        }

        public static readonly DependencyProperty Button10ImageProperty = DependencyProperty.Register(
            "Button10Image",
            typeof(ImageSource),
            typeof(Footer10ButtonsUserControl)
        );

        //Button 10 command
        public RelayCommand Button10Command
        {
            get { return (RelayCommand)GetValue(Button10CommandProperty); }
            set { SetValue(Button10CommandProperty, value); }
        }

        public static readonly DependencyProperty Button10CommandProperty = DependencyProperty.Register("Button10Command", typeof(RelayCommand), typeof(Footer10ButtonsUserControl));

        public SolidColorBrush Button10BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button10BackgroundColorProperty); }
            set { SetValue(Button10BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button10BackgroundColorProperty = DependencyProperty.Register("Button10BackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

    }
}
