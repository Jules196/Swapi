# Swapi

Parallele Desktop-Anwendungen, die denselben modernen .NET-Projektstandard
verwenden:

- **Swapi.Wpf** – native Windows-Anwendung mit WPF
- **Swapi.Avalonia** – plattformübergreifende Anwendung mit Avalonia
- **Swapi.Wpf.Hosting** – dieselbe WPF-Anwendung, aber mit `HostApplicationBuilder`
- **Swapi.Avalonia.Hosting** – dieselbe Avalonia-Anwendung, aber mit `HostApplicationBuilder`
- **WPF_old** – das ursprüngliche WPF-Beispiel inklusive Backend und Tests

Die Projekte basieren auf .NET 10, nutzen MVVM mit
`CommunityToolkit.Mvvm`, Nullable Reference Types und zentral verwaltete
Paketversionen.

## Theming (Light/Dark)

Alle vier Beispiele in `src/` lassen sich zur Laufzeit zwischen hellem und
dunklem Design umschalten. Die Auswahl steht als Kombinationsfeld oben rechts
im Fenster.

- **Avalonia**: Die Anwendung nutzt die `ThemeVariant`-Unterstützung des
  Fluent-Themes. `Theming/ThemeManager.cs` setzt
  `Application.Current.RequestedThemeVariant` auf `Default` (System), `Light`
  oder `Dark`. Eigene Farben liegen in `App.axaml` in
  `ResourceDictionary.ThemeDictionaries` und werden per `DynamicResource`
  verwendet.
- **WPF**: WPF bringt kein eigenes Theming mit. Die Farben sind deshalb in
  `Themes/Light.xaml` und `Themes/Dark.xaml` als Pinsel mit identischen
  Schlüsseln definiert. `Theming/ThemeManager.cs` tauscht das zusammengeführte
  Wörterbuch der Anwendung aus; da die Views die Pinsel mit `DynamicResource`
  referenzieren, wirkt der Wechsel sofort. Implizite Styles in `App.xaml`
  sorgen dafür, dass auch die Standardsteuerelemente dem Theme folgen.

## Voraussetzungen

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Windows zum Ausführen der WPF-Anwendung
- Windows, macOS oder Linux zum Ausführen der Avalonia-Anwendung

## Projektstruktur

```text
Swapi.slnx
Directory.Build.props
Directory.Packages.props
src/
├── Swapi.Wpf/
├── Swapi.Wpf.Hosting/
├── Swapi.Avalonia/
└── Swapi.Avalonia.Hosting/
WPF_old/
SwapiBackend/
SwapiBackendTests/
SwapiFrontEndWPF.Test/
```

## Entwickeln

```bash
dotnet restore Swapi.slnx
dotnet build Swapi.slnx
```

Avalonia starten:

```bash
dotnet run --project src/Swapi.Avalonia/Swapi.Avalonia.csproj
```

WPF unter Windows starten:

```powershell
dotnet run --project src/Swapi.Wpf/Swapi.Wpf.csproj
```

Das ursprüngliche WPF-Beispiel unter Windows starten:

```powershell
dotnet run --project WPF_old/WPF_old.csproj
```

## Beispiele mit dem ApplicationBuilder

`Swapi.Wpf.Hosting` und `Swapi.Avalonia.Hosting` zeigen dieselbe Oberfläche,
bauen die Anwendung aber mit dem `HostApplicationBuilder`
(`Host.CreateApplicationBuilder`) aus `Microsoft.Extensions.Hosting` auf:

- Der Einstiegspunkt liegt in `Program.cs`; die UI wird erst nach dem Aufbau des
  Hosts gestartet.
- `ISwapiPersons`, das `MainViewModel`, das Hauptfenster und die
  `Application`-Klasse werden über Dependency Injection registriert und
  aufgelöst.
- Logging (`ILogger<T>`) und Konfiguration stehen zur Verfügung; die Quelle der
  Star-Wars-Daten wird über den Abschnitt `Swapi:SourceUri` aus
  `appsettings.json` gebunden (leerer Wert = Standard-Endpunkt).

Avalonia-Beispiel starten:

```bash
dotnet run --project src/Swapi.Avalonia.Hosting/Swapi.Avalonia.Hosting.csproj
```

WPF-Beispiel unter Windows starten:

```powershell
dotnet run --project src/Swapi.Wpf.Hosting/Swapi.Wpf.Hosting.csproj
```