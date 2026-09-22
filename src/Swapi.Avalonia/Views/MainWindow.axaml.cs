using System;
using Avalonia.Controls;
using Swapi.Avalonia.Theming;

namespace Swapi.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
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
    private void OnThemeSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ThemeSelector.SelectedItem is AppTheme theme)
        {
            ThemeManager.Apply(theme);
        }
    }
}
