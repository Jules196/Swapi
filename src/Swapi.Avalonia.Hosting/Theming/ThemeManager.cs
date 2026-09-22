using Avalonia;
using Avalonia.Styling;

namespace Swapi.Avalonia.Hosting.Theming;

/// <summary>Theme options offered in the user interface.</summary>
public enum AppTheme
{
    /// <summary>Follows the theme variant of the operating system.</summary>
    System,

    /// <summary>Always light colours.</summary>
    Light,

    /// <summary>Always dark colours.</summary>
    Dark,
}

/// <summary>
/// Switches the theme variant of the application at runtime. Avalonia resolves
/// the Fluent theme resources and the theme dictionaries in <c>App.axaml</c>
/// for the requested variant, so no resources have to be swapped manually.
/// </summary>
public static class ThemeManager
{
    /// <summary>The theme that is currently selected.</summary>
    public static AppTheme CurrentTheme { get; private set; } = AppTheme.System;

    /// <summary>
    /// Applies the requested theme variant to the running application.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    public static void Apply(AppTheme theme)
    {
        Application? application = Application.Current;
        if (application is null)
        {
            return;
        }

        application.RequestedThemeVariant = theme switch
        {
            AppTheme.Light => ThemeVariant.Light,
            AppTheme.Dark => ThemeVariant.Dark,
            _ => ThemeVariant.Default,
        };

        CurrentTheme = theme;
    }
}
