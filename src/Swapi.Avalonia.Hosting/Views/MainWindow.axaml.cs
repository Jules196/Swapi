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

        // Fill the theme selector with the available themes and preselect the
        // theme that is currently applied.
        ThemeSelector.ItemsSource = Enum.GetValues<AppTheme>();
        ThemeSelector.SelectedItem = ThemeManager.CurrentTheme;
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
