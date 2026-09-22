using System;
using System.Linq;
using System.Windows;

namespace Swapi.Wpf.Hosting.Theming;

/// <summary>Available colour themes of the application.</summary>
public enum AppTheme
{
    /// <summary>Light colours.</summary>
    Light,

    /// <summary>Dark colours.</summary>
    Dark,
}

/// <summary>
/// Swaps the theme resource dictionary of the application at runtime. WPF has
/// no built-in theming, so the brushes are defined per theme in
/// <c>Themes/Light.xaml</c> and <c>Themes/Dark.xaml</c> and the views reference
/// them with <c>DynamicResource</c>.
/// </summary>
public static class ThemeManager
{
    /// <summary>Marks the dictionaries that belong to a theme.</summary>
    private const string ThemeFolder = "/Themes/";

    /// <summary>The theme that is currently applied.</summary>
    public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

    /// <summary>
    /// Replaces the merged theme dictionary of the application. Because all
    /// theme colours are referenced with <c>DynamicResource</c>, the open
    /// windows update immediately.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    public static void Apply(AppTheme theme)
    {
        Application? application = Application.Current;
        if (application is null)
        {
            return;
        }

        ResourceDictionary themeDictionary = new()
        {
            Source = new Uri($"pack://application:,,,/Themes/{theme}.xaml", UriKind.Absolute),
        };

        foreach (ResourceDictionary existing in application.Resources.MergedDictionaries
                     .Where(IsThemeDictionary)
                     .ToList())
        {
            application.Resources.MergedDictionaries.Remove(existing);
        }

        application.Resources.MergedDictionaries.Insert(0, themeDictionary);
        CurrentTheme = theme;
    }

    /// <summary>
    /// Determines whether a merged dictionary was loaded from the themes folder.
    /// </summary>
    /// <param name="dictionary">The dictionary to check.</param>
    /// <returns><see langword="true"/> for theme dictionaries.</returns>
    private static bool IsThemeDictionary(ResourceDictionary dictionary) =>
        dictionary.Source is not null
        && dictionary.Source.OriginalString.Contains(ThemeFolder, StringComparison.OrdinalIgnoreCase);
}
