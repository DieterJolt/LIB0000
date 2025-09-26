using System.Security.Cryptography;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LIB0000
{
    public partial class SliderbarUserControl : UserControl
    {

        #region Commands
        #endregion

        #region Constructor

        public SliderbarUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //SliderValue = e.NewValue;
            MySlider.Minimum = MinValue;
            RedrawTrack();
        }

        private void Slider_Loaded(object sender, RoutedEventArgs e)
        {
            RedrawTrack();
            MyTextBlock.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("SliderValue")
            {
                Source = this,
                StringFormat = "F" + DigitsAfterComma
            });
        }

        #endregion

        #region Fields
        #endregion

        #region Methods


        private void RedrawTrack()
        {
            if (LowerLimitArea <= MinValue) { return; }
            if (UpperLimitArea <= LowerLimitArea) { return; }
            if (MaxValue <= UpperLimitArea) { return; }

            SliderTicks = new DoubleCollection() { MinValue, LowerLimitArea, UpperLimitArea, MaxValue };
            TrackCanvas.Children.Clear();

            double totalWidth = TrackCanvas.ActualWidth;
            double height = TrackCanvas.ActualHeight;

            double beginValue = MinValue;
            double maxValue = MaxValue;
            double lowerLimitArea = LowerLimitArea;
            double upperLimitArea = UpperLimitArea;

            double range = maxValue - beginValue;

            double x1 = totalWidth * ((lowerLimitArea - beginValue) / range);
            double x2 = totalWidth * ((upperLimitArea - beginValue) / range);

            // Linker rode zone
            TrackCanvas.Children.Add(new Rectangle
            {
                Fill = ColorOutOfRange,
                Width = x1,
                Height = height
            });

            // Label links (begin)
            var leftLabel = new TextBlock
            {
                Text = beginValue.ToString(),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };
            TrackCanvas.Children.Add(leftLabel);
            Canvas.SetLeft(leftLabel, beginValue);
            Canvas.SetTop(leftLabel, height + 10);

            // Groene zone
            TrackCanvas.Children.Add(new Rectangle
            {
                Fill = ColorInRange,
                Width = x2 - x1,
                Height = height,
                Margin = new Thickness(x1, 0, 0, 0)
            });

            // Label lowerLimitAreaGreen
            var lowerLabel = new TextBlock
            {
                Text = lowerLimitArea.ToString(),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };
            TrackCanvas.Children.Add(lowerLabel);
            Canvas.SetLeft(lowerLabel, x1);
            Canvas.SetTop(lowerLabel, height + 10);

            // Rechter rode zone
            TrackCanvas.Children.Add(new Rectangle
            {
                Fill = ColorOutOfRange,
                Width = totalWidth - x2,
                Height = height,
                Margin = new Thickness(x2, 0, 0, 0)
            });

            // Label upperLimitAreaGreen
            var upperLabel = new TextBlock
            {
                Text = upperLimitArea.ToString(),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };
            TrackCanvas.Children.Add(upperLabel);
            Canvas.SetLeft(upperLabel, x2);
            Canvas.SetTop(upperLabel, height + 10);

            // Label rechts (einde)
            var rightLabel = new TextBlock
            {
                Text = maxValue.ToString(),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };
            TrackCanvas.Children.Add(rightLabel);
            Canvas.SetLeft(rightLabel, totalWidth);
            Canvas.SetTop(rightLabel, height + 10);
        }

        #endregion

        #region Properties

        public string TitleName
        {
            get { return (string)GetValue(TitleNameProperty); }
            set { SetValue(TitleNameProperty, value); }
        }

        public static readonly DependencyProperty TitleNameProperty = DependencyProperty.Register("TitleName", typeof(string), typeof(SliderbarUserControl));

        public string DigitsAfterComma
        {
            get { return (string)GetValue(DigitsAfterCommaProperty); }
            set { SetValue(DigitsAfterCommaProperty, value); }
        }

        public static readonly DependencyProperty DigitsAfterCommaProperty = DependencyProperty.Register("DigitsAfterComma", typeof(string), typeof(SliderbarUserControl));

        public SolidColorBrush ColorBackground
        {
            get { return (SolidColorBrush)GetValue(ColorBackgroundProperty); }
            set { SetValue(ColorBackgroundProperty, value); }
        }

        public static readonly DependencyProperty ColorBackgroundProperty = DependencyProperty.Register("ColorBackground", typeof(SolidColorBrush), typeof(SliderbarUserControl));

        public SolidColorBrush ColorBorder
        {
            get { return (SolidColorBrush)GetValue(ColorBorderProperty); }
            set { SetValue(ColorBorderProperty, value); }
        }

        public static readonly DependencyProperty ColorBorderProperty = DependencyProperty.Register("ColorBorder", typeof(SolidColorBrush), typeof(SliderbarUserControl));

        public SolidColorBrush ColorInRange
        {
            get { return (SolidColorBrush)GetValue(ColorInRangeProperty); }
            set { SetValue(ColorInRangeProperty, value); }
        }

        public static readonly DependencyProperty ColorInRangeProperty = DependencyProperty.Register("ColorInRange", typeof(SolidColorBrush), typeof(SliderbarUserControl));

        public SolidColorBrush ColorOutOfRange
        {
            get { return (SolidColorBrush)GetValue(ColorOutOfRangeProperty); }
            set { SetValue(ColorOutOfRangeProperty, value); }
        }

        public static readonly DependencyProperty ColorOutOfRangeProperty = DependencyProperty.Register("ColorOutOfRange", typeof(SolidColorBrush), typeof(SliderbarUserControl));

        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(SliderbarUserControl));

        public double LowerLimitArea
        {
            get { return (double)GetValue(LowerLimitAreaProperty); }
            set { SetValue(LowerLimitAreaProperty, value); }
        }

        public static readonly DependencyProperty LowerLimitAreaProperty = DependencyProperty.Register("LowerLimitArea", typeof(double), typeof(SliderbarUserControl));

        public double UpperLimitArea
        {
            get { return (double)GetValue(UpperLimitAreaProperty); }
            set { SetValue(UpperLimitAreaProperty, value); }
        }

        public static readonly DependencyProperty UpperLimitAreaProperty = DependencyProperty.Register("UpperLimitArea", typeof(double), typeof(SliderbarUserControl));

        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(SliderbarUserControl));

        public double SliderValue
        {
            get { return (double)GetValue(SliderValueProperty); }
            set { SetValue(SliderValueProperty, value); }
        }

        public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register("SliderValue", typeof(double), typeof(SliderbarUserControl));

        //Test Louis
        //    public static readonly DependencyProperty SliderValueProperty =
        //DependencyProperty.Register(
        //    nameof(SliderValue),            
        //    typeof(double),
        //    typeof(SliderbarUserControl),    
        //    new PropertyMetadata(0.0, OnSliderValueChanged));

        //Test Louis
        //   public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register(
        //"SliderValue",
        //typeof(double),
        //typeof(SliderbarUserControl),
        //new FrameworkPropertyMetadata(
        //    0.0,
        //    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        //    OnSliderValueChanged));
        //
        //public double SliderValue
        //{
        //    get { return (double)GetValue(SliderValueProperty); }
        //    set { SetValue(SliderValueProperty, value); }
        //}


        public DoubleCollection SliderTicks
        {
            get { return (DoubleCollection)GetValue(SliderTicksProperty); }
            set { SetValue(SliderTicksProperty, value); }
        }

        public static readonly DependencyProperty SliderTicksProperty = DependencyProperty.Register("SliderTicks", typeof(DoubleCollection), typeof(SliderbarUserControl),
        new PropertyMetadata(new DoubleCollection() { 0, 0, 0, 0 }));



        #endregion


    }
}
