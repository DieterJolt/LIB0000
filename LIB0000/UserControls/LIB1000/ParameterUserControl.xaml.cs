using System.Windows.Controls;

namespace LIB0000
{
    public partial class ParameterUserControl : UserControl
    {
        public ParameterUserControl()
        {
            InitializeComponent();
        }

        //Label Name
        public string LabelName
        {
            get { return (string)GetValue(LabelNameProperty); }
            set { SetValue(LabelNameProperty, value); }
        }

        public static readonly DependencyProperty LabelNameProperty = DependencyProperty.Register("LabelName", typeof(string), typeof(ParameterUserControl));

        //Label Text
        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(ParameterUserControl));

        //Label Text
        public string LabelUnitText
        {
            get { return (string)GetValue(LabelUnitTextProperty); }
            set { SetValue(LabelUnitTextProperty, value); }
        }

        public static readonly DependencyProperty LabelUnitTextProperty = DependencyProperty.Register("LabelUnitText", typeof(string), typeof(ParameterUserControl));

        //LabelPlusCommand
        public RelayCommand LabelPlusCommand
        {
            get { return (RelayCommand)GetValue(LabelPlusCommandProperty); }
            set { SetValue(LabelPlusCommandProperty, value); }
        }

        public static readonly DependencyProperty LabelPlusCommandProperty = DependencyProperty.Register("LabelPlusCommand", typeof(RelayCommand), typeof(ParameterUserControl));

        //LabelMinusCommand
        public RelayCommand LabelMinusCommand
        {
            get { return (RelayCommand)GetValue(LabelMinusCommandProperty); }
            set { SetValue(LabelMinusCommandProperty, value); }
        }

        public static readonly DependencyProperty LabelMinusCommandProperty = DependencyProperty.Register("LabelMinusCommand", typeof(RelayCommand), typeof(ParameterUserControl));

        //Button Minvalue
        public int MinValue
        {
            get { return (int)GetValue(ButtonMinValueProperty); }
            set { SetValue(ButtonMinValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonMinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(ParameterUserControl)
        );

        //Button Maxvalue
        public int MaxValue
        {
            get { return (int)GetValue(ButtonMaxValueProperty); }
            set { SetValue(ButtonMaxValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonMaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(ParameterUserControl)
        );

        //Button Setvalue
        public int Value
        {
            get { return (int)GetValue(ButtonSetValueProperty); }
            set { SetValue(ButtonSetValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonSetValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(ParameterUserControl)
        );

        //Button StepBigvalue
        public int StepBigValue
        {
            get { return (int)GetValue(ButtonStepBigValueProperty); }
            set { SetValue(ButtonStepBigValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonStepBigValueProperty = DependencyProperty.Register("StepBigValue", typeof(int), typeof(ParameterUserControl)
        );

        //Button StepBigTime
        public int StepBigTime
        {
            get { return (int)GetValue(ButtonStepBigTimeValueProperty); }
            set { SetValue(ButtonStepBigTimeValueProperty, value); }
        }

        public static readonly DependencyProperty ButtonStepBigTimeValueProperty = DependencyProperty.Register("StepBigTime", typeof(int), typeof(ParameterUserControl)
        );

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (int.TryParse(txt.Text, out int value))
            {
                if (value < MinValue)
                {
                    txt.Text = MinValue.ToString();
                }
                else if (value > MaxValue)
                {
                    txt.Text = MaxValue.ToString();
                }
                else
                {
                    Value = value;
                }
            }
            else
            {
                txt.Text = Value.ToString();
            }
        }
    }
}
