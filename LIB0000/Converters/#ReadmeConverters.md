# Readme Converters Folder

Converters worden gebruikt om data te converteren van het ene type naar het andere type. 
In dit voorbeeld wordt er gebruik gemaakt van een `Bool` naar `String` converter.

## 1. Code in de class

Naming rule = Converter + inputType + To + outputType + [optional] + [optional]

[optional] = extra informatie over de conversie zoals 1 wordt visible en 0 wordt hidden

```csharp
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
```

## 2. Static resource in de XAML

De converter wordt gedefinieerd in de resources van App.xaml

```xml
<Application
    x:Class="LIB1000.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml"
    xmlns:local="clr-namespace:LIB1000"
    DispatcherUnhandledException="OnDispatcherUnhandledException"
    Exit="OnExit"
    Startup="OnStartup">
<Application.Resources>
    <ResourceDictionary>
        <local:ConverterBoolToVisibility1Visible0Hidden x:Key="ConverterBoolToVisibility1Visible0Hidden"/>
</ Application.Resources >
</ Application >
```

## 3. Binding in de XAML

De converter wordt gebruikt in de binding van een control.

```xml
<Window
	x:Class="LIB1000.MainWindow"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	xmlns:local="clr-namespace:LIB1000"
	mc:Ignorable="d"
	Title="MainWindow" Height="450" Width="800">
	<Grid>
		<Button Content="Button" Visibility="{Binding IsVisible, Converter={StaticResource ConverterBoolToVisibility1Visible0Hidden}}"/>
	</Grid>
    </Window>
```

