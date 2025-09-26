# Readme Dictionaries Folder

De folder dictionaries bevat dictories die gebruikt worden in de code. De volgende dictionaries zijn aanwezig:

	- FontDictionary: bevat de dictionaries die gebruikt worden om de fonts te bepalen.

Naming rule is de naam van de dictionary gevolgd door Dictionary.xaml.

## 1. Toevoegen van de dictionaries aan App.xaml.cs

We voegen de dictionaries toe aan App.xaml onder MergedDictionaries.

```csharp
			<ResourceDictionary.MergedDictionaries>
				<ResourceDictionary Source="Dictionaries/FontsDictionary.xaml"/>
			</ResourceDictionary.MergedDictionaries>
```

## 2. Gebruik van de dictionaries in xaml

De dictionaries kunnen gebruikt worden in de code door de volgende code te gebruiken:

```xaml
<Label Text="Hello, Forms!" FontFamily="{StaticResource FontFamilyRegular}" 
FontSize="{StaticResource FontSizeMedium}" TextColor="{StaticResource ColorPrimary}" />
```

