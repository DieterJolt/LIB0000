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

        public SolidColorBrush Button1BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button1BackgroundColorProperty); }
            set { SetValue(Button1BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button1BackgroundColorProperty = DependencyProperty.Register("Button1BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 2

        //Button 2 text
        public string Button2Text
        {
            get { return (string)GetValue(Button2TextProperty); }
            set { SetValue(Button2TextProperty, value); }
        }

        public static readonly DependencyProperty Button2TextProperty = DependencyProperty.Register("Button2Text", typeof(string), typeof(Footer5ButtonsUserControl));

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

        public SolidColorBrush Button2BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button2BackgroundColorProperty); }
            set { SetValue(Button2BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button2BackgroundColorProperty = DependencyProperty.Register("Button2BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 3

        //Button 3 text
        public string Button3Text
        {
            get { return (string)GetValue(Button3TextProperty); }
            set { SetValue(Button3TextProperty, value); }
        }

        public static readonly DependencyProperty Button3TextProperty = DependencyProperty.Register("Button3Text", typeof(string), typeof(Footer5ButtonsUserControl));

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

        public SolidColorBrush Button3BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button3BackgroundColorProperty); }
            set { SetValue(Button3BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button3BackgroundColorProperty = DependencyProperty.Register("Button3BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 4

        //Button 4 text
        public string Button4Text
        {
            get { return (string)GetValue(Button4TextProperty); }
            set { SetValue(Button4TextProperty, value); }
        }

        public static readonly DependencyProperty Button4TextProperty = DependencyProperty.Register("Button4Text", typeof(string), typeof(Footer5ButtonsUserControl));

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

        public SolidColorBrush Button4BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button4BackgroundColorProperty); }
            set { SetValue(Button4BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button4BackgroundColorProperty = DependencyProperty.Register("Button4BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

        #region Button 5

        //Button 5 text
        public string Button5Text
        {
            get { return (string)GetValue(Button5TextProperty); }
            set { SetValue(Button5TextProperty, value); }
        }

        public static readonly DependencyProperty Button5TextProperty = DependencyProperty.Register("Button5Text", typeof(string), typeof(Footer5ButtonsUserControl));

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

        public SolidColorBrush Button5BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(Button5BackgroundColorProperty); }
            set { SetValue(Button5BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty Button5BackgroundColorProperty = DependencyProperty.Register("Button5BackgroundColor", typeof(SolidColorBrush), typeof(Footer5ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion

    }
}
