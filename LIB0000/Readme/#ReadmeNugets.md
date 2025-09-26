# Readme Nugets

## 1. Gebruiken van de Nuget 

Rechtsklik op het project en kies voor Manage Nuget Packages. 
Kies bovenaan rechts voor Jolt Package Source.
Zie voor meer info hierover [#ReadmeJoltInstellingen](LIB1000.HMI/Readme/#ReadmeJoltInstellingen.md)

Kies voor de nieuwste versie van de Nuget en klik op Install.

Nu kan deze Nuget gebruikt worden in dit project.

## 2. Aanpassen van de Nuget

Indien er aanpassingen nodig zijn aan de Nuget, halen we de repo af van Github.
We maken een branch aan voor de aanpassingen en maken de aanpassingen.

De solution zal meestal 2 projecten tonen.
Een project met de code van de Nuget en een project om te visualiseren.

Het is belangrijk om te kijken hoe het HMI project is gekoppeld aan de Nuget.
Om aan te passen en te testen, moet deze gekoppeld zijn met een project reference
én is de Nuget NIET aanwezig in het HMI project.

## 3. Nieuwe Nuget maken

- Ga naar de Properties van het class library project (voorbeeld LIB1000).
- Generate Nuget package on build aanvinken.
- Version nummer aanpassen in de code bij (<Project Sdk="Microsoft.NET.Sdk">)(dubbel klikken op het project)
- Build van het project
- De Nuget zal in de bin\X64\Debug folder van het project staan.
- Maak hiervan een zip bestand
- Kopieer deze naar de juiste locatie op de server.
```D:\OneDrive - jolt.be.co-telenet.be\ServerShared\TECH\Software\Microsoft Visual Studio\NuGetPackages```

- De naming rule voor de nuget is : LIB1000 v x.y.z.nupkg

- Push de aanpassingen naar de Github repo.
- Maak een nieuwe release aan op Github
- Voeg de nieuwe versie van de Nuget toe aan de release (de zip file)
