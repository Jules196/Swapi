using Swapi.Wpf.Hosting.ViewModels;
using System.Windows;

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
    }
}
