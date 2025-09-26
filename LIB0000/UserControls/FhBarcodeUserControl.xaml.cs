using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace LIB0000
{
    public partial class FhBarcodeUserControl : UserControl
    {
        #region Commands

        #endregion

        #region Constructor
        public FhBarcodeUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void cnv_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            reDrawrectangles();
        }

        private void btnEditRegio_Click(object sender, RoutedEventArgs e)
        {
            clearRectangle("AreaRegio");

            editRegioBusy = true;
        }

        private void btnMeasure_Click(object sender, RoutedEventArgs e)
        {
            SelectedFhService.Measure();
        }

        private void cnvArea_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name = "";
            System.Windows.Media.Brush brush = System.Windows.Media.Brushes.Green;

            if (editRegioBusy)
            {
                name = "AreaRegio";
                brush = System.Windows.Media.Brushes.Orange;
            }

            if (name != "")
            {
                rectangleUpperLeftTemp = e.GetPosition(cnv);

                rectangleTemp = new System.Windows.Shapes.Rectangle
                {
                    Name = name,
                    Stroke = brush,
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    Width = 10,
                    Height = 10
                };
                Canvas.SetLeft(rectangleTemp, rectangleUpperLeftTemp.X);
                Canvas.SetTop(rectangleTemp, rectangleUpperLeftTemp.Y);

                cnv.Children.Add(rectangleTemp);
            }
        }

        private void cnv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            editRegioBusy = false;

            for (int i = cnv.Children.Count - 1; i >= 0; i--)
            {
                if (cnv.Children[i] is System.Windows.Shapes.Rectangle)
                {
                    System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)cnv.Children[i];
                    if (rectangle.Name == "AreaRegio")
                    {
                        RegionUpperLeft = new System.Windows.Point(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle));
                        RegionLowerRight = new System.Windows.Point(Canvas.GetLeft(rectangle) + rectangle.Width, Canvas.GetTop(rectangle) + rectangle.Height);
                    }
                }
            }

            for (int i = cnv.Children.Count - 1; i >= 0; i--)
            {
                if (cnv.Children[i] is System.Windows.Shapes.Rectangle)
                {
                    System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)cnv.Children[i];
                    if (rectangle.Name == "AreaRegio")
                    {
                        if (!(rectangle.Width < Convert.ToDouble(AreaMinWidth) || rectangle.Height < Convert.ToDouble(AreaMinHeight)))
                        {
                            // Rechthoek groot genoeg
                            if (rectangle.Name == "AreaRegio")
                            {
                                ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "002").FirstOrDefault().ScreenValue = RegionUpperLeft.X.ToString();
                                ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "003").FirstOrDefault().ScreenValue = RegionUpperLeft.Y.ToString();
                                ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "004").FirstOrDefault().ScreenValue = RegionLowerRight.X.ToString();
                                ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "005").FirstOrDefault().ScreenValue = RegionLowerRight.Y.ToString();
                            }
                        }
                    }
                }
            }
            if (UpdateUICommand != null)
            {
                if (UpdateUICommand.CanExecute(null))
                {
                    // Controle op gewijzigde settings -> zorgt ervoor dat save-knop tevoorschijn komt
                    UpdateUICommand.Execute(null);
                }
            }
        }
        private void cnvArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }

            if (editRegioBusy)
            {
                ActualImageHeight = cnv.ActualHeight;
                ActualImageWidth = cnv.ActualWidth;

                var productDetail = ProductDetailList.Where(p => p.HardwareId == SelectedHardware.Id && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "006").FirstOrDefault();

                if (productDetail != null)
                {
                    productDetail.ScreenValue = ActualImageWidth.ToString();
                }

                productDetail = ProductDetailList.Where(p => p.HardwareId == SelectedHardware.Id && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "007").FirstOrDefault();

                if (productDetail != null)
                {
                    productDetail.ScreenValue = ActualImageHeight.ToString();
                }

                if ((rectangleTemp != null))
                {
                    rectangleLowerRightTemp = e.GetPosition(cnv);
                    double left = rectangleUpperLeftTemp.X < rectangleLowerRightTemp.X ? rectangleUpperLeftTemp.X : rectangleLowerRightTemp.X;
                    double top = rectangleUpperLeftTemp.Y < rectangleLowerRightTemp.Y ? rectangleUpperLeftTemp.Y : rectangleLowerRightTemp.Y;

                    rectangleTemp.Width = Math.Abs(rectangleLowerRightTemp.X - rectangleUpperLeftTemp.X);
                    rectangleTemp.Height = Math.Abs(rectangleLowerRightTemp.Y - rectangleUpperLeftTemp.Y);

                    Canvas.SetLeft(rectangleTemp, left);
                    Canvas.SetTop(rectangleTemp, top);
                }
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SelectedHardwareChanged();
        }
        #endregion

        #region Fields        

        private bool editRegioBusy;

        private System.Windows.Point rectangleUpperLeftTemp;
        private System.Windows.Point rectangleLowerRightTemp;
        private System.Windows.Shapes.Rectangle rectangleTemp;

        #endregion

        #region Methods

        private void clearRectangle(string nameOfRectangle)
        {
            for (int i = cnv.Children.Count - 1; i >= 0; i--)
            {
                if (cnv.Children[i] is System.Windows.Shapes.Rectangle)
                {
                    System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)cnv.Children[i];
                    if (rectangle.Name == nameOfRectangle)
                    {
                        cnv.Children.RemoveAt(i);
                    }
                }
            }
        }

        private void reDrawrectangles()
        {
            if (ProductDetailList != null && SelectedFhService.HardwareId != null)
            {
                double x, y = 0;
                x = Convert.ToDouble(ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "002").FirstOrDefault().Value);
                y = Convert.ToDouble(ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "003").FirstOrDefault().Value);
                RegionUpperLeft = new System.Windows.Point(x, y);
                x = Convert.ToDouble(ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "004").FirstOrDefault().Value);
                y = Convert.ToDouble(ProductDetailList.Where(p => p.HardwareId == SelectedFhService.HardwareId && p.HardwareFunction == HardwareFunction.Fhv7Barcode && p.Nr == "005").FirstOrDefault().Value);
                RegionLowerRight = new System.Windows.Point(x, y);

                ActualImageHeight = cnv.ActualHeight;
                ActualImageWidth = cnv.ActualWidth;


                clearRectangle("AreaRegio");

                rectangleUpperLeftTemp = RegionUpperLeft;

                rectangleTemp = new System.Windows.Shapes.Rectangle
                {
                    Name = "AreaRegio",
                    Stroke = System.Windows.Media.Brushes.Orange,
                    StrokeThickness = 2,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    Width = RegionLowerRight.X - RegionUpperLeft.X,
                    Height = RegionLowerRight.Y - RegionUpperLeft.Y
                };
                Canvas.SetLeft(rectangleTemp, rectangleUpperLeftTemp.X);
                Canvas.SetTop(rectangleTemp, rectangleUpperLeftTemp.Y);

                cnv.Children.Add(rectangleTemp);
            }
        }

        private bool checkSaveAllowed()
        {
            bool saveAllowed = true;
            for (int i = cnv.Children.Count - 1; i >= 0; i--)
            {
                if (cnv.Children[i] is System.Windows.Shapes.Rectangle)
                {
                    System.Windows.Shapes.Rectangle rectangle = (System.Windows.Shapes.Rectangle)cnv.Children[i];
                    if (rectangle.Name == "AreaRegio" && btnEditRegio.Visibility == Visibility.Visible)
                    {
                        if (rectangle.Width < Convert.ToDouble(AreaMinWidth) || rectangle.Height < Convert.ToDouble(AreaMinHeight))
                        {
                            saveAllowed = false;
                        }
                    }
                }
            }
            return saveAllowed;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static void OnSelectedHardwareChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FhBarcodeUserControl fhBarcodeUserControl && e.NewValue is HardwareModel newHardware)
            {
                fhBarcodeUserControl.SelectedHardware = newHardware;
                fhBarcodeUserControl.SelectedHardwareChanged();
            }
        }

        public void SelectedHardwareChanged()
        {
            if (SelectedHardware != null)
            {
                if (ListFhService != null)
                {
                    SelectedFhService = ListFhService.Where(fhService => fhService.HardwareId == SelectedHardware.Id).FirstOrDefault();
                }
            }
        }
        #endregion

        #region Properties

        public string Image1
        {
            get { return (string)GetValue(Image1Property); }
            set { SetValue(Image1Property, value); }
        }

        public static readonly DependencyProperty Image1Property = DependencyProperty.Register("Image1", typeof(string), typeof(FhBarcodeUserControl));

        public System.Windows.Point RegionUpperLeft
        {
            get { return (System.Windows.Point)GetValue(RegionUpperLeftProperty); }
            set { SetValue(RegionUpperLeftProperty, value); }
        }

        public static readonly DependencyProperty RegionUpperLeftProperty = DependencyProperty.Register("RegionUpperLeft", typeof(System.Windows.Point), typeof(FhBarcodeUserControl));

        public System.Windows.Point RegionLowerRight
        {
            get { return (System.Windows.Point)GetValue(RegionLowerRightProperty); }
            set { SetValue(RegionLowerRightProperty, value); }
        }

        public static readonly DependencyProperty RegionLowerRightProperty = DependencyProperty.Register("RegionLowerRight", typeof(System.Windows.Point), typeof(FhBarcodeUserControl));

        public string AreaMinHeight
        {
            get { return (string)GetValue(AreaMinHeightProperty); }
            set { SetValue(AreaMinHeightProperty, value); }
        }
        public static readonly DependencyProperty AreaMinHeightProperty = DependencyProperty.Register("AreaMinHeight", typeof(string), typeof(FhBarcodeUserControl));

        public string AreaMinWidth
        {
            get { return (string)GetValue(AreaMinWidthProperty); }
            set { SetValue(AreaMinWidthProperty, value); }
        }

        public static readonly DependencyProperty AreaMinWidthProperty = DependencyProperty.Register("AreaMinWidth", typeof(string), typeof(FhBarcodeUserControl));


        public double ActualImageHeight
        {
            get { return (double)GetValue(ActualImageHeightProperty); }
            set { SetValue(ActualImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ActualImageHeightProperty = DependencyProperty.Register("ActualImageHeight", typeof(double), typeof(FhBarcodeUserControl));
        public double ActualImageWidth
        {
            get { return (double)GetValue(ActualImageWidthProperty); }
            set { SetValue(ActualImageWidthProperty, value); }
        }
        public static readonly DependencyProperty ActualImageWidthProperty = DependencyProperty.Register("ActualImageWidth", typeof(double), typeof(FhBarcodeUserControl));

        public ObservableCollection<FhService> ListFhService
        {
            get { return (ObservableCollection<FhService>)GetValue(ListFhServiceProperty); }
            set { SetValue(ListFhServiceProperty, value); }
        }

        public static readonly DependencyProperty ListFhServiceProperty = DependencyProperty.Register("ListFhService", typeof(ObservableCollection<FhService>), typeof(FhBarcodeUserControl));

        public FhService SelectedFhService
        {
            get { return (FhService)GetValue(SelectedFhServiceProperty); }
            set { SetValue(SelectedFhServiceProperty, value); }
        }

        public static readonly DependencyProperty SelectedFhServiceProperty = DependencyProperty.Register(nameof(SelectedFhService), typeof(FhService), typeof(FhBarcodeUserControl),
                new PropertyMetadata(null));
        public HardwareModel SelectedHardware
        {
            get { return (HardwareModel)GetValue(SelectedHardwareProperty); }
            set { SetValue(SelectedHardwareProperty, value); }
        }

        public static readonly DependencyProperty SelectedHardwareProperty = DependencyProperty.Register("SelectedHardware", typeof(HardwareModel), typeof(FhBarcodeUserControl), new PropertyMetadata(null, OnSelectedHardwareChanged));

        public ObservableCollection<ProductDetailJoinModel> ProductDetailList
        {
            get { return (ObservableCollection<ProductDetailJoinModel>)GetValue(ProductDetailListProperty); }
            set { SetValue(ProductDetailListProperty, value); }
        }

        public static readonly DependencyProperty ProductDetailListProperty = DependencyProperty.Register("ProductDetailList", typeof(ObservableCollection<ProductDetailJoinModel>), typeof(FhBarcodeUserControl));

        public ICommand UpdateUICommand
        {
            get { return (ICommand)GetValue(UpdateUICommandProperty); }
            set { SetValue(UpdateUICommandProperty, value); }
        }

        public static readonly DependencyProperty UpdateUICommandProperty = DependencyProperty.Register("UpdateUICommand", typeof(ICommand), typeof(FhBarcodeUserControl), new PropertyMetadata(null));

        #endregion        
    }
}
