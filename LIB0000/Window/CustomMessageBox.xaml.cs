using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LIB0000
{
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string message, string title = "Melding", MessageBoxImage icon = MessageBoxImage.None)
        {
            InitializeComponent();
            this.Title = title;
            MessageText.Text = message;

            // Icoon instellen
            MessageIcon.Source = GetSystemIcon(icon);

            //Window in het midden van het scherm centreren
            var parentWindow = System.Windows.Application.Current.MainWindow;
            this.Left = parentWindow.Left + (parentWindow.Width - this.ActualWidth) / 2;
            this.Top = parentWindow.Top + (parentWindow.Height - this.ActualHeight) / 2;
        }

        private ImageSource GetSystemIcon(MessageBoxImage icon)
        {
            Icon sysIcon = icon switch
            {
                MessageBoxImage.Error => SystemIcons.Error,
                MessageBoxImage.Warning => SystemIcons.Warning,
                MessageBoxImage.Information => SystemIcons.Information,
                MessageBoxImage.Question => SystemIcons.Question,
                _ => null
            };

            if (sysIcon != null)
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    sysIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }

            return null; // Geen icoon tonen als er geen geldig type is
        }

        // OK knop sluiten
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        // Sluitknop (X)
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        // Standaard Show methode
        public static bool Show(string message, string title = "Melding", MessageBoxImage icon = MessageBoxImage.None)
        {
            CustomMessageBox msgBox = new CustomMessageBox(message, title, icon);
            return msgBox.ShowDialog() == true;
        }
    }
}
