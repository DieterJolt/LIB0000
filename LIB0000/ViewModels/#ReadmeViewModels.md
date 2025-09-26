# Readme ViewModels Folder

View models zijn classes die gebruikt worden om data te presenteren aan de views. 
Deze classes bevatten geen business logica, maar enkel properties die data bevatten. 

Naming rule voor deze classes is : Naam van de view + ViewModel.cs

Hier werken we met de CommunityToolkit van Microsoft.
Deze package bevat een aantal attributen die gebruikt worden om de viewmodels te markeren.

ObservableObject is een base class die gebruikt wordt om de viewmodels te maken.
Hierdoor kunnen we de properties van de viewmodels binden aan de views.
Met observable properties kunnen we de views updaten wanneer de properties van de viewmodels veranderen.

Indien de markering [ObservableProperty] gebruikt wordt, zal de property automatisch een event triggeren wanneer de waarde verandert.
Eronder zie je een private field die gebruikt wordt om de waarde van de property op te slaan.
Achterliggend wordt een public property gemaakt die de private field gebruikt om de waarde op te halen of te wijzigen.

De private field geven we altijd de naming : _ + naam van de property.
De public property geven we altijd de naming : Naam van de property met een hoofdletter ( Deze wordt automatisch gegenereerd door de attribuut)

## 1. ObservableProperty

### 1.1 ObservableProperty in een ViewModel

```csharp

```csharp
namespace LIB1000
{
    public partial class MessagesViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _text = new();        
    }
}
```

### 1.2 Binding van een ObservableProperty in een xaml view


```xaml
<TextBox Text="{Binding Text}" />
```

Om de bindings te zien als je typt in xaml moet je volgende toevoegen bovenaan :

```xaml
DataContext = "{Binding RelativeSource={RelativeSource Self}}"
```

```xaml
<Page
    x:Class="LIB1000.MessagesView"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:ui="http://schemas.lepo.co/wpfui/2022/xaml"
    xmlns:local="clr-namespace:LIB1000"
    
    xmlns:fa="http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp"
    DataContext = "{Binding RelativeSource={RelativeSource Self}}"
    Language="NL-BE">
```


### 1.3 Binding met een Service

Indien dat er een service is geconfigureerd zoals bv MessageService wordt binding als volgt :

```xaml
<TextBox Text="{Binding BasicService.Messages.Text}" />
```


## 2. ObservableCollection

## 2.1 ObservableCollection in een ViewModel

ObservableCollection is een class die gebruikt wordt om een collectie van objecten te presenteren aan de views.

Hier is de naming rule voor de property : _ + naam van de property

```csharp
		[ObservableProperty]
		private ObservableCollection<object> _listHistory = new();
```

## 2.2 Binding van een ObservableCollection in een xaml view

```xaml
<ListView ItemsSource="{Binding ListHistory}" />
```

## 3. RelayCommand

### 3.1 RelayCommand in een ViewModel

RelayCommand is een class die gebruikt wordt om commando's te binden aan de views.

Hier is de naming rule voor de methode : MethodeNaam met hoofdletter

Hier wordt dan automatisch een property gemaakt met de naam van de methode + Command

```csharp
        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }
```

### 3.2 Binding van een RelayCommand in een xaml view

```xaml
<Button Command="{Binding OnCounterIncrementCommand}" />
```

## 4. RelayCommand met parameter

### 4.1 RelayCommand met parameter in een ViewModel

RelayCommand kan ook gebruikt worden met parameters.

```csharp
		[RelayCommand]
		private void OnCounterIncrement(int value)
		{
			Counter += value;
		}
```

### 4.2 Binding van een RelayCommand met parameter in een xaml view

```xaml
<Button Command="{Binding OnCounterIncrementCommand}" CommandParameter="1" />
```