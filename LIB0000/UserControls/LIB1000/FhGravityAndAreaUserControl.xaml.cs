using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class FhGravityAndAreaUserControl : UserControl
    {
        #region Commands
        #endregion

        #region Constructor

        public FhGravityAndAreaUserControl()
        {
            InitializeComponent();
            var dpd = DependencyPropertyDescriptor.FromProperty(Image.SourceProperty, typeof(Image));
            dpd.AddValueChanged(Image, OnImageSourceChanged);
        }

        #endregion

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Methods

        private void OnImageSourceChanged(object sender, EventArgs e)
        {
            // Forceer een layout/update
            Image.InvalidateMeasure();
            Image.InvalidateArrange();
            cnv.InvalidateVisual();
        }

        #endregion

        #region Properties

        //Image
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(FhGravityAndAreaUserControl));

        public double CameraResolutionY
        {
            get { return (double)GetValue(CameraResolutionYProperty); }
            set { SetValue(CameraResolutionYProperty, value); }
        }

        public static readonly DependencyProperty CameraResolutionYProperty = DependencyProperty.Register("CameraResolutionY", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double CameraResolutionX
        {
            get { return (double)GetValue(CameraResolutionXProperty); }
            set { SetValue(CameraResolutionXProperty, value); }
        }

        public static readonly DependencyProperty CameraResolutionXProperty = DependencyProperty.Register("CameraResolutionX", typeof(double), typeof(FhGravityAndAreaUserControl));


        //Rectangle 1
        public SolidColorBrush Rect1Color
        {
            get { return (SolidColorBrush)GetValue(Rect1ColorProperty); }
            set { SetValue(Rect1ColorProperty, value); }
        }

        public static readonly DependencyProperty Rect1ColorProperty = DependencyProperty.Register("Rect1Color", typeof(SolidColorBrush), typeof(FhGravityAndAreaUserControl));

        public double Rect1RegionHeight
        {
            get { return (double)GetValue(Rect1RegionHeightProperty); }
            set { SetValue(Rect1RegionHeightProperty, value); }
        }

        public static readonly DependencyProperty Rect1RegionHeightProperty = DependencyProperty.Register("Rect1RegionHeight", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double Rect1RegionWidth
        {
            get { return (double)GetValue(Rect1RegionWidthProperty); }
            set { SetValue(Rect1RegionWidthProperty, value); }
        }

        public static readonly DependencyProperty Rect1RegionWidthProperty = DependencyProperty.Register("Rect1RegionWidth", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double Rect1RegionUpperLeftX
        {
            get { return (double)GetValue(Rect1RegionUpperLeftXProperty); }
            set { SetValue(Rect1RegionUpperLeftXProperty, value); }
        }

        public static readonly DependencyProperty Rect1RegionUpperLeftXProperty = DependencyProperty.Register("Rect1RegionUpperLeftX", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double Rect1RegionUpperLeftY
        {
            get { return (double)GetValue(Rect1RegionUpperLeftYProperty); }
            set { SetValue(Rect1RegionUpperLeftYProperty, value); }
        }

        public static readonly DependencyProperty Rect1RegionUpperLeftYProperty = DependencyProperty.Register("Rect1RegionUpperLeftY", typeof(double), typeof(FhGravityAndAreaUserControl));


        //Rectangle 2
        public SolidColorBrush Rect2Color
        {
            get { return (SolidColorBrush)GetValue(Rect2ColorProperty); }
            set { SetValue(Rect2ColorProperty, value); }
        }

        public static readonly DependencyProperty Rect2ColorProperty = DependencyProperty.Register("Rect2Color", typeof(SolidColorBrush), typeof(FhGravityAndAreaUserControl));

        public double Rect2RegionHeight
        {
            get { return (double)GetValue(Rect2RegionHeightProperty); }
            set { SetValue(Rect2RegionHeightProperty, value); }
        }

        public static readonly DependencyProperty Rect2RegionHeightProperty = DependencyProperty.Register("Rect2RegionHeight", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double Rect2RegionWidth
        {
            get { return (double)GetValue(Rect2RegionWidthProperty); }
            set { SetValue(Rect2RegionWidthProperty, value); }
        }

        public static readonly DependencyProperty Rect2RegionWidthProperty = DependencyProperty.Register("Rect2RegionWidth", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double Rect2RegionUpperLeftX
        {
            get { return (double)GetValue(Rect2RegionUpperLeftXProperty); }
            set { SetValue(Rect2RegionUpperLeftXProperty, value); }
        }

        public static readonly DependencyProperty Rect2RegionUpperLeftXProperty = DependencyProperty.Register("Rect2RegionUpperLeftX", typeof(double), typeof(FhGravityAndAreaUserControl));

        public double Rect2RegionUpperLeftY
        {
            get { return (double)GetValue(Rect2RegionUpperLeftYProperty); }
            set { SetValue(Rect2RegionUpperLeftYProperty, value); }
        }

        public static readonly DependencyProperty Rect2RegionUpperLeftYProperty = DependencyProperty.Register("Rect2RegionUpperLeftY", typeof(double), typeof(FhGravityAndAreaUserControl));

        #endregion


    }
}
