using Swapi.Wpf.Theming;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Swapi.Wpf;

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
    /// Applies the theme chosen in the combo box. Because the views use
    /// <c>DynamicResource</c>, the window updates without a restart.
    /// </summary>
    private void OnThemeSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeSelector.SelectedItem is AppTheme theme)
        {
            ThemeManager.Apply(theme);
        }
    }

    /// <summary>
    /// Applies the accent style chosen in the combo box.
    /// </summary>
    private void OnStyleSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (StyleSelector.SelectedItem is AppStyle style)
        {
            ThemeManager.Apply(style);
        }
    }

    /// <summary>
    /// Applies the metrics (font sizes, paddings, margins) chosen in the combo box.
    /// </summary>
    private void OnMetricsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MetricsSelector.SelectedItem is AppMetrics metrics)
        {
            ThemeManager.Apply(metrics);
        }
    }
}
