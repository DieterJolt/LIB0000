# Readme Models Folder

In deze folder staan de modellen die gebruikt worden in de readme van de repository. 

De naming rule is als volgt: NaamModel + Model.cs

Voorbeeld van een model :

```csharp
    public class MessageJoinModel
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Nr { get; set; }
        public string MessageText { get; set; }
        public string Help { get; set; }
        public MessageType Type { get; set; }
        public DateTime TimeStamp { get; set; }

    }
```

Indien er een model wordt gebruikt voor het genereren van een database tabel, 
dan wordt dit model ook in de Models folder geplaatst.

Deze modellen worden dan gebruikt in de DbContext klasse om de database te genereren.

Voorbeeld van een model dat gebruikt wordt voor het genereren van een database tabel.
Hier wordt de [Key] en [DatabaseGenerated(DatabaseGeneratedOption.Identity)] attributen gebruikt om de Id kolom te genereren.

```csharp
    public class MessageActiveModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Group { get; set; }
        public string Nr { get; set; }  

        public DateTime LastUpdate { get; set; }
    }
```


