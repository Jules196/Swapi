using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SwapiBackend;
using SwapiBackend.DTOs;

namespace Swapi.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    public partial IReadOnlyList<PersonDTO> People { get; private set; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; private set; }

    [ObservableProperty]
    public partial string? ErrorMessage { get; private set; }

    private readonly ISwapiPersons swapiPersons;

    public MainViewModel(ISwapiPersons swapiPersons)
    {
        this.swapiPersons = swapiPersons;
    }

    [RelayCommand]
    private async Task LoadPeopleAsync(CancellationToken cancellationToken)
    {
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            IReadOnlyList<PersonDTO>? people =
                await swapiPersons.GetAllPersonNamesAsync(cancellationToken);

            if (people is null)
            {
                ErrorMessage = "The Star Wars characters could not be loaded.";
                return;
            }

            People = people;
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception)
        {
            ErrorMessage = "The Star Wars characters could not be loaded.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
