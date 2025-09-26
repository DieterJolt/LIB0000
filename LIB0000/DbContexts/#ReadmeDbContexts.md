# Readme DbContext Folder

Deze folder bevat de DbContext files voor de database. 
De DbContext files zijn de files die de database connectie en de database structuur beschrijven.

## 1. Voorbeeld van de datacontext

Voorbeeld van een DbContext file:
```csharp
    public class LocalDbContext : DbContext
    {
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<MessageActiveModel> MessagesActive { get; set; }
        public DbSet<MessageHistoryLineModel> MessagesHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuiler)
        {
            // fix path
            string databasePath = @"C:\FH\Messages.db";
            
            // Ensure the directory exists
            string directory = Path.GetDirectoryName(databasePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            optionsBuiler.UseSqlite("Data Source=" + databasePath);
        }
            
    }
```
Hier worden de verschillende tabellen van de database beschreven.
Deze tabellen zijn de `Messages`, `MessagesActive` en `MessagesHistory` tabellen.
Deze tabellen worden beschreven door de `MessageModel`, `MessageActiveModel` en `MessageHistoryLineModel` modellen.

De `OnConfiguring` methode zorgt ervoor dat de database connectie wordt geconfigureerd.
In dit geval wordt er gebruik gemaakt van een SQLite database.
De database wordt opgeslagen in de `C:\FH\Messages.db` file.
Als de file niet bestaat wordt deze aangemaakt.


## 2. Voorbeeld van het aanmaken van een nieuwe tabel

### 2.1 In de folder Model maken we een model aan

In dit geval is dit UsersModel.
Dus in dit geval zullen we de kolommen Id, User, Password en Level hebben.

```csharp
namespace LIB1000
{
    public class UsersModel
    {
        // de key notatie is om de Id uniek te maken.
        // in dit geval zijn de kolommen nu niet nullable
        // indien gewenst moet notatie ? worden bijgezet
        // voorbeeld public string? User { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
 
    }
}
```

### 2.2 In de folder DbContext maken we een DbContext bij aan

In dit geval is dit UserDbContext

```csharp
    public class UsersDbContext : DbContext
    {
        // hier wordt het model UsersModel gebruikt om de tabel 'Users' te maken.

        public DbSet<UsersModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuiler)
        {
            // dit is het path van de database en ook de naam
            string databasePath = @"C:\FH\Users.db";

            // Indien dat deze folder niet bestaat wordt deze aangemaakt

            string directory = Path.GetDirectoryName(databasePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            optionsBuiler.UseSqlite("Data Source=" + databasePath);
        }
    }
```

### 2.3 In de folder Services maken we een service bij 

In dit geval is dit UserService.
Daar maken we een methode om een lijst van users te bewaren in de database.


```csharp
public bool CreateListOfUsersInDatabase(List<UsersModel> lUsers)
        {
            bool result = true;
            // hier maken we gebruik van de dbContext die we in 2.2 hebben gemaakt
            using (var context = new UsersDBContext())
            {
                context.Database.EnsureCreated();

                // hier worden alle users toegevoegd 
               
                foreach (var user in lUsers)
                {
                    UsersModel us = new UsersModel();
                    {
                        us.User = user.User;
                        us.Password = user.Password;
                        us.Level = user.Level;
                    }
                }
                context.SaveChanges();
            }

            // hier wordt de lijst vanuit de datase opgevraagd en 
            // in een locale lijst gezet om te displayen

            ListUsers = GetListOfUsersFromDatabase();

            return true;
        }
```

ListUsers moeten we aanmaken bij Properties van de UserService

        [ObservableProperty]
        private List<object> _listUsers = new List<object>();


### 2.4 In de folder Lists maken we een list bij 

In dit geval is dit UserList.
Hier maken we een lijst om standaard in de database te zetten.

```csharp
    public static class UsersList
    {
        public static List<UsersModel> GetUsers()
        {
            List<UsersModel> lUsers = new List<UsersModel>();
            lUsers.Add(new UsersModel
            {
                Id = 1,
                User = "Operator",
                Password = "Operator",
                Level = 1,

            });
            lUsers.Add(new UsersModel
            {
                Id = 2,
                User = "Steller",
                Password = "Steller",
                Level = 2,

            });
            lUsers.Add(new UsersModel
            {
                Id = 3,
                User = "Technieker",
                Password = "Technieker",
                Level = 3,

            });

            return lUsers;
        }
    }
```


### 2.5 In de MainView.xaml.cs gaan we de methode UserService.CreateListOfUsersInDatabase(AddUsersToList()); toevoegen


```csharp
UserService.CreateListOfUsersInDatabase(AddUsersToList());
```

Bij de methodes voegen we de lijst toe

```csharp

        public List<UsersModel> AddUsersToList()
        {
            // Add your lists of users here from the different services
            List<UsersModel> lUsers = new List<UsersModel>();
            lUsers.AddRange(UsersList.GetUsers());
            
            return lUsers;
        }
```


### 2.6 In de xaml gaan we de lijst koppelen aan een datagrid

Bij de datagrid moet de ItemsSource gebind worden met de lijst die in 2.3 aangemaakt hebben.
Belangrijk is dat UpdateSourceTrigger op PropertyChanged staat.

        <ui:DataGrid ItemsSource="{Binding UserService.ListUsers,Mode=OneWay,UpdateSourceTrigger=PropertyChanged}"
          Height="400" 
          IsManipulationEnabled="True">
        </ui:DataGrid>



