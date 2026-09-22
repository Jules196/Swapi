# Swapi

Parallel desktop applications that use the same modern .NET project standard:

- **Swapi.Wpf** – native Windows application using WPF
- **Swapi.Avalonia** – cross-platform application using Avalonia
- **Swapi.Wpf.Hosting** – the same WPF application, but using `HostApplicationBuilder`
- **Swapi.Avalonia.Hosting** – the same Avalonia application, but using `HostApplicationBuilder`
- **WPF_old** – the original WPF sample including backend and tests

The projects target .NET 10, use MVVM with `CommunityToolkit.Mvvm`, nullable
reference types and centrally managed package versions.

## Appearance (theme, style, metrics)

All four samples in `src/` can be switched at runtime along three independent
axes. The selection is available as combo boxes in the top right of the window.

- **Theme** (`Themes/`, Avalonia: `ThemeDictionaries` in `App.axaml`) – the base
  colors (`Light`/`Dark`, plus `System` in Avalonia).
- **Style** (`Styles/Classic`, `Ocean`, `Sunset`) – the accent colors
  (`AccentBrush`, `AccentForegroundBrush`, `AccentSubtleBrush`) used for
  headings, buttons and the progress bar.
- **Metrics** (`Metrics/Compact`, `Comfortable`, `Spacious`) – font sizes,
  margins, paddings and corner radii (for example `TitleFontSize`,
  `CardPadding`, `SectionMargin`, `ControlCornerRadius`).

Every variant of an axis defines the same keys, so they can be combined freely.
`Theming/ThemeManager.cs` provides one `Apply` overload each for `AppTheme`,
`AppStyle` and `AppMetrics`.

- **Avalonia**: the colors use the `ThemeVariant` support of the Fluent theme;
  `ThemeManager.Apply(AppTheme)` sets
  `Application.Current.RequestedThemeVariant` to `Default` (system), `Light` or
  `Dark`. Style and metric dictionaries are swapped as `ResourceInclude` entries
  in `Application.Resources.MergedDictionaries`.
- **WPF**: WPF does not ship its own theming. All values are therefore defined
  per variant as a resource dictionary; the `ThemeManager` replaces the
  corresponding merged dictionary of the application. Because the views
  reference the values with `DynamicResource`, the change takes effect
  immediately. Implicit styles in `App.xaml` make sure the default controls
  follow along as well.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Windows to run the WPF application
- Windows, macOS or Linux to run the Avalonia application

## Project structure

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

## Development

```bash
dotnet restore Swapi.slnx
dotnet build Swapi.slnx
```

Run Avalonia:

```bash
dotnet run --project src/Swapi.Avalonia/Swapi.Avalonia.csproj
```

Run WPF on Windows:

```powershell
dotnet run --project src/Swapi.Wpf/Swapi.Wpf.csproj
```

Run the original WPF sample on Windows:

```powershell
dotnet run --project WPF_old/WPF_old.csproj
```

## Samples using the ApplicationBuilder

`Swapi.Wpf.Hosting` and `Swapi.Avalonia.Hosting` show the same user interface,
but build the application with the `HostApplicationBuilder`
(`Host.CreateApplicationBuilder`) from `Microsoft.Extensions.Hosting`:

- The entry point is in `Program.cs`; the UI only starts after the host has been
  built.
- `ISwapiPersons`, the `MainViewModel`, the main window and the `Application`
  class are registered and resolved through dependency injection.
- Logging (`ILogger<T>`) and configuration are available; the source of the
  Star Wars data is bound from the `Swapi:SourceUri` section of
  `appsettings.json` (an empty value means the default endpoint).

Run the Avalonia sample:

```bash
dotnet run --project src/Swapi.Avalonia.Hosting/Swapi.Avalonia.Hosting.csproj
```

Run the WPF sample on Windows:

```powershell
dotnet run --project src/Swapi.Wpf.Hosting/Swapi.Wpf.Hosting.csproj
```
