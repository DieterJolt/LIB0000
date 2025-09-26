# Readme Classes

## 1.1 Naming van de classes

De naam van een class begint altijd met een hoofdletter en is in CamelCase. 
Dit betekent dat de eerste letter van elk woord in de naam van de class een hoofdletter is. 
Bijvoorbeeld: `class MijnEersteClass`.

Afhankelijk van in welke folder de class moet heeft deze een ander achtervoegsel.

Bijvoorbeeld : Folder Lists is de naam MessagesList.cs


## 1.2 Structuur van de class

Een class ziet er zo uit:

```csharp	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB1000
{
    public class ExampleClass
    {

        #region Commands
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
        #endregion


    }
}
```

### 1.2.1 Usings

Probeer de usings zo kort mogelijk te houden.
Gebruik geen usings die niet nodig zijn.
Deze kunnen worden verwijderd door de sneltoets `Ctrl + .` te gebruiken.

### 1.2.2 Regions

We gebruiken regions om de class op te delen in verschillende delen.
Dit maakt het makkelijker om de class te lezen en te onderhouden.
Deze kunnen gemakkelijk worden toegevoegd door de JOLT snippet 'jr + tab' te gebruiken.
Hier kan de snippet ingesteld worden [#ReadmeJoltInstellingen](LIB1000.HMI/Readme/#ReadmeJoltInstellingen.md)

### 1.2.3 Naming van de code

De naam van de code binnen de class begint altijd met een kleine letter en is in CamelCase.
Dit betekent dat de eerste letter van elk woord in de naam van de code een hoofdletter is.
Bijvoorbeeld: `private string mijnEersteString`.

Public code begint altijd met een hoofdletter en is ook in CamelCase.

Indien we met [ObservableProperty] werken, dan beginnen de properties met een underscore.]

voorbeeld:
```csharp
[ObservableProperty]
private string _mijnEersteString;
```

In dit voorbeeld wordt de observable property MijnEersteString automatisch aangemaakt.

### 1.2.4 Volgorde van de code

We proberen zoveel mogelijk de methodes, properties, fields binnen de regions
alfabetisch te ordenen. Dit maakt het makkelijker om de code te lezen en te onderhouden.