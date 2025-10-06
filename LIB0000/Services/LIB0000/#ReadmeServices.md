# Readme Services Folder

De folder services bevat de services die gebruikt worden in de applicatie. 
Deze services worden gebruikt om de data van de backend op te halen en te verwerken. 
De services worden aangeroepen vanuit de componenten.

## 1. Maak een nieuwe Service aan

Om een nieuwe service aan te maken, maak je een nieuwe class aan in de services folder.
Naming rule is dat de naam van de class moet eindigen op 'Service'.
Neem nu als voorbeeld de service 'MessagesService.cs'.
		
## 2. In App.xaml.cs, voeg de service toe aan de dependency injection container

Bij de methodes van App.xaml.cs file, voeg de service toe aan de dependency injection container.
Dit doe je door de volgende code toe te voegen in de ConfigureServices methode:

```csharp
                //Voeg de services toe
                services.AddSingleton<MessageService>();
```

## 3. Gebruik de service in een component

Om de service te gebruiken in een component, voeg je de service toe aan de constructor van de component.
Dit doe je door de volgende code toe te voegen in de component:

```csharp
		private readonly MessageService _messageService;

		public MessagesComponent(MessageService messageService)
		{
			_messageService = messageService;
		}
```

## 4. Standaard services

We hebben 2 standaard services die gebruikt worden in de applicatie:

	- ApplicationHostService: Deze service wordt gebruikt om de applicatie te starten en te stoppen.
	- GlobalService: Deze service wordt gebruikt om globale data op te slaan en te gebruiken in de applicatie.
	
	DVH Nog extra informatie hierover


## 5. Voorbeeld service component binden in een view


### 5.1 Voeg een nieuwe service toe onder de folder Services

bijvoorbeeld LoginLogoutService.cs

### 5.2 Voeg de code toe in deze service

Hier wordt nu een commando Count en een variabele Counter toegevoegd

```csharp
        #region Commands
        [RelayCommand]
        private void Count()
        {
            Counter++;
        }

        #endregion
        #region Constructor
        #endregion
        #region Events
        #endregion
        #region Fields
        #endregion
        #region Methods

        #endregion
        #region Properties
        [ObservableProperty]
        private int _counter;

        #endregion
```		

### 5.3 Voeg de service toe in App.xaml.cs

Bij de methodes van App.xaml.cs file, voeg de service toe aan de dependency injection container.
Dit doe je door de volgende code toe te voegen in de ConfigureServices methode:

```csharp
                //Voeg de services toe
                services.AddSingleton<LoginLogoutService>();
```

### 5.4 Een view aanmaken, beschreven in Views/ReadmeView

### 5.5 Gebruik deze service in deze view

Om de service te gebruiken in een component, voeg je de service toe aan de constructor van de component.
Dit doe je door de volgende code toe te voegen in de component:

Dus in de constructor wordt de service aangemaakt
In field maak je een copy van de service.
 
```csharp
        #region Commands
        #endregion

        #region Constructor

        public UserLoginLogoutView(LoginLogoutService loginLogoutService)
        {
            LoginLogoutService = loginLogoutService;
            DataContext = this;
            InitializeComponent();

        }
        #endregion

        #region Events
        #endregion

        #region Fields
        public LoginLogoutService LoginLogoutService { get; }
        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion
```

### 5.6 Binding in de xaml

In de UserLoginLogoutView.xaml

1. Binding RelativeSource toevoegen
2. Binding naar command CountCommand
3. Binding naar variabele Counter

```xaml
<Page x:Class="LIB1000.UserLoginLogoutView"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
      xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
      xmlns:local="clr-namespace:LIB1000"
      mc:Ignorable="d" 
      DataContext = "{Binding RelativeSource={RelativeSource Self}}"
      d:DesignHeight="450" d:DesignWidth="800"
      Title="UserLoginLogoutView">

    <Grid>
        <TextBlock Text="{Binding LoginLogoutService.Counter}"/>
        <Button Margin="325,271,0,0" VerticalAlignment="Top" Command="{Binding LoginLogoutService.CountCommand }">Add</Button>
    </Grid>
</Page>
```

						
							
								
									
													
