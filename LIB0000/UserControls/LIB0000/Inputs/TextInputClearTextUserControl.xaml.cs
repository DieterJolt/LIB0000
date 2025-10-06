using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;


namespace LIB0000
{
    public partial class TextInputClearTextUserControl : UserControl
    {
        public TextInputClearTextUserControl()
        {
            InitializeComponent();
        }

        public string TextLeft
        {
            get { return (string)GetValue(TextLeftProperty); }
            set { SetValue(TextLeftProperty, value); }
        }

        public static readonly DependencyProperty TextLeftProperty = DependencyProperty.Register("TextLeft", typeof(string), typeof(TextInputClearTextUserControl));

        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(string), typeof(TextInputClearTextUserControl));

        public FontFamily FontFamilyInput
        {
            get { return (FontFamily)GetValue(FontFamilyInputProperty); }
            set { SetValue(FontFamilyInputProperty, value); }
        }

        public static readonly DependencyProperty FontFamilyInputProperty =
            DependencyProperty.Register(
                "FontFamilyInput",
                typeof(FontFamily),
                typeof(TextInputClearTextUserControl),
                new PropertyMetadata(new FontFamily("Segoe UI"))  // default waarde
            );

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Input = null;
        }
    }
}
