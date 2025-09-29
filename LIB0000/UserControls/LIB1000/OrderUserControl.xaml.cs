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
    /// <summary>
    /// Interaction logic for OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl
    {
        public OrderUserControl()
        {
            InitializeComponent();
        }

        //Label Name
        public OrderHistoryModel OrderHistory
        {
            get
            {
                if (OrderHistory?.OrderHistoryType is string)
                {

                }

                return (OrderHistoryModel)GetValue(OrderHistoryProperty);
            }
            set
            {
                if (OrderHistory?.OrderHistoryType is string)
                {

                }

                SetValue(OrderHistoryProperty, value);
            }
        }

        public static readonly DependencyProperty OrderHistoryProperty = DependencyProperty.Register("OrderHistory", typeof(OrderHistoryModel), typeof(OrderUserControl));

    }

}
