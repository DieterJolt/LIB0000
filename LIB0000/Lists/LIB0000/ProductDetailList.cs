
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using Windows.ApplicationModel.UserDataAccounts.SystemAccess;
using Windows.Media.Capture;

namespace LIB0000
{
    public static class ProductDetailList
    {
        public static List<ProductDetailModel> GetSettings(HardwareType hardwareType, HardwareFunction hardwareFunction)
        {
            switch (hardwareType)
            {
                case HardwareType.None:

                    if (hardwareFunction == HardwareFunction.None)
                    {
                        List<ProductDetailModel> listProductDetail = new List<ProductDetailModel>
                        {
                            new ProductDetailModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Lengte",
                                            SettingText = "Lengte product",ControlType= ControlType.TextBox,
                                            VariableType = VariableType.Int, MinValue = "", MaxValue = "", DefaultValue = "10" ,
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new ProductDetailModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Breedte",
                                            SettingText = "Breedte product",ControlType= ControlType.TextBox,
                                            VariableType = VariableType.Int, MinValue = "", MaxValue = "", DefaultValue = "20",
                                            PossibleComboBoxValues = "", UserVisible = true},
                        };
                        return listProductDetail;
                    }
                    else
                    {
                        return null;
                    }

                case HardwareType.GigeCam:

                    if (hardwareFunction == HardwareFunction.GigeCamShapeSearch)
                    {
                        List<ProductDetailModel> listProductDetail = new List<ProductDetailModel>
                        {
                            new ProductDetailModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Correlatie",
                                SettingText = "Correlatie", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "5", MaxValue = "100", DefaultValue = "90",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new ProductDetailModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Hoek",
                                SettingText = "Hoek", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "-5", MaxValue = "5", DefaultValue = "5",
                                PossibleComboBoxValues = "", UserVisible = true},
                        };
                        return listProductDetail;
                    }
                    else if (hardwareFunction == HardwareFunction.GigeCamCounting)
                    {
                        List<ProductDetailModel> listProductDetail = new List<ProductDetailModel>
                        {
                            new ProductDetailModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Counting number",
                                SettingText = "Barcode", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "5",
                                PossibleComboBoxValues = "", UserVisible = true},
                        };
                        return listProductDetail;
                    }
                    else
                    {
                        return null;
                    }

                case HardwareType.FHV7:

                    if (hardwareFunction == HardwareFunction.Fhv7ShapeSearch)
                    {
                        List<ProductDetailModel> listProductDetail = new List<ProductDetailModel>
                        {
                            new ProductDetailModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Correlatie",
                                SettingText = "Correlatie", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "5", MaxValue = "100", DefaultValue = "90",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new ProductDetailModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Hoek",
                                SettingText = "Hoek", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "-180", MaxValue = "180", DefaultValue = "5",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new ProductDetailModel { Nr = "003", HardwareFunction = hardwareFunction, SettingName = "RegionUpperLeftX",
                                SettingText = "RegionUpperLeftX", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "004", HardwareFunction = hardwareFunction, SettingName = "RegionUpperLeftY",
                                SettingText = "RegionUpperLeftY", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "005", HardwareFunction = hardwareFunction, SettingName = "RegionLowerRightX",
                                SettingText = "RegionLowerRightX", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "200",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "006", HardwareFunction = hardwareFunction, SettingName = "ModelLowerRightY",
                                SettingText = "ModelLowerRightY", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "200",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "007", HardwareFunction = hardwareFunction, SettingName = "ModelUpperLeftX",
                                SettingText = "ModelUpperLeftX", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "50",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "008", HardwareFunction = hardwareFunction, SettingName = "ModelUpperLeftY",
                                SettingText = "ModelUpperLeftY", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "50",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "009", HardwareFunction = hardwareFunction, SettingName = "ModelLowerRightX",
                                SettingText = "ModelLowerRightX", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "100",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "010", HardwareFunction = hardwareFunction, SettingName = "ModelLowerRightY",
                                SettingText = "ModelLowerRightY", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "100",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "011", HardwareFunction = hardwareFunction, SettingName = "ImageWidth",
                                SettingText = "ImageWidth", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "100",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "012", HardwareFunction = hardwareFunction, SettingName = "ImageHeight",
                                SettingText = "ImageHeight", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "100",
                                PossibleComboBoxValues = "", UserVisible = false},

                        };
                        return listProductDetail;
                    }
                    else if (hardwareFunction == HardwareFunction.Fhv7Barcode)
                    {
                        List<ProductDetailModel> listProductDetail = new List<ProductDetailModel>
                        {
                            new ProductDetailModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Barcode",
                                SettingText = "Barcode", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "5", MaxValue = "100", DefaultValue = "90",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new ProductDetailModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "RegionUpperLeftX",
                                SettingText = "RegionUpperLeftX", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "003", HardwareFunction = hardwareFunction, SettingName = "RegionUpperLeftY",
                                SettingText = "RegionUpperLeftY", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "004", HardwareFunction = hardwareFunction, SettingName = "RegionLowerRightX",
                                SettingText = "RegionLowerRightX", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "200",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "005", HardwareFunction = hardwareFunction, SettingName = "RegionLowerRightY",
                                SettingText = "RegionLowerRightY", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "200",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "006", HardwareFunction = hardwareFunction, SettingName = "ImageWidth",
                                SettingText = "ImageWidth", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "100",
                                PossibleComboBoxValues = "", UserVisible = false},

                            new ProductDetailModel { Nr = "007", HardwareFunction = hardwareFunction, SettingName = "ImageHeight",
                                SettingText = "ImageHeight", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Double, MinValue = "0", MaxValue = "10000", DefaultValue = "100",
                                PossibleComboBoxValues = "", UserVisible = false},
                        };
                        return listProductDetail;
                    }
                    else
                    {
                        return null;
                    }

                default:
                    return null;
            }
        }
    }

    public static class HardwareFunctionMapper
    {
        public static readonly Dictionary<HardwareType, ObservableCollection<HardwareFunction>> HardwareFunctionMap = new()
        {
            { HardwareType.None, new ObservableCollection<HardwareFunction> { HardwareFunction.None } },
            { HardwareType.GigeCam, new ObservableCollection<HardwareFunction> { HardwareFunction.GigeCamShapeSearch, HardwareFunction.GigeCamCounting } },
            { HardwareType.FHV7, new ObservableCollection<HardwareFunction> { HardwareFunction.Fhv7ShapeSearch, HardwareFunction.Fhv7Barcode } },
            { HardwareType.V430, new ObservableCollection<HardwareFunction> { HardwareFunction.V430Barcode } },
            { HardwareType.PLC, new ObservableCollection<HardwareFunction> { HardwareFunction.None } }
        };

        public static ObservableCollection<HardwareFunction> GetFunctionsForHardware(HardwareType hardwareType)
        {
            return HardwareFunctionMap.TryGetValue(hardwareType, out var functions) ? functions : new ObservableCollection<HardwareFunction>();
        }
    }
}
