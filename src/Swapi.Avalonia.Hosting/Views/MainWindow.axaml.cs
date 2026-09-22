using System;
using Avalonia.Controls;
using Swapi.Avalonia.Hosting.Theming;
using Swapi.Avalonia.Hosting.ViewModels;

namespace Swapi.Avalonia.Hosting.Views;

/// <summary>
/// Code-behind of the main window. The window receives its view model through
/// constructor injection instead of creating it itself.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Parameterless constructor required by the XAML runtime loader and the
    /// previewer; it leaves the data context empty.
    /// </summary>
    public MainWindow()
    {
        // Loads the XAML defined in MainWindow.axaml.
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
    /// <param name="sender">The theme combo box.</param>
    /// <param name="e">Selection data provided by Avalonia.</param>
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
    /// <param name="sender">The style combo box.</param>
    /// <param name="e">Selection data provided by Avalonia.</param>
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
    /// <param name="sender">The metrics combo box.</param>
    /// <param name="e">Selection data provided by Avalonia.</param>
    private void OnMetricsSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (MetricsSelector.SelectedItem is AppMetrics metrics)
        {
            ThemeManager.Apply(metrics);
        }
    }

    /// <summary>
    /// Creates the window and binds it to the injected view model.
    /// </summary>
    /// <param name="viewModel">View model resolved by the host container.</param>
    public MainWindow(MainViewModel viewModel)
        : this()
    {
        // All bindings in the XAML resolve against this view model.
        DataContext = viewModel;
    }
}