## 3. Toevoegen van een user aan de database


## 4. Verbinden met een SQL Server op een andere computer

### 4.1.1 SQL Server Configuration Manager

1. Open de SQL Server Configuration Manager op de computer waar SSMS is geïnstaleerd.
2. Ga naar SQL Server Network configuration > Protocols voor jou SQL Server instantie (Dit is meestal SQLEXPRESS).
3. Zorg ervoor dat TCP/IP enabled is.

### 4.1.2 Poortinstellingen controleren

1. klik met de rechtermuis op TCP/IP en selecteer properties.
2. klik op Ip Addresses en scroll naar beneden. Hier onderaan zie je IPAll staan, bij TCP Port vul je 
1433 in(Dit is het standaard poortnummer).


### 4.3 SQL Server Browser-service inschakelen

1. Druk Windows + R in voor het uitvoer venster
2. Typ services.msc en enter
3. Zoek in het services scherm naar SQL Server Browser. Rechtermuisklik hierop en ga naar eigenschappen.
Verander hier het Opstarttype naar Automatisch.
4. Open Command Prompt als administrator.
5. Typ NET START "SQL Server Browser" en enter. 

Als dit lukt krijg je deze meldingen =>
De SQL Server Browser-service wordt gestart.
De SQL Server Browser-service is gestart.

### 4.4 Firewall instellingen aanpassen

1. Ga naarWindows Defender Firewall > Geavanceerde instellingen > Regels voor binnenkomende verbindingen > Nieuwe regel...
2. Klik op poort en ga naar volgende > Selecteer TCP en selecteer Specifieke lokale poorten en vul in 1433(standaard poortnummer) en klik op volgende.
3. Selecteer "De verbinding toestaan" en klik op volgende, laat "domein, prive, openbaar" aanstaan en klik volgende.
4. Geef de poort een naam en voltooi.

### 4.5 Maak op de SQL Server een nieuwe login aan

1. Op de computer waar ssms is geïnstaleerd ga je naar Logins en klik je met de rechtermuis op Logins en 
  selecteer New Login.
2. Bij login name vul je de naam in die je de login wil geven.
3. Onder de login name selecteer je SQL Server Authentication. zo kan je een wachtwoord ingeven.
4. Ga verder naar Server rolls en selecteer dbcreator en public.

### 4.6 Ga naar visual studio naar de DbContext

Verander de connectiestring naar bv dit => var connectionString = 
@"Server = 10.5.6.120,1433; Database = OrdersDb;User Id = JOLT; Password = JOLT; Encrypt = False";
Zorg er ook voor dat de 2 computers in de zelfde range zitten en dat het ip adress van de computer 
waar SSMS op is geïnstaleerd word ingevuld in de connectiestring.


## 5. Migrations aanmaken voor lokale verbinding op eigen pc met SSMS

### 5.1 DbContext aanpasen

vb:
internal class TestDbContext : DbContext
{

    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderHistoryModel> Details { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuiler)
    {
        // SQL Server connectiestring
        var connectionString = @"Server=MSI\SQLEXPRESS;Database=OrdersDb;Trusted_Connection=True;TrustServerCertificate=True;"

        // Controleer of de connectiestring is ingesteld
        if (!string.IsNullOrEmpty(connectionString))
        {
            optionsBuiler.UseSqlServer(connectionString);
        }
    }

}

### 5.2.1 Migrations aanmaken

Ga naar Tools -> NuGet Package Manager -> Package Manager Console

### 5.2.2

Als eerste typ je dit:

 Add-Migration InitialCreate -context TestDbContext
 
Als dit lukt dan deze command:

Update-Database -context TestDbContext

### 5.2.3

Als de vorige stap succesvol was controleer dan of de database is toegevoegt in SQL managementstudio.
Als SQL al open stond start het programma dan opnieuw op. 
als alles goed is dan staat de juiste DbContext tussen de Databases.

### 5.3

Als stap 5.2.3 gelukt is dan maak je de CRUD operaties aan of pas je deze aan voor een SQL server. bv dit :

public void AddOrderHistory()
{
    using (var orderContext = new TestDbContext())
    {
        // Zoek naar een bestaand orderhistory met de bewerkte Id
        OrderHistoryModel row = orderContext.Details.FirstOrDefault(o => o.Id == OrderHistory.Edit.Id);

        // Als het niet bestaat, maak een nieuwe rij
        if (row == null)
        {
            row = new OrderHistoryModel();

            row.LoginId = BasicService.UsersService.Login.ActualUser.Id;
            row.OrderId = Order.Selected.Id;
            row.Type = OrderHistory.Edit.Type;
            row.Counter = OrderHistory.Edit.Counter;
            row.Info = OrderHistory.Edit.Info;
            row.TimeStamp = DateTime.Now;

            // Voeg het nieuwe orderhistory toe
            orderContext.Details.Add(row);
            orderContext.SaveChanges();

            // Update de lijst met orderhistory
            OrderHistory.GetList();
        }
    }
}

### 5.4

Als stap 5.3 gebeurd is dan voer je deze 2 commands uit in de terminal via deze knoppen
=> Tools -> NuGet Package Manager -> Package Manager Console

De eerste command is =>  Add-Migration SqlServerMigration -context TestDbContext
Tweede command is => Update-Database -context TestDbContext

## 6. Testgegevens Microsoft Azure SQL Server

Servername:    joltcloud.database.windows.net 

SQL login:    LOGINJOLT 

PASS:    JOLT@2BoostJOLT@2Boost


