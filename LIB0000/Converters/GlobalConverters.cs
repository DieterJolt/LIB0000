using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FontAwesome.Sharp;
using Wpf.Ui.Controls;

namespace LIB0000
{
    public class CanvasRatioMultiBoolToVisibilityConverterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var value in values)
            {
                if (value is bool b && b)
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterBoolToVisibility0Visible1Hidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Hidden : Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
    public class ApprovedOnTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 4) return string.Empty;

            string name = values[0] as string ?? "";
            string version = values[1]?.ToString() ?? "";
            string approvedDate = values[2]?.ToString() ?? "";
            bool isApproved = values[3] is bool && (bool)values[3];

            if (isApproved)
                return $"{name} [ versie {version} ] [ Approved on {approvedDate} ]";
            else
                return $"{name} [ versie {version} ] [ Not approved ]";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ProductGroupIdToNameMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int productGroupId && values[1] is List<ProductGroupModel> productGroupList)
            {
                // Zoek de productgroup met het gegeven ID
                var productGroup = productGroupList.FirstOrDefault(i => i.Id == productGroupId);
                return productGroup?.Name ?? "";
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is string productGroupName && parameter is List<ProductGroupModel> productGroupList)
            {
                var productGroup = productGroupList.FirstOrDefault(i => i.Name == productGroupName);
                return new object[] { productGroup?.Id ?? 0, productGroupList };
            }
            return new object[] { 0, parameter };
        }
    }

    public class ProductIdToNameMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int ProductId && values[1] is List<ProductModel> instructionList)
            {
                // Zoek de product met het gegeven ID
                var Product = instructionList.FirstOrDefault(i => i.Id == ProductId);
                return Product?.Name ?? "";
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is string ProductName && parameter is List<ProductModel> ProductList)
            {
                var Product = ProductList.FirstOrDefault(i => i.Name == ProductName);
                return new object[] { Product?.Id ?? 0, ProductList };
            }
            return new object[] { 0, parameter };
        }
    }

   

    public class UserNameAndLevelMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2)
                return string.Empty;

            string firstName = values[0]?.ToString() ?? string.Empty;
            string lastName = values[1]?.ToString() ?? string.Empty;

            return $"{firstName} {lastName}"; // Combineer de waarden
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CheckboxMultiVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                return DependencyProperty.UnsetValue;

            string stringValue = values[0] as string;
            if (values[1] is bool boolValue)
            {
                if (boolValue == true)
                {
                    return Visibility.Visible;
                }
                else if (boolValue == false && stringValue != "")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }

            }
            return string.IsNullOrEmpty(stringValue) ? Visibility.Hidden : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringMultiVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                return DependencyProperty.UnsetValue;

            string stringValue = values[0] as string;
            if (values[1] is Visibility visibilityValue)
            {


                if (visibilityValue == Visibility.Visible && stringValue != "")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            return string.IsNullOrEmpty(stringValue) ? Visibility.Hidden : Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ByteArrayToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2) { return Visibility.Hidden; }
            byte[] lastByteArray = ConvertImageToByteArray("pack://application:,,,/Assets/Images/VoorbeeldAfbeelding.png");

            if (values[0] is byte[] currentByteArray && values[1] is bool b)
            {
                if ((AreArraysEqual(currentByteArray, lastByteArray)) && (b == false))
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool AreArraysEqual(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null || array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
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

    }

   

   



    

    public class ConverterBoolToInverseBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return false;
        }
    }

    public class ConverterVisibilityToInverseVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            }

            return Visibility.Hidden;
        }
    }

    //multiConverter

    public class MultiConverterEqualToTrue : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2 || values[0] == null || values[1] == null)
                return false;

            return values[0].ToString() == values[1].ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // null
    public class ConverterNullOr0VisibilityNullHiddenNotnullVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return Visibility.Hidden; }

            if (value is int i)
            {
                return i == 0 ? Visibility.Hidden : Visibility.Visible;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // bool
    public class ConverterBoolToColor0Transparent1Red : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? App.ColorRed : Brushes.Transparent;
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToColor0Transparent1Green : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? App.ColorGreen : Brushes.Transparent;
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToColor0Transparent1Yellow : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? App.ColorYellow : Brushes.Transparent;
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToColor1Transparent0Red : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? Brushes.Transparent : App.ColorRed;
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToColor0Transparent1Orange : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? App.ColorOrange : Brushes.Transparent;

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToColor0Transparent1Blue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is bool boolValue)
                return boolValue ? App.ColorBlue : Brushes.Transparent;

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToColor0Red1DarkGray : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? App.ColorDarkGray : App.ColorRed;

            return App.ColorRed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class ConverterBoolToVisibility1Visible0Hidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Hidden;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterBoolToVisibility1Visible0Collapsed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterBoolToColor0Red1Green : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? App.ColorGreen : App.ColorRed;
            return App.ColorRed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterIntToGridVisibility0NotVisible1Visible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue == 1 ? "*" : "0*";
            }

            return "0*";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterValueDivideBy2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double returnValue = 0;
            if (value is Single s)
            {
                returnValue = s / 2;
            }
            if (value is Double d)
            {
                returnValue = d / 2;
            }

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // enum
    public class ConverterEnumToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not string enumString)
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
            }

            if (!Enum.IsDefined(typeof(Wpf.Ui.Appearance.ApplicationTheme), value))
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");
            }

            var enumValue = Enum.Parse(typeof(Wpf.Ui.Appearance.ApplicationTheme), enumString);

            return enumValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is not string enumString)
            {
                throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
            }

            return Enum.Parse(typeof(Wpf.Ui.Appearance.ApplicationTheme), enumString);
        }
    }

    //enum

    public class ConverterEnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            int intValue;

            // Check if value is an Enum and convert to int
            if (value.GetType().IsEnum)
            {
                intValue = (int)value;
            }
            else if (value is int)
            {
                intValue = (int)value;
            }
            else
            {
                return Visibility.Collapsed;
            }

            if (int.TryParse(parameter.ToString(), out int paramValue))
            {
                return intValue == paramValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    // messagetype
    public class ConverterMessageTypeToBackgroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageType messageType)
            {
                switch (messageType)
                {
                    case MessageType.Error:
                        return App.ColorRed;
                    case MessageType.Warning:
                        return App.ColorOrange;
                    case MessageType.Info:
                        return App.ColorBlue;
                    case MessageType.Wait:
                        return Brushes.Transparent;
                    case MessageType.Running:
                        return App.ColorGreen;
                    default:
                        return Brushes.Transparent;
                }
            }
            return Brushes.Transparent;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterMessageTypeToForegroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageType messageType)
            {
                switch (messageType)
                {
                    case MessageType.Error:
                        return App.ColorWhite;
                    case MessageType.Warning:
                        return App.ColorWhite;
                    case MessageType.Info:
                        return App.ColorBlack;
                    case MessageType.Wait:
                        return App.ColorWhite;
                    case MessageType.Running:
                        return App.ColorWhite;
                    default:
                        return Brushes.Transparent;
                }
            }
            return Brushes.Transparent;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterMessageTypeToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is MessageType messageType)
            {
                switch (messageType)
                {
                    case MessageType.Error:
                        return IconChar.TimesCircle;
                    case MessageType.Warning:
                        return IconChar.Warning;
                    case MessageType.Info:
                        return IconChar.InfoCircle;
                    case MessageType.Wait:
                        return IconChar.StopCircle;
                    case MessageType.Running:
                        return IconChar.PlayCircle;
                    default:
                        return IconChar.Question;
                }
            }
            return new SymbolIcon(SymbolRegular.Info24);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // create the converter message type to string
    public class ConverterMessageTypeToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageType messageType)
            {
                switch (messageType)
                {
                    case MessageType.Error:
                        return "Error ";
                    case MessageType.Warning:
                        return "Warning";
                    case MessageType.Info:
                        return "Information";
                    case MessageType.Wait:
                        return "Machine is stopped";
                    case MessageType.Running:
                        return "Machine is running";
                    default:
                        return "Unknown";
                }
            }
            return "Unknown";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // screenvaluemode
    public class ConverterScreenValueModeToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScreenValueMode ssv)
            {
                switch (ssv)
                {
                    case ScreenValueMode.None:
                        return Brushes.Transparent;
                    case ScreenValueMode.Changed:
                        return App.ColorYellow;
                    case ScreenValueMode.BelowMinValue:
                        return App.ColorRed;
                    case ScreenValueMode.AboveMaxValue:
                        return App.ColorRed;
                    default:
                        return Brushes.Transparent;
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterScreenValueModeString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScreenValueMode ssv)
            {
                switch (ssv)
                {
                    case ScreenValueMode.None:
                        return "";
                    case ScreenValueMode.Changed:
                        return "";
                    case ScreenValueMode.BelowMinValue:
                        return "Waarde onder minimumwaarde !";
                    case ScreenValueMode.AboveMaxValue:
                        return "Waarde boven maximumwaarde !";
                    default:
                        return ""; ;
                }
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // string
    public class ConverterStringToString1Aan : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                s = s.ToLower();
                if (s == "true" || s == "1" || s == "aan" || s == "on") { return "Aan"; }
            }

            return "Uit";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConverterStringToComboBoxItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                string[] items = s.Split("|");
                return new ObservableCollection<string>(items);
            }

            return new ObservableCollection<string>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // This method is not used for a one-way binding
            throw new NotImplementedException();
        }
    }
    public class ConverterStringToVisibilityEmptyStringCollapsed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                return string.IsNullOrEmpty(s) ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterStringToVisibilityEmptyStringHidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterStringToBool1True : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                s = s.ToLower();
                if (s == "true" || s == "1" || s == "aan" || s == "on") { return true; }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {

                return b ? "true" : "false";
            }
            return "false";
        }
    }

    //List
    public class ConverterObservableCollectionHardwareFunctionCountGreaterThenParameterVisble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<HardwareFunction> listHardwareFunctions)
            {
                return listHardwareFunctions.Count > System.Convert.ToInt32(parameter.ToString()) ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value is not int intValue)
            {
                return Visibility.Collapsed; // Zet op Collapsed als er geen waarde is of het geen integer is.
            }

            if (parameter is string expectedValue && int.TryParse(expectedValue, out int expectedIntValue))
            {
                return intValue == expectedIntValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed; // Als de parameter niet overeenkomt of ongeldig is, zet op Collapsed
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class OddEvenIntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue % 2 == 0 ? App.ColorDarkGray : App.ColorGray;
            }

            // Standaardkleur als het geen int is
            return App.ColorDarkGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // HardwareFunction

    public class ConverterHardWareFunctionToVisibilityFhv7ShapeSearchVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HardwareFunction hardwareFunction && hardwareFunction == HardwareFunction.Fhv7ShapeSearch)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterHardWareFunctionToVisibilityFhv7BarcodeVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HardwareFunction hardwareFunction && hardwareFunction == HardwareFunction.Fhv7Barcode)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //Fontsize
    public class ConverterActualHeightToFontSize : IValueConverter
    {
        public double MinFontSize { get; set; } = 26;
        public double MaxFontSize { get; set; } = 64;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height)
            {
                double scaledSize = height * 0.6;
                return Math.Max(MinFontSize, Math.Min(scaledSize, MaxFontSize));
            }
            return 14.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ConverterJoinId : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[1] is IEnumerable<object> list && values[0] != null && parameter is string param)
            {
                var propertyNames = param.Split(',');
                if (propertyNames.Length != 2) return "";

                string idProp = propertyNames[0].Trim();
                string nameProp = propertyNames[1].Trim();
                var idValue = values[0];

                foreach (var item in list)
                {
                    var itemId = item.GetType().GetProperty(idProp)?.GetValue(item);
                    if (itemId != null && itemId.Equals(idValue))
                    {
                        return item.GetType().GetProperty(nameProp)?.GetValue(item)?.ToString() ?? "";
                    }
                }
            }
            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is string name && parameter is string param && targetTypes.Length == 2)
            {
                var propertyNames = param.Split(',');
                if (propertyNames.Length != 2) return new object[] { 0, null };

                string idProp = propertyNames[0].Trim();
                string nameProp = propertyNames[1].Trim();

                // Since values[1] isn't passed here, this needs to be handled externally
                return Binding.DoNothing as object[]; // or throw NotSupportedException
            }

            return new object[] { 0, null };
        }
    }


}
