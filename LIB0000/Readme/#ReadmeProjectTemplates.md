# Readme Project Templates


## 1. Project Template gebruiken

Bij het opstarten van Visual Studio kies voor de optie "Create a new project". 

Kies voor de LIB1000 v x.y.z template.

Deze templates worden bewaard op volgende locatie :

```D:\OneDrive - jolt.be.co-telenet.be\ServerShared\TECH\Software\Microsoft Visual Studio\ProjectTemplates```

Geef het project de naam van het project zoals bijvoorbeeld COR0001

Afhankelijk van de hardware en functie van het project,
kan het nodig zijn om andere nuget packages te installeren.

[#ReadmeNugets](https://github.com/DieterJolt/LIB1000/blob/master/LIB1000.HMI/Readme/%23ReadmeNugets.md)

## 2. Project Template aanpassen

### 2.1 Voorbereidingen voor aanpassen van het project template

Om het project template aan te passen, kan het LIB1000 worden geopend in Visual Studio.

Om aanpassen aan de nuget LIB1000 te doen, 
kan je in het project LIB1000.HMI de nuget package LIB1000 best verwijderen.

In plaats van de referentie naar de nuget package, 
kan je de referentie naar de project LIB1000 toevoegen in het LIB1000.HMI.

### 2.2 Exporteren van het project template

Verwijder bij LIB1000.HMI de referentie naar het project LIB1000.
Voeg nu bij LIB1000.HMI via manage Nuget packages de laatste nuget versie van LIB1000 toe.

Hier is de informatie te vinden om de nuget te genereren : 

[#ReadmeNugets](https://github.com/DieterJolt/LIB1000/blob/master/LIB1000.HMI/Readme/%23ReadmeNugets.md)

Als het project heeft gewerkt met de nieuwe nuget package, dan kan de project
template worden geëxporteerd.

Duid het project aan en ga naar "Project" -> "Export Template".
Kies voor "Project template" en volg de stappen.

Templatename : LIB1000 v x.y.z  
Template description : JOLT Template  
Icon : JOLT.ico te vinden op volgende locatie :  
```D:\OneDrive - jolt.be.co-telenet.be\ServerShared\TECH\Software\Microsoft Visual Studio\Icons```
Preview image : JOLT.png te vinden op volgende locatie :   
```D:\OneDrive - jolt.be.co-telenet.be\ServerShared\TECH\Software\Microsoft Visual Studio\Icons```
Output location (te kopiëren naar onderstaande folder):   
```D:\OneDrive - jolt.be.co-telenet.be\ServerShared\TECH\Software\Microsoft Visual Studio\ProjectTemplates```
