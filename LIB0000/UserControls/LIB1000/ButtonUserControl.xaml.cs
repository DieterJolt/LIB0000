using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace LIB0000
{
    public partial class ButtonUserControl : UserControl
    {

        #region Commands
        #endregion

        #region Constructor

        public ButtonUserControl()
        {
            InitializeComponent();
            timer = new DispatcherTimer();

            timer.Tick += Timer_Tick;
        }
        //StepBigTime

        #endregion

        #region Events

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(StepBigTime);
            timer.Start();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (Substract)
            {
                Value -= StepValue;
            }
            else
            {
                Value += StepValue;
            }

            if (Value < MinValue)
            {
                Value = MinValue;
            }

            if (Value > MaxValue)
            {
                Value = MaxValue;
            }
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            timer.Stop();
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer.Stop();
        }

        #endregion

        #region Fields
        #endregion

        #region Methods

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Substract)
            {
                Value -= StepBigValue;
            }
            else
            {
                Value += StepBigValue;
            }

            if (Value < MinValue)
            {
                Value = MinValue;
            }

            if (Value > MaxValue)
            {
                Value = MaxValue;
            }
        }

        #endregion

        #region Properties

        //Button icon
        public FontAwesome.Sharp.IconChar Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(ButtonIconProperty); }
            set { SetValue(ButtonIconProperty, value); }
        }

        public static readonly DependencyProperty ButtonIconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.Sharp.IconChar), typeof(ButtonUserControl)
        );

        //Button Iconcollor
        public SolidColorBrush IconColor
        {
            get { return (System.Windows.Media.SolidColorBrush)GetValue(ButtonIconColorProperty); }
            set { SetValue(ButtonIconColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonIconColorProperty = DependencyProperty.Register("IconColor", typeof(System.Windows.Media.SolidColorBrush), typeof(ButtonUserControl)
        );

        //Button Setvalue
        public int Value
        {
            get { return (int)GetValue(ButtonSetValueProperty); }
            set { SetValue(ButtonSetValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonSetValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(ButtonUserControl)
        );

        //Button Minvalue
        public int MinValue
        {
            get { return (int)GetValue(ButtonMinValueProperty); }
            set { SetValue(ButtonMinValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonMinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(ButtonUserControl)
        );

        //Button Maxvalue
        public int MaxValue
        {
            get { return (int)GetValue(ButtonMaxValueProperty); }
            set { SetValue(ButtonMaxValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonMaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(ButtonUserControl)
        );

        //Button Stepvalue
        public int StepValue
        {
            get { return (int)GetValue(ButtonStepValueProperty); }
            set { SetValue(ButtonStepValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonStepValueProperty = DependencyProperty.Register("StepValue", typeof(int), typeof(ButtonUserControl)
        );

        //Button StepBigvalue
        public int StepBigValue
        {
            get { return (int)GetValue(ButtonStepBigValueProperty); }
            set { SetValue(ButtonStepBigValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonStepBigValueProperty = DependencyProperty.Register("StepBigValue", typeof(int), typeof(ButtonUserControl)
        );

        //Button StepBigTime
        public int StepBigTime
        {
            get { return (int)GetValue(ButtonStepBigTimeValueProperty); }
            set { SetValue(ButtonStepBigTimeValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonStepBigTimeValueProperty = DependencyProperty.Register("StepBigTime", typeof(int), typeof(ButtonUserControl)
        );

        //Button Subtract
        public bool Substract
        {
            get { return (bool)GetValue(SubstractProperty); }
            set { SetValue(SubstractProperty, value); }
        }

        public static readonly DependencyProperty SubstractProperty = DependencyProperty.Register("Substract", typeof(bool), typeof(ButtonUserControl)
        );

        private DispatcherTimer timer;

        public SolidColorBrush ButtonBackgroundColor
        {
            get { return (SolidColorBrush)GetValue(ButtonBackgroundColorProperty); }
            set { SetValue(ButtonBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonBackgroundColorProperty = DependencyProperty.Register("ButtonBackgroundColor", typeof(SolidColorBrush), typeof(Footer10ButtonsUserControl), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF343434"))));

        #endregion



    }
}
