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

        // Fill the theme selector with the available themes and preselect the
        // theme that is currently applied.
        ThemeSelector.ItemsSource = Enum.GetValues<AppTheme>();
        ThemeSelector.SelectedItem = ThemeManager.CurrentTheme;
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
}
