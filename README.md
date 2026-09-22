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

## Erscheinungsbild (Theme, Style, Metriken)

Alle vier Beispiele in `src/` lassen sich zur Laufzeit in drei voneinander
unabhängigen Achsen umschalten. Die Auswahl steht als Kombinationsfelder oben
rechts im Fenster.

- **Theme** (`Themes/`, Avalonia: `ThemeDictionaries` in `App.axaml`) – die
  Grundfarben (`Light`/`Dark`, in Avalonia zusätzlich `System`).
- **Style** (`Styles/Classic`, `Ocean`, `Sunset`) – die Akzentfarben
  (`AccentBrush`, `AccentForegroundBrush`, `AccentSubtleBrush`), die für
  Überschriften, Schaltflächen und den Fortschrittsbalken verwendet werden.
- **Metriken** (`Metrics/Compact`, `Comfortable`, `Spacious`) – Schriftgrößen,
  Abstände, Innenabstände und Eckradien (z. B. `TitleFontSize`, `CardPadding`,
  `SectionMargin`, `ControlCornerRadius`).

Jede Variante einer Achse definiert dieselben Schlüssel, sodass sie beliebig
kombiniert werden können. `Theming/ThemeManager.cs` bietet je eine
`Apply`-Überladung für `AppTheme`, `AppStyle` und `AppMetrics`.

- **Avalonia**: Die Farben nutzen die `ThemeVariant`-Unterstützung des
  Fluent-Themes; `ThemeManager.Apply(AppTheme)` setzt
  `Application.Current.RequestedThemeVariant` auf `Default` (System), `Light`
  oder `Dark`. Style- und Metrik-Wörterbücher werden als `ResourceInclude` in
  `Application.Resources.MergedDictionaries` ausgetauscht.
- **WPF**: WPF bringt kein eigenes Theming mit. Alle Werte sind deshalb je
  Variante als Ressourcen-Wörterbuch definiert; der `ThemeManager` ersetzt das
  jeweils zusammengeführte Wörterbuch der Anwendung. Da die Views die Werte mit
  `DynamicResource` referenzieren, wirkt der Wechsel sofort. Implizite Styles in
  `App.xaml` sorgen dafür, dass auch die Standardsteuerelemente folgen.

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