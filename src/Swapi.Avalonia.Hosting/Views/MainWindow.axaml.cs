using Avalonia.Controls;
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
