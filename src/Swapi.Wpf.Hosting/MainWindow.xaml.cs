using Swapi.Wpf.Hosting.ViewModels;
using System.Windows;

namespace Swapi.Wpf.Hosting;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
