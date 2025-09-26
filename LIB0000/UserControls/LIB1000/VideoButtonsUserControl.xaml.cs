using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace LIB0000
{
    public partial class VideoButtonsUserControl : UserControl
    {
        public VideoButtonsUserControl()
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

        public static readonly DependencyProperty Button1TextProperty = DependencyProperty.Register("Button1Text", typeof(string), typeof(VideoButtonsUserControl));

        //Button 1 Textcollor
        public SolidColorBrush Button1TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button1TextColorProperty); }
            set { SetValue(Button1TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button1TextColorProperty = DependencyProperty.Register("Button1TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 1 Visibility
        public bool Button1Visibility
        {
            get { return (bool)GetValue(Button1VisibilityProperty); }
            set { SetValue(Button1VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button1VisibilityProperty = DependencyProperty.Register("Button1Visibility", typeof(bool), typeof(VideoButtonsUserControl)
        );

        //Button 1 icon
        public FontAwesome.Sharp.IconChar Button1Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(Button1IconProperty); }
            set { SetValue(Button1IconProperty, value); }
        }

        public static readonly DependencyProperty Button1IconProperty = DependencyProperty.Register("Button1Icon", typeof(FontAwesome.Sharp.IconChar), typeof(VideoButtonsUserControl)
        );

        //Button 1 Iconcollor
        public SolidColorBrush Button1IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button1IconColorProperty); }
            set { SetValue(Button1IconColorProperty, value); }
        }

        public static readonly DependencyProperty Button1IconColorProperty = DependencyProperty.Register("Button1IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 1 command
        public RelayCommand Button1Command
        {
            get { return (RelayCommand)GetValue(Button1CommandProperty); }
            set { SetValue(Button1CommandProperty, value); }
        }

        public static readonly DependencyProperty Button1CommandProperty = DependencyProperty.Register("Button1Command", typeof(RelayCommand), typeof(VideoButtonsUserControl));

        public SolidColorBrush Button1BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button1BackgroundColorProperty); }
            set { SetValue(Button1BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button1BackgroundColorProperty = DependencyProperty.Register("Button1BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2B2B2B"))));

        #endregion

        #region Button 2

        //Button 2 text
        public string Button2Text
        {
            get { return (string)GetValue(Button2TextProperty); }
            set { SetValue(Button2TextProperty, value); }
        }

        public static readonly DependencyProperty Button2TextProperty = DependencyProperty.Register("Button2Text", typeof(string), typeof(VideoButtonsUserControl));

        //Button 2 Textcollor
        public SolidColorBrush Button2TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button2TextColorProperty); }
            set { SetValue(Button2TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button2TextColorProperty = DependencyProperty.Register("Button2TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 2 Visibility
        public bool Button2Visibility
        {
            get { return (bool)GetValue(Button2VisibilityProperty); }
            set { SetValue(Button2VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button2VisibilityProperty = DependencyProperty.Register("Button2Visibility", typeof(bool), typeof(VideoButtonsUserControl)
        );

        //Button 2 icon
        public FontAwesome.Sharp.IconChar Button2Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(Button2IconProperty); }
            set { SetValue(Button2IconProperty, value); }
        }

        public static readonly DependencyProperty Button2IconProperty = DependencyProperty.Register("Button2Icon", typeof(FontAwesome.Sharp.IconChar), typeof(VideoButtonsUserControl)
        );

        //Button 2 Iconcollor
        public SolidColorBrush Button2IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button2IconColorProperty); }
            set { SetValue(Button2IconColorProperty, value); }
        }

        public static readonly DependencyProperty Button2IconColorProperty = DependencyProperty.Register("Button2IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 2 command
        public RelayCommand Button2Command
        {
            get { return (RelayCommand)GetValue(Button2CommandProperty); }
            set { SetValue(Button2CommandProperty, value); }
        }

        public static readonly DependencyProperty Button2CommandProperty = DependencyProperty.Register("Button2Command", typeof(RelayCommand), typeof(VideoButtonsUserControl));

        public SolidColorBrush Button2BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button2BackgroundColorProperty); }
            set { SetValue(Button2BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button2BackgroundColorProperty = DependencyProperty.Register("Button2BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2B2B2B"))));

        #endregion

        #region Button 3

        //Button 3 text
        public string Button3Text
        {
            get { return (string)GetValue(Button3TextProperty); }
            set { SetValue(Button3TextProperty, value); }
        }

        public static readonly DependencyProperty Button3TextProperty = DependencyProperty.Register("Button3Text", typeof(string), typeof(VideoButtonsUserControl));

        //Button 3 Textcollor
        public SolidColorBrush Button3TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button3TextColorProperty); }
            set { SetValue(Button3TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button3TextColorProperty = DependencyProperty.Register("Button3TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 3 Visibility
        public bool Button3Visibility
        {
            get { return (bool)GetValue(Button3VisibilityProperty); }
            set { SetValue(Button3VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button3VisibilityProperty = DependencyProperty.Register("Button3Visibility", typeof(bool), typeof(VideoButtonsUserControl)
        );

        //Button 3 icon
        public FontAwesome.Sharp.IconChar Button3Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(Button3IconProperty); }
            set { SetValue(Button3IconProperty, value); }
        }

        public static readonly DependencyProperty Button3IconProperty = DependencyProperty.Register("Button3Icon", typeof(FontAwesome.Sharp.IconChar), typeof(VideoButtonsUserControl)
        );

        //Button 3 Iconcollor
        public SolidColorBrush Button3IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button3IconColorProperty); }
            set { SetValue(Button3IconColorProperty, value); }
        }

        public static readonly DependencyProperty Button3IconColorProperty = DependencyProperty.Register("Button3IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 3 command
        public RelayCommand Button3Command
        {
            get { return (RelayCommand)GetValue(Button3CommandProperty); }
            set { SetValue(Button3CommandProperty, value); }
        }

        public static readonly DependencyProperty Button3CommandProperty = DependencyProperty.Register("Button3Command", typeof(RelayCommand), typeof(VideoButtonsUserControl));

        public SolidColorBrush Button3BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button3BackgroundColorProperty); }
            set { SetValue(Button3BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button3BackgroundColorProperty = DependencyProperty.Register("Button3BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2B2B2B"))));
        #endregion

        #region Button 4

        //Button 4 text
        public string Button4Text
        {
            get { return (string)GetValue(Button4TextProperty); }
            set { SetValue(Button4TextProperty, value); }
        }

        public static readonly DependencyProperty Button4TextProperty = DependencyProperty.Register("Button4Text", typeof(string), typeof(VideoButtonsUserControl));

        //Button 4 Textcollor
        public SolidColorBrush Button4TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button4TextColorProperty); }
            set { SetValue(Button4TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button4TextColorProperty = DependencyProperty.Register("Button4TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 4 Visibility
        public bool Button4Visibility
        {
            get { return (bool)GetValue(Button4VisibilityProperty); }
            set { SetValue(Button4VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button4VisibilityProperty = DependencyProperty.Register("Button4Visibility", typeof(bool), typeof(VideoButtonsUserControl)
        );

        //Button 4 icon
        public FontAwesome.Sharp.IconChar Button4Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(Button4IconProperty); }
            set { SetValue(Button4IconProperty, value); }
        }

        public static readonly DependencyProperty Button4IconProperty = DependencyProperty.Register("Button4Icon", typeof(FontAwesome.Sharp.IconChar), typeof(VideoButtonsUserControl)
        );

        //Button 4 Iconcollor
        public SolidColorBrush Button4IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button4IconColorProperty); }
            set { SetValue(Button4IconColorProperty, value); }
        }

        public static readonly DependencyProperty Button4IconColorProperty = DependencyProperty.Register("Button4IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 4 command
        public RelayCommand Button4Command
        {
            get { return (RelayCommand)GetValue(Button4CommandProperty); }
            set { SetValue(Button4CommandProperty, value); }
        }

        public static readonly DependencyProperty Button4CommandProperty = DependencyProperty.Register("Button4Command", typeof(RelayCommand), typeof(VideoButtonsUserControl));

        public SolidColorBrush Button4BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button4BackgroundColorProperty); }
            set { SetValue(Button4BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button4BackgroundColorProperty = DependencyProperty.Register("Button4BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2B2B2B"))));

        #endregion

        #region Button 5

        //Button 5 text
        public string Button5Text
        {
            get { return (string)GetValue(Button5TextProperty); }
            set { SetValue(Button5TextProperty, value); }
        }

        public static readonly DependencyProperty Button5TextProperty = DependencyProperty.Register("Button5Text", typeof(string), typeof(VideoButtonsUserControl));

        //Button 5 Textcollor
        public SolidColorBrush Button5TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button5TextColorProperty); }
            set { SetValue(Button5TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button5TextColorProperty = DependencyProperty.Register("Button5TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 5 Visibility
        public bool Button5Visibility
        {
            get { return (bool)GetValue(Button5VisibilityProperty); }
            set { SetValue(Button5VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button5VisibilityProperty = DependencyProperty.Register("Button5Visibility", typeof(bool), typeof(VideoButtonsUserControl)
        );

        //Button 5 icon
        public FontAwesome.Sharp.IconChar Button5Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(Button5IconProperty); }
            set { SetValue(Button5IconProperty, value); }
        }

        public static readonly DependencyProperty Button5IconProperty = DependencyProperty.Register("Button5Icon", typeof(FontAwesome.Sharp.IconChar), typeof(VideoButtonsUserControl)
        );

        //Button 5 Iconcollor
        public SolidColorBrush Button5IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button5IconColorProperty); }
            set { SetValue(Button5IconColorProperty, value); }
        }

        public static readonly DependencyProperty Button5IconColorProperty = DependencyProperty.Register("Button5IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 5 command
        public RelayCommand Button5Command
        {
            get { return (RelayCommand)GetValue(Button5CommandProperty); }
            set { SetValue(Button5CommandProperty, value); }
        }

        public static readonly DependencyProperty Button5CommandProperty = DependencyProperty.Register("Button5Command", typeof(RelayCommand), typeof(VideoButtonsUserControl));

        public SolidColorBrush Button5BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button5BackgroundColorProperty); }
            set { SetValue(Button5BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button5BackgroundColorProperty = DependencyProperty.Register("Button5BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2B2B2B"))));

        #endregion

        #region Button 6

        //Button 6 text
        public string Button6Text
        {
            get { return (string)GetValue(Button6TextProperty); }
            set { SetValue(Button6TextProperty, value); }
        }

        public static readonly DependencyProperty Button6TextProperty = DependencyProperty.Register("Button6Text", typeof(string), typeof(VideoButtonsUserControl));

        //Button 6 Textcollor
        public SolidColorBrush Button6TextColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button6TextColorProperty); }
            set { SetValue(Button6TextColorProperty, value); }
        }

        public static readonly DependencyProperty Button6TextColorProperty = DependencyProperty.Register("Button6TextColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 6 Visibility
        public bool Button6Visibility
        {
            get { return (bool)GetValue(Button6VisibilityProperty); }
            set { SetValue(Button6VisibilityProperty, value); }
        }

        public static readonly DependencyProperty Button6VisibilityProperty = DependencyProperty.Register("Button6Visibility", typeof(bool), typeof(VideoButtonsUserControl)
        );

        //Button 6 icon
        public FontAwesome.Sharp.IconChar Button6Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(Button6IconProperty); }
            set { SetValue(Button6IconProperty, value); }
        }

        public static readonly DependencyProperty Button6IconProperty = DependencyProperty.Register("Button6Icon", typeof(FontAwesome.Sharp.IconChar), typeof(VideoButtonsUserControl)
        );

        //Button 6 Iconcollor
        public SolidColorBrush Button6IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(Button6IconColorProperty); }
            set { SetValue(Button6IconColorProperty, value); }
        }

        public static readonly DependencyProperty Button6IconColorProperty = DependencyProperty.Register("Button6IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(VideoButtonsUserControl)
        );

        //Button 6 command
        public RelayCommand Button6Command
        {
            get { return (RelayCommand)GetValue(Button6CommandProperty); }
            set { SetValue(Button6CommandProperty, value); }
        }

        public static readonly DependencyProperty Button6CommandProperty = DependencyProperty.Register("Button6Command", typeof(RelayCommand), typeof(VideoButtonsUserControl));

        public SolidColorBrush Button6BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button6BackgroundColorProperty); }
            set { SetValue(Button6BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button6BackgroundColorProperty = DependencyProperty.Register("Button6BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF2B2B2B"))));

        #endregion



    }
}
