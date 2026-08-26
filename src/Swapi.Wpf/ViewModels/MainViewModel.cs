using CommunityToolkit.Mvvm.ComponentModel;

namespace Swapi.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to WPF!";
}
