using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class LayoutEdit12PdfUserControl : UserControl
    {
        public LayoutEdit12PdfUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if ((InstructionModel != null) && (InstructionModel.Text1 != null) && (InstructionModel.Text1 != ""))
                {
                    pdfBrowser.Navigate(InstructionModel.Text1);
                    pdfBrowser.Visibility = Visibility.Visible;
                }
                else
                {
                    pdfBrowser.Visibility = Visibility.Hidden;
                }
            }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newPdf = FilePicker();

            if ((newPdf != null) && (newPdf != ""))
            {
                InstructionModel.Text1 = newPdf;
                pdfBrowser.Navigate(InstructionModel.Text1);
                pdfBrowser.Visibility = Visibility.Visible;
            }
        }


        public string FilePicker()
        {
            string selectedFileName = null;

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Title = "Kies bestand";
                openFileDialog.Filter = "PDF-bestanden|*.pdf";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    selectedFileName = openFileDialog.FileName;
                }
            }
            return selectedFileName;
        }

        public InstructionModel InstructionModel
        {
            get { return (InstructionModel)GetValue(InstructionModelProperty); }
            set { SetValue(InstructionModelProperty, value); }
        }

        public static readonly DependencyProperty InstructionModelProperty = DependencyProperty.Register("InstructionModel", typeof(InstructionModel), typeof(LayoutEdit12PdfUserControl));

        public ICommand cmdInstructionOk
        {
            get => (ICommand)GetValue(cmdInstructionOkProperty);
            set => SetValue(cmdInstructionOkProperty, value);
        }

        public static readonly DependencyProperty cmdInstructionOkProperty = DependencyProperty.Register(nameof(cmdInstructionOk), typeof(ICommand), typeof(LayoutEdit12PdfUserControl), new PropertyMetadata(null));


        public Visibility EditVisibilityMode
        {
            get { return (Visibility)GetValue(EditVisibilityModeProperty); }
            set { SetValue(EditVisibilityModeProperty, value); }
        }

        public static readonly DependencyProperty EditVisibilityModeProperty = DependencyProperty.Register("EditVisibility", typeof(Visibility), typeof(LayoutEdit12PdfUserControl));

        public Visibility NextButtonVisibilityMode
        {
            get { return (Visibility)GetValue(NextButtonVisibilityModeProperty); }
            set { SetValue(NextButtonVisibilityModeProperty, value); }
        }

        public static readonly DependencyProperty NextButtonVisibilityModeProperty = DependencyProperty.Register("NextButtonVisibilityMode", typeof(Visibility), typeof(LayoutEdit12PdfUserControl));

        public bool EditEnabledMode
        {
            get { return (bool)GetValue(EditEnabledModeProperty); }
            set { SetValue(EditEnabledModeProperty, value); }
        }

        public static readonly DependencyProperty EditEnabledModeProperty = DependencyProperty.Register("EditEnabledMode", typeof(bool), typeof(LayoutEdit12PdfUserControl));


    }
}
