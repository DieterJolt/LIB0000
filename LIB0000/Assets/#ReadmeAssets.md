# Readme Assets Folder

Deze folder bevat alle assets. Dit zijn afbeeldingen, video's, etc. die in het project gebruikt worden.

In de properties van de assets zijn deze als volgt ingesteld:

- `Copy to Output Directory`: `Copy Always`
- `Build Action`: `Resource`

We proberen er ook voor te zorgen dat deze files in de assets folder niet te groot zijn.
Dit is te belastend voor de push naar github.
Github heeft een limiet van 100MB per file. Als een file groter is dan 100MB, dan kan deze niet gepushed worden naar github.
We houden een regel aan dat een enkele file niet groter dan 10 MB mag zijn.