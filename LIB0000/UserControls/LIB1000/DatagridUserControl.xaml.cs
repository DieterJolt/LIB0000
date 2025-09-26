using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace LIB0000
{
    /// <summary>
    /// Interaction logic for DatagridUserControl.xaml
    /// </summary>
    public partial class DatagridUserControl : UserControl, INotifyPropertyChanged
    {
        public DatagridUserControl()
        {
            DataContext = this;
            InitializeComponent();


        }

        public object ItemsSource01
        {
            get => GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                OnPropertyChanged(nameof(ItemsSource01));
            }
        }

        public object SelectedItem01
        {
            get => GetValue(SelectedItemProperty);
            set
            {
                SetValue(SelectedItemProperty, value);
                OnPropertyChanged(nameof(SelectedItem01));
            }
        }

        public int TestString
        {
            get => (int)GetValue(TestStringProperty);
            set
            {
                SetValue(TestStringProperty, value);
                OnPropertyChanged(nameof(TestString));
            }
        }



        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource01", typeof(object), typeof(DatagridUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem01", typeof(object), typeof(DatagridUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TestStringProperty =
    DependencyProperty.Register("TestString", typeof(int), typeof(DatagridUserControl));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
