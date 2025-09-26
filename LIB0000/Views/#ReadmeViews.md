# Readme Views Folder

Views zijn de pagina's die de gebruiker te zien krijgt. 
In deze folder staan alle views die gebruikt worden.

De algemene view is altijd 'MainView'. Deze view wordt altijd als eerste geladen.
Hierin worden alle andere views geladen. Deze is ook de enige van het type
'Window'. Alle andere views zijn van het type 'Page'.

Toevoegen van een nieuwe view en deze laden in de MainView kan als volgt:

## 1. Maak een nieuwe view aan

- Rechtsklik op het project en kies 'Add' -> 'Page (WPF)'.

- Naming rule hier is : 'NaamVanDeView' + 'View'. Bijvoorbeeld: 'LoginView'.

- Klik op 'Add'.

- Verplaats deze naar de folder Views en kies om de namespace niet aan te passen.



## 2. Voeg de nieuwe view toe in App.xaml.cs

```csharp
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ApplicationHostService>();

                //Voeg de services toe
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<MessageService>();
                services.AddSingleton<SettingService>();
                services.AddSingleton<GlobalService>();
                //Voeg de views toe
                services.AddSingleton<MainView>();
                services.AddSingleton<SettingsView>();
                services.AddSingleton<HistoriekView>();
                services.AddSingleton<MessagesView>();
                services.AddSingleton<HomeView>();
                services.AddSingleton<AboutView>();
                services.AddSingleton<LoginView>();
                // Voeg de viewmodels toe
                services.AddSingleton<MainViewModel>();             
                services.AddSingleton<AboutViewModel>();
                services.AddSingleton<MessagesViewModel>();
            }).Build();
```

- Voeg de nieuwe view toe aan de services in App.xaml.cs.

```csharp
				services.AddSingleton<NaamVanDeView>();
```


## 3. Voeg de nieuwe view toe aan de MainViewModel.cs

```csharp
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Algemeen overzicht",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home32 },
                TargetPageType = typeof(HomeView)                
            },
            new NavigationViewItem()
            {
                Content = "Meldingen",
                Icon = new SymbolIcon { Symbol = SymbolRegular.CommentMultiple32 },
                TargetPageType = typeof(MessagesView)
            },
            new NavigationViewItem()
            {
                Content = "Gebruikers & beveiliging",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Key32 },
                TargetPageType = typeof(LoginView)                
            },
            new NavigationViewItem()
            {
                Content = "Instellingen",
                Icon = new SymbolIcon { Symbol = SymbolRegular.ContentSettings32 },
                TargetPageType = typeof(SettingsView)                
            },
        };
```

- Voeg de nieuwe view toe aan de ObservableCollection '_menuItems' in de MainView.

```csharp
			new NavigationViewItem()
			{
				Content = "NaamVanDeView",
				Icon = new SymbolIcon { Symbol = SymbolRegular.IconNaam32 },
				TargetPageType = typeof(NaamVanDeView)
			},
```

- De nieuwe view is nu toegevoegd aan de MainView en kan worden geladen.

-  De icoon kan worden aangepast door een andere 'Symbol' te kiezen.                                                 
De lijst is terug op : https://github.com/microsoft/fluentui-system-icons/blob/main/icons_regular.md_

```csharp
  Icon = new SymbolIcon { Symbol = SymbolRegular.ContentSettings32 },
```






