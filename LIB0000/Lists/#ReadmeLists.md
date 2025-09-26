# Readme Lists Folder

In deze folder staan lijsten die gebruikt worden in het project.

	- MessagesList : hier staan alle messages die gebruikt worden in het project
	- SettingsList : hier staan alle settings die gebruikt worden in het project

## 1. MessageList

### 1.1 Gebruik maken van de MessageService

De MessageService is een service die gebruikt wordt om messages te beheren.

Voeg de MessageService toe aan de services in Methods van de App.xaml.cs

```csharp
			services.AddSingleton<MessageService>();

```

Gebruik de service in de code via dependency injection

```csharp
		private readonly MessageService MessageService;

		public MainView(MessageService messageService)
		{
			MessageService = messageService;
		}
```


### 1.2 Samenstellen van de MessageList

Hier staan alle messages die gebruikt worden in het project.
Deze messages kunnen van het type `error`, `warning` of `info` zijn.
Het is de bedoeling als volgt :

In MessageList.cs van LIB1000.HMI worden de algemene messages van het project opgeslagen
Bij andere libraries die we maken, kunnen we een aparte MessageList.cs maken waarin we de messages van die library opslaan

Bij de opstarten van de HMI worden alle messages van de verschillende libraries samengevoegd in de MessageList van de HMI,
via de functie 'AddMessagesToList' in MainView.xaml.cs
  
```csharp
        public void AddMessagesToList()
        {
            // Add your lists of messages here from the different services
            List<MessageModel> lMessages = new List<MessageModel>();
            lMessages.AddRange(MessagesList.GetMessages("Systeem"));
            lMessages.AddRange(MessagesList.GetMessages("Algemeen"));
            lMessages.AddRange(LIB0002.MessagesList.GetMessages("Robot"));
        }
```

### 1.3 Bewaren van de MessageList

De messages worden opgeslagen in de database via de functie 'SaveMessagesListToDatabase' in MainView.xaml.cs
Dit is via SQLite, een lokale database die in de applicatie zit op de locatie @"C:\FH\Messages.db"

```csharp
        public void SaveMessagesListToDatabase(List<MessageModel> lMessages)
        {
            // Create the list of possible messages to the database
            bool result;
            result = MessageService.CreateListOfPossibleMessagesInDatabase(lMessages);
            if (result == false) { System.Windows.MessageBox.Show("Probleem bij opstart van applicatie : Toevoegen van messages database niet gelukt"); }
        }
```

### 1.4 Gebruik van de MessageList

In de thread van de applicatie kunnen de messages worden geset en gereset.
Als deze messages worden geset, worden ze toegevoegd aan de ActiveMessagesList en HistoryMessagesList van de MessageService.
Als deze messages worden gereset, worden ze verwijderd uit de ActiveMessagesList van de MessageService.


Voorbeeld van het setten van een message :

```csharp
MessageService.SetItemActiveMessage("Camera", "001");
```

Voorbeeld van het resetten van de message :

```csharp
MessageService.ResetItemActiveMessage("Camera", "001");
```


### 1.5 Toevoegen van MessageList van een andere library

Als je bijvoorbeeld een library LIB0002 gebruiken die een MessageList bevat, dan kan je deze toevoegen aan de MessageList van de HMI.

```csharp
			lMessages.AddRange(LIB0002.MessagesList.GetMessages("Robot"));
```


## 2. SettingsList

SettingsList werkt op dezelfde manier als MessageList.
Hier staan alle settings die gebruikt worden in het project.
Ook kunnen settings van verschillende libraries worden samengevoegd in de SettingsList van de HMI.


