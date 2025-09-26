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
    public partial class TextCheckboxUserControl : UserControl
    {
        public TextCheckboxUserControl()
        {
            InitializeComponent();
        }

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public static readonly DependencyProperty CheckedProperty = DependencyProperty.Register("Checked", typeof(bool), typeof(TextCheckboxUserControl));

        public string TextRight
        {
            get { return (string)GetValue(TextRightProperty); }
            set { SetValue(TextRightProperty, value); }
        }

        public static readonly DependencyProperty TextRightProperty = DependencyProperty.Register("TextRight", typeof(string), typeof(TextCheckboxUserControl));

        public string TextLeft
        {
            get { return (string)GetValue(TextLeftProperty); }
            set { SetValue(TextLeftProperty, value); }
        }
        public static readonly DependencyProperty TextLeftProperty = DependencyProperty.Register("TextLeft", typeof(string), typeof(TextCheckboxUserControl));
    }
}
