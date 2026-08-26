# Swapi

Zwei parallele Desktop-Anwendungen, die denselben modernen .NET-Projektstandard
verwenden:

- **Swapi.Wpf** – native Windows-Anwendung mit WPF
- **Swapi.Avalonia** – plattformübergreifende Anwendung mit Avalonia

Beide Projekte basieren auf .NET 10, nutzen MVVM mit
`CommunityToolkit.Mvvm`, Nullable Reference Types und zentral verwaltete
Paketversionen.

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
└── Swapi.Avalonia/
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