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
    public partial class LayoutEdit03SnapshotUserControl : UserControl
    {
        #region Commands
        #endregion

        #region Constructor

        public LayoutEdit03SnapshotUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Change_Image(object sender, MouseButtonEventArgs e)
        {
            byte[] newImage = ConvertImageFileLocationToByteArray();

            if ((sender is FrameworkElement element) && (newImage != null))
            {
                string elementName = element.Name;
                switch (elementName)
                {
                    case "Image1":
                        InstructionModel.Image1 = newImage;
                        break;
                    case "Image2":
                        InstructionModel.Image2 = newImage;
                        break;
                }
            }
        }

        public byte[] ConvertImageFileLocationToByteArray()
        {
            byte[] result = null;

            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Title = "Kies bestand";
                openFileDialog.Filter = "Afbeeldingen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Alle bestanden (*.*)|*.*";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string selectedFileName = openFileDialog.FileName;
                    result = File.ReadAllBytes(selectedFileName);

                }
            }
            return result;
        }

        #endregion

        #region Fields
        #endregion

        #region Methods
        #endregion

        #region Properties

        public InstructionModel InstructionModel
        {
            get { return (InstructionModel)GetValue(InstructionModelProperty); }
            set { SetValue(InstructionModelProperty, value); }
        }

        public static readonly DependencyProperty InstructionModelProperty = DependencyProperty.Register("InstructionModel", typeof(InstructionModel), typeof(LayoutEdit03SnapshotUserControl));

        public ICommand cmdInstructionOk
        {
            get => (ICommand)GetValue(cmdInstructionOkProperty);
            set => SetValue(cmdInstructionOkProperty, value);
        }

        public static readonly DependencyProperty cmdInstructionOkProperty = DependencyProperty.Register(nameof(cmdInstructionOk), typeof(ICommand), typeof(LayoutEdit03SnapshotUserControl), new PropertyMetadata(null));

        public Visibility EditVisibilityMode
        {
            get { return (Visibility)GetValue(EditVisibilityModeProperty); }
            set { SetValue(EditVisibilityModeProperty, value); }
        }

        public static readonly DependencyProperty EditVisibilityModeProperty = DependencyProperty.Register("EditVisibilityMode", typeof(Visibility), typeof(LayoutEdit03SnapshotUserControl));

        public Visibility NextButtonVisibilityMode
        {
            get { return (Visibility)GetValue(NextButtonVisibilityModeProperty); }
            set { SetValue(NextButtonVisibilityModeProperty, value); }
        }

        public static readonly DependencyProperty NextButtonVisibilityModeProperty = DependencyProperty.Register("NextButtonVisibilityMode", typeof(Visibility), typeof(LayoutEdit03SnapshotUserControl));

        public bool EditEnabledMode
        {
            get { return (bool)GetValue(EditEnabledModeProperty); }
            set { SetValue(EditEnabledModeProperty, value); }
        }

        public static readonly DependencyProperty EditEnabledModeProperty = DependencyProperty.Register("EditEnabledMode", typeof(bool), typeof(LayoutEdit03SnapshotUserControl));

        #endregion
    }
}
