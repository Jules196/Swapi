using System;
using Avalonia.Controls;
using Swapi.Avalonia.Theming;

namespace Swapi.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Fill the selectors with the available variants and preselect the
        // theme, style and metrics that are currently applied.
        ThemeSelector.ItemsSource = Enum.GetValues<AppTheme>();
        ThemeSelector.SelectedItem = ThemeManager.CurrentTheme;

        StyleSelector.ItemsSource = Enum.GetValues<AppStyle>();
        StyleSelector.SelectedItem = ThemeManager.CurrentStyle;

        MetricsSelector.ItemsSource = Enum.GetValues<AppMetrics>();
        MetricsSelector.SelectedItem = ThemeManager.CurrentMetrics;
    }

    /// <summary>
    /// Applies the theme chosen in the combo box. Avalonia re-evaluates the
    /// theme dictionaries for the new variant, so the window updates at once.
    /// </summary>
    private void OnThemeSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ThemeSelector.SelectedItem is AppTheme theme)
        {
            ThemeManager.Apply(theme);
        }
    }

    /// <summary>
    /// Applies the accent style chosen in the combo box.
    /// </summary>
    private void OnStyleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (StyleSelector.SelectedItem is AppStyle style)
        {
            ThemeManager.Apply(style);
        }
    }

    /// <summary>
    /// Applies the metrics (font sizes, paddings, margins) chosen in the combo box.
    /// </summary>
    private void OnMetricsSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (MetricsSelector.SelectedItem is AppMetrics metrics)
        {
            ThemeManager.Apply(metrics);
        }
    }
}
