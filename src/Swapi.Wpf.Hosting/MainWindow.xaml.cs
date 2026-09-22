using Swapi.Wpf.Hosting.Theming;
using Swapi.Wpf.Hosting.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Swapi.Wpf.Hosting;

/// <summary>
/// Code-behind of the main window. The window receives its view model through
/// constructor injection instead of creating it itself.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Creates the window and binds it to the injected view model.
    /// </summary>
    /// <param name="viewModel">View model resolved by the host container.</param>
    public MainWindow(MainViewModel viewModel)
    {
        // Loads the XAML defined in MainWindow.xaml.
        InitializeComponent();

        // All bindings in the XAML resolve against this view model.
        DataContext = viewModel;

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
    /// Applies the theme chosen in the combo box. Because the views reference
    /// the colours with <c>DynamicResource</c>, the window updates immediately.
    /// </summary>
    /// <param name="sender">The theme combo box.</param>
    /// <param name="e">Selection data provided by WPF.</param>
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
    /// <param name="sender">The style combo box.</param>
    /// <param name="e">Selection data provided by WPF.</param>
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
    /// <param name="sender">The metrics combo box.</param>
    /// <param name="e">Selection data provided by WPF.</param>
    private void OnMetricsSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (MetricsSelector.SelectedItem is AppMetrics metrics)
        {
            ThemeManager.Apply(metrics);
        }
    }
}
