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

/// <summary>Available accent styles of the application.</summary>
public enum AppStyle
{
    /// <summary>Blue accents.</summary>
    Classic,

    /// <summary>Teal accents.</summary>
    Ocean,

    /// <summary>Orange accents.</summary>
    Sunset,
}

/// <summary>Available sizes and spacings of the application.</summary>
public enum AppMetrics
{
    /// <summary>Small fonts and tight spacings.</summary>
    Compact,

    /// <summary>Default fonts and spacings.</summary>
    Comfortable,

    /// <summary>Large fonts and generous spacings.</summary>
    Spacious,
}

/// <summary>
/// Swaps the appearance resource dictionaries of the application at runtime.
/// WPF has no built-in theming, so the values are defined per variant in
/// <c>Themes/</c> (colours), <c>Styles/</c> (accents) and <c>Metrics/</c>
/// (sizes and spacings); the views reference them with <c>DynamicResource</c>.
/// </summary>
public static class ThemeManager
{
    /// <summary>Marks the dictionaries that belong to a theme.</summary>
    private const string ThemeFolder = "/Themes/";

    /// <summary>Marks the dictionaries that belong to a style.</summary>
    private const string StyleFolder = "/Styles/";

    /// <summary>Marks the dictionaries that belong to a metric set.</summary>
    private const string MetricsFolder = "/Metrics/";

    /// <summary>The theme that is currently applied.</summary>
    public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

    /// <summary>The style that is currently applied.</summary>
    public static AppStyle CurrentStyle { get; private set; } = AppStyle.Classic;

    /// <summary>The metric set that is currently applied.</summary>
    public static AppMetrics CurrentMetrics { get; private set; } = AppMetrics.Comfortable;

    /// <summary>
    /// Replaces the merged theme dictionary of the application. Because all
    /// theme colours are referenced with <c>DynamicResource</c>, the open
    /// windows update immediately.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    public static void Apply(AppTheme theme)
    {
        Swap(ThemeFolder, theme.ToString());
        CurrentTheme = theme;
    }

    /// <summary>
    /// Replaces the merged style dictionary, which holds the accent colours.
    /// </summary>
    /// <param name="style">The style to apply.</param>
    public static void Apply(AppStyle style)
    {
        Swap(StyleFolder, style.ToString());
        CurrentStyle = style;
    }

    /// <summary>
    /// Replaces the merged metrics dictionary, which holds the font sizes,
    /// paddings, margins and corner radii.
    /// </summary>
    /// <param name="metrics">The metric set to apply.</param>
    public static void Apply(AppMetrics metrics)
    {
        Swap(MetricsFolder, metrics.ToString());
        CurrentMetrics = metrics;
    }

    /// <summary>
    /// Removes the dictionary that is currently merged from <paramref name="folder"/>
    /// and merges the requested one instead.
    /// </summary>
    /// <param name="folder">Folder the dictionaries of this category live in.</param>
    /// <param name="name">File name of the dictionary without extension.</param>
    private static void Swap(string folder, string name)
    {
        Application? application = Application.Current;
        if (application is null)
        {
            return;
        }

        ResourceDictionary dictionary = new()
        {
            Source = new Uri($"pack://application:,,,{folder}{name}.xaml", UriKind.Absolute),
        };

        foreach (ResourceDictionary existing in application.Resources.MergedDictionaries
                     .Where(candidate => IsFrom(candidate, folder))
                     .ToList())
        {
            application.Resources.MergedDictionaries.Remove(existing);
        }

        application.Resources.MergedDictionaries.Insert(0, dictionary);
    }

    /// <summary>
    /// Determines whether a merged dictionary was loaded from the given folder.
    /// </summary>
    /// <param name="dictionary">The dictionary to check.</param>
    /// <param name="folder">The folder to compare against.</param>
    /// <returns><see langword="true"/> for dictionaries of that folder.</returns>
    private static bool IsFrom(ResourceDictionary dictionary, string folder) =>
        dictionary.Source is not null
        && dictionary.Source.OriginalString.Contains(folder, StringComparison.OrdinalIgnoreCase);
}
