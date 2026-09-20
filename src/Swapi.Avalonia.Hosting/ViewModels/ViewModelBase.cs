using CommunityToolkit.Mvvm.ComponentModel;

namespace Swapi.Avalonia.Hosting.ViewModels;

/// <summary>
/// Common base class for all view models of this sample. It adds the change
/// notification implementation of <see cref="ObservableObject"/> and gives the
/// XAML compiler a shared type for <c>x:DataType</c> declarations.
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
}
