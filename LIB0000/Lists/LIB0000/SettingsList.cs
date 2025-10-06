
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using Windows.ApplicationModel.UserDataAccounts.SystemAccess;
using Windows.Media.Capture;

namespace LIB0000
{
    public static class SettingsList
    {
        public static List<SettingModel> GetSettings(HardwareType hardwareType, HardwareFunction hardwareFunction)
        {
            switch (hardwareType)
            {
                case HardwareType.None:

                    if (hardwareFunction == HardwareFunction.None)
                    {
                        List<SettingModel> lSettings = new List<SettingModel>
                        {

                            new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Server database link",
                                            SettingText = "Leeg indien geen server database",ControlType= ControlType.TextBox,
                                            VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "Server=192.168.0.153,1433;User Id = JOLT; Password = JOLT;Encrypt=False;" ,
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Workstation naam",
                                            SettingText = "Unieke naam voor deze workstation",ControlType= ControlType.TextBox,
                                            VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "",
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "003", HardwareFunction = hardwareFunction, SettingName = "Taal",
                                            SettingText = "Taal van het programma",ControlType= ControlType.ComboBox,
                                            VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "Engels",
                                            PossibleComboBoxValues = "Nederlands|Engels", UserVisible = true},

                            new SettingModel { Nr = "004", HardwareFunction = hardwareFunction, SettingName = "Active Directory",
                                            SettingText = "Login Via Active Directory",ControlType= ControlType.ToggleSwitch,
                                            VariableType = VariableType.Bool, MinValue = "", MaxValue = "", DefaultValue = "false" ,
                                            PossibleComboBoxValues = "", UserVisible = true},
                        };

                        return lSettings;
                    }
                    else if (hardwareFunction == HardwareFunction.MachineParTab1)
                    {
                        List<SettingModel> lSettings = new List<SettingModel>
                        {

                            new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Example of textbox",
                                            SettingText = "Info about the textbox.",ControlType= ControlType.TextBox,
                                            VariableType = VariableType.String, MinValue = "0", MaxValue = "10", DefaultValue = "5" ,
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Example of FilePicker",
                                            SettingText = "Info about the FilePicker",ControlType= ControlType.TextBoxWithFilePicker,
                                            VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "" ,
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "003", HardwareFunction = hardwareFunction, SettingName = "Example of FolderPicker",
                                            SettingText = "Info about the FolderPicker",ControlType= ControlType.TextBoxWithFolderPicker,
                                            VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "" ,
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "004", HardwareFunction = hardwareFunction, SettingName = "Example of ComboBox",
                                            SettingText = "Info about the ComboBox",ControlType= ControlType.ComboBox,
                                            VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "KEUZE 1" ,
                                            PossibleComboBoxValues = "KEUZE 1|KEUZE 2|KEUZE 3|KEUZE 4", UserVisible = true},

                            new SettingModel { Nr = "005", HardwareFunction = hardwareFunction, SettingName = "Example of ToggleSwitch",
                                            SettingText = "Info about the ToggleSwitch",ControlType= ControlType.ToggleSwitch,
                                            VariableType = VariableType.Bool, MinValue = "", MaxValue = "", DefaultValue = "true" ,
                                            PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "006", HardwareFunction = hardwareFunction, SettingName = "Example of Slider",
                                            SettingText = "Info about the Slider",ControlType= ControlType.Slider,
                                            VariableType = VariableType.Double, MinValue = "0", MaxValue = "100", DefaultValue = "50" ,
                                            PossibleComboBoxValues = "", UserVisible = true},
                                          

                        };

                        return lSettings;
                    }
                    {
                        return null;
                    }

                case HardwareType.PLC:

                    if (hardwareFunction == HardwareFunction.None)
                    {
                        List<SettingModel> lSettings = new List<SettingModel>
                        {
                            new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Ip adress HMI (voor connectie met PLC)",
                                SettingText = "Ip adress HMI (voor connectie met PLC)", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "10.5.6.110",
                                PossibleComboBoxValues = "", UserVisible = true},
                        };
                        return lSettings;
                    }
                    else
                    {
                        return null;
                    }

                case HardwareType.GigeCam:

                    if (hardwareFunction == HardwareFunction.None)
                    {
                        List<SettingModel> lSettings = new List<SettingModel>
                        {
                            new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Ip adress",
                                SettingText = "Ip adress camera", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "10.5.6.100",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Shutter",
                                SettingText = "Shutter", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "5", MaxValue = "10000", DefaultValue = "500",
                                PossibleComboBoxValues = "", UserVisible = true},
                        };
                        return lSettings;
                    }
                    else
                    {
                        return null;
                    }

                case HardwareType.FHV7:

                    if (hardwareFunction == HardwareFunction.None)
                    {
                        List<SettingModel> lSettings = new List<SettingModel>
                        {
                            new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Ip adress",
                                SettingText = "Ip adress camera", ControlType = ControlType.TextBox,
                                VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "10.5.6.100",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "002", HardwareFunction = hardwareFunction, SettingName = "Shutter",
                                SettingText = "Shutter", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "5", MaxValue = "10000", DefaultValue = "500",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "003", HardwareFunction = hardwareFunction, SettingName = "Gain",
                                SettingText = "Gain", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "0", MaxValue = "240", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "004", HardwareFunction = hardwareFunction, SettingName = "Verlichting boven",
                                SettingText = "Verlichting boven", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "0", MaxValue = "255", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "005", HardwareFunction = hardwareFunction, SettingName = "Verlichting rechts",
                                SettingText = "Verlichting rechts", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "0", MaxValue = "255", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "006", HardwareFunction = hardwareFunction, SettingName = "Verlichting links",
                                SettingText = "Verlichting links", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "0", MaxValue = "255", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = true},

                            new SettingModel { Nr = "007", HardwareFunction = hardwareFunction, SettingName = "Verlichting onder",
                                SettingText = "Verlichting onder", ControlType = ControlType.TextBox,
                                VariableType = VariableType.Int, MinValue = "0", MaxValue = "255", DefaultValue = "0",
                                PossibleComboBoxValues = "", UserVisible = true},
                        };
                        return lSettings;
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
}
