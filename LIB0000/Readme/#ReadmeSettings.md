# Readme Settings

## Seting toevoegen

## 1.1 Setting toevoegen in SettingsList

In SettingsList.cs kan je onder MachineParTab1 voorbeelden vinden van Settings die je kan toevoegen.

Deze settings zijn :
1. Texbox
2. FilePicker
3. FolderPicker
4. ComboBox
5. ToggleSwitch
6. Slider

VoorBeeld:
Je wil een Texbox setting toevoegen die een text opslaat.

Hiervoor kan je deze code gebruiken:
```csharp
new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Example of textbox",
                   SettingText = "Info about the textbox.",ControlType= ControlType.TextBox,
                   VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "" ,
                   PossibleComboBoxValues = "", UserVisible = true},
```
Bij deze code pas je volgende delen aan:
1. Nr : Uniek nummer van de setting
2. SettingName : Naam van de setting
3. SettingText : Info over de setting
4. DefaultValue : Standaard waarde van de setting


## 1.2 Parameter aanmaken in MachineParTyp

In MachineParTyp moet er een property worden aangemaakt voor de setting.

Bijvoorbeeld:
```csharp
[ObservableProperty]
        string _exampleOfTextbox;
```


## 1.3 Setting toevoegen in globalService

In gobalService moet de setting worden toegevoegd in de methode getSettings.

Bijvoorbeeld:
```csharp
Machine.Par.ExampleOfTextbox = Convert.ToString(BasicService.SettingsService.GetSetting(
                               "001", 0, HardwareFunction.MachineParTab1));
```

Als deze stappen voltooid zijn is de setting toegevoegd en kan deze gebruikt worden.



## 2. Nieuwe tab aanmaken

## 2.1 HardwareFunction toevoegen

In BasicService.cs voeg je een onder "public enum HardwareFunction" een nieuwe function toe. 

Bijvoorbeeld:
```csharp
MachineParTab2 = 2,
```

## 2.2 Code toevoegen in Constructor Basicservice

In de constructor van BasicService.cs voeg je een nieuwe result toe.

Bijvoorbeeld:
```csharp
result = SettingsService.AddToSettingsDatabase(SettingsList.GetSettings(HardwareType.None,
HardwareFunction.MachineParTab1), 0, "MachineTab2");
```
In de Displayname kan je de naam van de Tab aanpassen. In dit voorbeeld is dat "MachineTab2".

## 2.3 Tab toevoegen in SettingsList.cs

In SettingsList.cs voeg je een else if toe in de Hardware.None case

Bijvoorbeeld:
```csharp
else if (hardwareFunction == HardwareFunction.MachineParTab2)
            {
                settings = new List<SettingModel>
                {
                    new SettingModel { Nr = "001", HardwareFunction = hardwareFunction, SettingName = "Example of textbox",
                                       SettingText = "Info about the textbox.",ControlType= ControlType.TextBox,
                                       VariableType = VariableType.String, MinValue = "", MaxValue = "", DefaultValue = "" ,
                                       PossibleComboBoxValues = "", UserVisible = true},
                };
            }
```

In dit voorbeeld is de nieuwe Tab toegevoegt samen met een voorbeeld setting. (Voorbeeldsetting zie 1.1)

Als deze stappen voltooid zijn is de Tab toegevoegd en kunnen er settings in toegevoegt worden zoals
de voorbeeldsetting van in 1.1.

