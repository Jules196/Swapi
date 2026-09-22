using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
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
/// Switches the appearance of the application at runtime. The colours follow
/// the theme variant, which Avalonia resolves for the Fluent theme and the
/// theme dictionaries in <c>App.axaml</c>. Accents (<c>Styles/</c>) and sizes
/// (<c>Metrics/</c>) live in separate resource dictionaries that are merged
/// into the application resources and exchanged on demand.
/// </summary>
public static class ThemeManager
{
    /// <summary>Base URI used to resolve the <c>avares</c> resource includes.</summary>
    private static readonly Uri BaseUri = new("avares://Swapi.Avalonia.Hosting/App.axaml");

    /// <summary>Folder the style dictionaries live in.</summary>
    private const string StyleFolder = "Styles";

    /// <summary>Folder the metrics dictionaries live in.</summary>
    private const string MetricsFolder = "Metrics";

    /// <summary>The theme that is currently selected.</summary>
    public static AppTheme CurrentTheme { get; private set; } = AppTheme.System;

    /// <summary>The style that is currently applied.</summary>
    public static AppStyle CurrentStyle { get; private set; } = AppStyle.Classic;

    /// <summary>The metric set that is currently applied.</summary>
    public static AppMetrics CurrentMetrics { get; private set; } = AppMetrics.Comfortable;

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
    /// and merges the requested one instead. Because the views reference the
    /// values with <c>DynamicResource</c>, the windows update immediately.
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

        IList<IResourceProvider> mergedDictionaries = application.Resources.MergedDictionaries;

        foreach (IResourceProvider existing in mergedDictionaries
                     .Where(candidate => IsFrom(candidate, folder))
                     .ToList())
        {
            mergedDictionaries.Remove(existing);
        }

        mergedDictionaries.Add(new ResourceInclude(BaseUri)
        {
            Source = new Uri($"avares://Swapi.Avalonia.Hosting/{folder}/{name}.axaml"),
        });
    }

    /// <summary>
    /// Determines whether a merged dictionary was loaded from the given folder.
    /// </summary>
    /// <param name="resourceProvider">The merged dictionary to check.</param>
    /// <param name="folder">The folder to compare against.</param>
    /// <returns><see langword="true"/> for dictionaries of that folder.</returns>
    private static bool IsFrom(IResourceProvider resourceProvider, string folder) =>
        resourceProvider is ResourceInclude include
        && include.Source is not null
        && include.Source.OriginalString.Contains($"/{folder}/", StringComparison.OrdinalIgnoreCase);
}
