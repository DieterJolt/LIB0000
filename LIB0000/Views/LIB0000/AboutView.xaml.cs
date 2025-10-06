
using System.Diagnostics;
using System.IO;
using Wpf.Ui.Controls;


namespace LIB0000
{
    public partial class AboutView : INavigableView<AboutViewModel>
    {


        #region Commands
        #endregion

        #region Constructor
        public AboutView(AboutViewModel viewModel, GlobalService globalService)
        {
            ViewModel = viewModel;
            GlobalService = globalService;
            DataContext = this;

            InitializeComponent();

            //var stlReader = new StLReader();
            //Model3DGroup modelGroup = stlReader.Read("C:\\FH\\MOR0001.stl");

            //modelVisual3D.Content = modelGroup;

        }

        #endregion

        #region Events

        private void WebsiteEvent(object sender, RoutedEventArgs e)
        {
            string joltWebsite = "https://www.jolt.be";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = joltWebsite,
                    UseShellExecute = true // Nodig voor .NET Core om de standaard browser te gebruiken
                });
            }
            catch (Exception ex)
            {
            }
        }

        private void OperatorManualEvent(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\FH\LIB1000MANUAL.pdf";

            // Als het bestand bestaat verwijder het
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string pdfUri = "pack://application:,,,/Assets/Manuals/LIB1000MANUAL.pdf";
            string tempFilePath = @"C:\FH\LIB1000MANUAL.pdf";// dubbele underscore

            // Laad de PDF als resource
            Uri uri = new Uri(pdfUri);
            var resourceStream = Application.GetResourceStream(uri);

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                resourceStream.Stream.CopyTo(fileStream); // Kopieer de inhoud naar een tijdelijk bestand
            }

            // Open het PDF-bestand met de standaard viewer
            Process.Start(new ProcessStartInfo
            {
                FileName = tempFilePath,
                UseShellExecute = true // Zorg ervoor dat het bestand met de standaard applicatie wordt geopend
            });
        }

        private void ElektrischPlanEvent(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\FH\LIB1000EL.pdf";

            // Als het bestand bestaat verwijder het
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string pdfUri = "pack://application:,,,/Assets/Manuals/LIB1000EL.pdf";
            string tempFilePath = @"C:\FH\LIB1000EL.pdf";


            // Laad de PDF als resource
            Uri uri = new Uri(pdfUri);
            var resourceStream = Application.GetResourceStream(uri);

            using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
            {
                resourceStream.Stream.CopyTo(fileStream); // Kopieer de inhoud naar een tijdelijk bestand
            }

            // Open het PDF-bestand met de standaard viewer
            Process.Start(new ProcessStartInfo
            {
                FileName = tempFilePath,
                UseShellExecute = true // Zorg ervoor dat het bestand met de standaard applicatie wordt geopend
            });

        }

        #endregion

        #region Fields
        public AboutViewModel ViewModel { get; }
        public GlobalService GlobalService { get; set; }

        #endregion

        #region Methods
        private void OpenAppFolder()
        {
            try
            {
                // Krijg het pad naar de map waar de applicatie is geïnstalleerd
                string appFolderPath = AppDomain.CurrentDomain.BaseDirectory + "HardwareBackups";

                // Open de map in de standaardbestandsverkenner
                Process.Start("explorer.exe", appFolderPath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }














        #endregion

        #region Properties
        #endregion


    }
}
