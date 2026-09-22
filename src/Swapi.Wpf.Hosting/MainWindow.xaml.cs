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

        // Fill the theme selector with the available themes and preselect the
        // theme that is currently applied.
        ThemeSelector.ItemsSource = Enum.GetValues<AppTheme>();
        ThemeSelector.SelectedItem = ThemeManager.CurrentTheme;
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
}
