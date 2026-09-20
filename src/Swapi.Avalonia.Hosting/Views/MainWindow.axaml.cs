using Avalonia.Controls;
using Swapi.Avalonia.Hosting.ViewModels;

namespace Swapi.Avalonia.Hosting.Views;

public partial class MainWindow : Window
{
    // Parameterless constructor for the XAML runtime loader and the designer.
    public MainWindow()
    {
        InitializeComponent();
    }

    public MainWindow(MainViewModel viewModel)
        : this()
    {
        DataContext = viewModel;
    }
}
