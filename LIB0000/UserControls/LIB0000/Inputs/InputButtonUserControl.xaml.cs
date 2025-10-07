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

    public partial class InputButtonUserControl : UserControl
    {
        public InputButtonUserControl()
        {
            InitializeComponent();
        }       

        public string TextRight
        {
            get { return (string)GetValue(TextRightProperty); }
            set { SetValue(TextRightProperty, value); }
        }

        public static readonly DependencyProperty TextRightProperty = DependencyProperty.Register("TextRight", typeof(string), typeof(InputButtonUserControl));

        public RelayCommand CommandRight
        {
            get { return (RelayCommand)GetValue(CommandRightProperty); }
            set { SetValue(CommandRightProperty, value); }
        }

        public static readonly DependencyProperty CommandRightProperty = DependencyProperty.Register("CommandRight", typeof(RelayCommand), typeof(InputButtonUserControl));

        public bool VisibilityRight
        {
            get { return (bool)GetValue(VisibilityRightProperty); }
            set { SetValue(VisibilityRightProperty, value); }
        }

        public static readonly DependencyProperty VisibilityRightProperty = DependencyProperty.Register("VisibilityRight", typeof(bool), typeof(InputButtonUserControl)
        );

        //Button 1 image
        public ImageSource ImageRight
        {
            get { return (ImageSource)GetValue(ImageRightProperty); }
            set { SetValue(ImageRightProperty, value); }
        }

        public static readonly DependencyProperty ImageRightProperty = DependencyProperty.Register("ImageRight", typeof(ImageSource),
            typeof(InputButtonUserControl)
        );


        public string Input
        {
            get { return (string)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(string), typeof(InputButtonUserControl));
    }
}
