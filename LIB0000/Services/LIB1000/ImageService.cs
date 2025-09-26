using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace LIB0000
{
    public partial class ImageService : ObservableObject
    {


        #region Commands
        #endregion
        #region Constructor
        public ImageService()
        {
        }
        #endregion
        #region Fields

        DateTime dtLastImage;

        #endregion
        #region Methods



        /// <summary>
        /// Converts an image file at the given location to a resized byte array.
        /// Only resizes the image if it exceeds the specified maximum dimensions while maintaining aspect ratio.
        /// </summary>
        /// <param name="filePath">The location of the image file.</param>
        /// <param name="maxWidth">The maximum width of the resized image.</param>
        /// <param name="maxHeight">The maximum height of the resized image.</param>
        /// <returns>A byte array representing the resized image, or the original image if no resizing is needed.</returns>
        public byte[] ConvertFileLocationImageToByteArray(string filePath, int maxWidth, int maxHeight)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("The file path is invalid or the file does not exist.");
            }

            using (var originalImage = Image.FromFile(filePath))
            {
                // Check if resizing is necessary
                if (originalImage.Width <= maxWidth && originalImage.Height <= maxHeight)
                {
                    // No resizing needed, return the original image as a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        originalImage.Save(memoryStream, ImageFormat.Jpeg); // Save as JPEG
                        return memoryStream.ToArray();
                    }
                }

                // Calculate new dimensions while maintaining aspect ratio
                float aspectRatio = (float)originalImage.Width / originalImage.Height;
                int targetWidth, targetHeight;

                if (originalImage.Width > originalImage.Height)
                {
                    targetWidth = maxWidth;
                    targetHeight = (int)(maxWidth / aspectRatio);
                }
                else
                {
                    targetHeight = maxHeight;
                    targetWidth = (int)(maxHeight * aspectRatio);
                }

                // Resize the image
                using (var resizedImage = new Bitmap(targetWidth, targetHeight))
                {
                    using (var graphics = Graphics.FromImage(resizedImage))
                    {
                        // Set quality options for resizing
                        graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                        // Draw the original image onto the resized bitmap
                        graphics.DrawImage(originalImage, 0, 0, targetWidth, targetHeight);
                    }

                    // Convert the resized bitmap to a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        resizedImage.Save(memoryStream, ImageFormat.Jpeg); // Save as JPEG (can be changed)
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public byte[] ConvertImageToByteArray(string uri)
        {
            System.Uri imageUri = new System.Uri(uri, UriKind.Absolute);
            BitmapImage bitmap = new BitmapImage(imageUri);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(memoryStream);

                return memoryStream.ToArray();
            }
        }


        public void GetGridImages()
        {
            //ListImages leeg maken
            ListGridImages.Clear();


            string[] bestanden = Directory.GetFiles(FilesLocation)
                    .Where(file => file.EndsWith(Extention, StringComparison.OrdinalIgnoreCase))
                    .Select(Path.GetFileName).ToArray();


            foreach (string bestand in bestanden)
            {
                // //Afbeelding laden en toevoegen aan de lijst
                ImageModel afbeelding = new ImageModel();
                afbeelding.ImageDescription = bestand;

                // voorbeeld van een file : 01012021141509000_OK_098

                if (bestand.Length == 35 && bestand.Contains("#"))
                {
                    string[] info = bestand.Split('#');

                    // Ok or NG
                    afbeelding.Status = info[0];

                    // Percentage                            
                    afbeelding.Percentage = int.Parse(info[1]);

                    // datum            
                    string bestandTime = info[2];
                    bestandTime = Path.GetFileNameWithoutExtension(bestandTime);
                    string format = "yyyy-MM-dd_HH-mm-ss-ffff";
                    afbeelding.DateTimeCreated = DateTime.ParseExact(bestandTime, format, CultureInfo.InvariantCulture);

                    // Toevoegen aan lijst
                    ListGridImages.Add(afbeelding);
                }
            }
            ListGridImages = FilterImagesList(ListGridImages);
        }

        public void GetLastImages()
        {
            //ListImages leeg maken
            ListImages.Clear();

            if (Directory.Exists(FilesLocation))
            {
                string[] bestanden = Directory.GetFiles(FilesLocation)
                    .Where(file => file.EndsWith(Extention, StringComparison.OrdinalIgnoreCase))
                    .Select(Path.GetFileName).ToArray();

                foreach (string bestand in bestanden)
                {
                    // //Afbeelding laden en toevoegen aan de lijst
                    ImageModel afbeelding = new ImageModel();
                    afbeelding.ImageDescription = FilesLocation + bestand;

                    // voorbeeld van een file : OK#095#2024-04-12_17-14-29-7780

                    if (bestand.Length == 35 && bestand.Contains("#"))
                    {
                        string[] info = bestand.Split('#');

                        // Ok or NG
                        afbeelding.Status = info[0];

                        // Percentage                            
                        afbeelding.Percentage = int.Parse(info[1]);

                        // datum            
                        string bestandTime = info[2];
                        bestandTime = Path.GetFileNameWithoutExtension(bestandTime);
                        string format = "yyyy-MM-dd_HH-mm-ss-ffff";
                        afbeelding.DateTimeCreated = DateTime.ParseExact(bestandTime, format, CultureInfo.InvariantCulture);

                        // Toevoegen aan lijst
                        ListImages.Add(afbeelding);
                    }
                }
            }
            else
            {
                Console.WriteLine("De opgegeven map bestaat niet.");
            }


            ListImages = new ObservableCollection<ImageModel>(
            ListImages.OrderByDescending(image => image.DateTimeCreated));


            if (ListImages.Count > 0)
            {
                try
                {
                    using (FileStream fs = File.Open(ListImages[0].ImageDescription, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        LastImage.Percentage = ListImages[0].Percentage;
                        LastImage.DateTimeCreated = ListImages[0].DateTimeCreated;
                        LastImage.Status = ListImages[0].Status;
                        LastImage.ImageDescription = ListImages[0].ImageDescription;

                        if (dtLastImage != ListImages[0].DateTimeCreated)
                        {
                            TimeSpan ts = ListImages[0].DateTimeCreated - dtLastImage;
                            TimeBetweenImages = ts.Milliseconds.ToString();
                        }

                        dtLastImage = ListImages[0].DateTimeCreated;
                    }
                }
                catch
                {
                }
            }

            if (ListImages.Count > 10)
            {

                List<ImageModel> removeImages = ListImages.OrderBy(image => image.DateTimeCreated)
                                                           .Take(ListImages.Count - 10)
                                                           .ToList();

                foreach (var oldestImage in removeImages)
                {
                    try
                    {
                        File.Delete(Path.Combine(FilesLocation, oldestImage.ImageDescription));
                        ListImages.Remove(oldestImage);
                    }
                    catch { }
                }
            }
        }


        public void UpdateSelected()
        {
            if (Selected is ImageModel)
            {
                Edit.DateTimeCreated = Selected.DateTimeCreated;
                Edit.ImageDescription = Selected.ImageDescription;
            }
        }

        public ObservableCollection<ImageModel> FilterImagesList(ObservableCollection<ImageModel> list)
        {

            var result = new ObservableCollection<ImageModel>(list);

            if (FilteredPercentage >= 0)
            {
                result = new ObservableCollection<ImageModel>(result
                    .Where(e => e.Percentage <= FilteredPercentage));
            }

            if (!string.IsNullOrEmpty(FilterdSelectedItem) && (FilterdSelectedItem != "Alles"))
            {
                result = new ObservableCollection<ImageModel>(result
                    .Where(e => e.Status.Contains(FilterdSelectedItem, StringComparison.OrdinalIgnoreCase)));
            }

            if (FilterStartDate != null && FilterEndDate != null)
            {
                result = new ObservableCollection<ImageModel>(result
                    .Where(e => e.DateTimeCreated >= FilterStartDate && e.DateTimeCreated <= FilterEndDate));
            }

            return result;
        }


        #endregion

        #region Properties

        [ObservableProperty]
        private ObservableCollection<ImageModel> _listGridImages = new ObservableCollection<ImageModel>();

        private DateTime _datumFilter = DateTime.Now;

        public DateTime DatumFilter
        {
            get { return _datumFilter; }
            set
            {
                _datumFilter = value;

                if (_datumFilter != value)
                {
                    _datumFilter = value;
                    OnPropertyChanged(nameof(DatumFilter));
                }

            }
        }

        [ObservableProperty]
        private ImageModel _selected = new ImageModel();

        [ObservableProperty]
        private ImageModel _edit = new ImageModel();

        [ObservableProperty]
        private string _filesLocation = "C:/FH/";

        [ObservableProperty]
        private string _extention = ".bmp";

        [ObservableProperty]
        private List<string> _listStatus = new List<string>();

        [ObservableProperty]
        private string _timeBetweenImages;

        [ObservableProperty]
        private int _selectedIndexDatagrid;


        private string _filterdSelectedItem;

        public string FilterdSelectedItem
        {
            get { return _filterdSelectedItem; }
            set
            {
                _filterdSelectedItem = value;

                GetGridImages();
            }
        }

        private DateTime _filterEndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

        public DateTime FilterEndDate
        {
            get { return _filterEndDate; }
            set
            {
                if (_filterEndDate != value)
                {
                    if (_filterEndDate > FilterStartDate)
                    {
                        _filterEndDate = value;
                    }
                    else
                    {
                        _filterEndDate = FilterStartDate.AddDays(1);
                    }
                    _filterEndDate = new DateTime(_filterEndDate.Year, _filterEndDate.Month, _filterEndDate.Day, 23, 59, 59);
                    OnPropertyChanged(nameof(_filterEndDate));
                    GetGridImages();
                }
            }
        }


        private DateTime _filterStartDate = DateTime.Today.AddDays(-30);
        public DateTime FilterStartDate
        {
            get { return _filterStartDate; }
            set
            {

                if (_filterStartDate != value)
                {
                    _filterStartDate = value;
                    _filterStartDate = new DateTime(_filterStartDate.Year, _filterStartDate.Month, _filterStartDate.Day, 0, 0, 0);
                    OnPropertyChanged(nameof(_filterStartDate));
                    GetGridImages();
                }

            }
        }


        private int _filteredPercentage;

        public int FilteredPercentage
        {
            get { return _filteredPercentage; }
            set
            {
                _filteredPercentage = value;

                GetGridImages();
            }
        }

        //Last Image

        [ObservableProperty]
        private ObservableCollection<ImageModel> _listImages = new ObservableCollection<ImageModel>();

        [ObservableProperty]
        private ImageModel _lastImage = new ImageModel();


        #endregion
    }
}
