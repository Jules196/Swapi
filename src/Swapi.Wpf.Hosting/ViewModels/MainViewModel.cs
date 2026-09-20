using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SwapiBackend;
using SwapiBackend.DTOs;

namespace Swapi.Wpf.Hosting.ViewModels;

public partial class MainViewModel : ObservableObject, IDisposable
{
    [ObservableProperty]
    public partial IReadOnlyList<PersonDTO> People { get; private set; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; private set; }

    [ObservableProperty]
    public partial string? ErrorMessage { get; private set; }

    [ObservableProperty]
    public partial string CurrentYear { get; set; } = "24ABY";

    [ObservableProperty]
    public partial string? CurrentYearError { get; private set; }

    [ObservableProperty]
    public partial string AverageHeight { get; private set; } = "—";

    [ObservableProperty]
    public partial string AverageAge { get; private set; } = "—";

    [ObservableProperty]
    public partial string MalePercentage { get; private set; } = "—";

    [ObservableProperty]
    public partial string FemalePercentage { get; private set; } = "—";

    private readonly ISwapiPersons swapiPersons;
    private readonly ILogger<MainViewModel> logger;
    private IReadOnlyList<PersonDetailDTO> personDetails = [];

    public MainViewModel(ISwapiPersons swapiPersons, ILogger<MainViewModel> logger)
    {
        this.swapiPersons = swapiPersons;
        this.logger = logger;
    }

    [RelayCommand]
    private async Task LoadPeopleAsync(CancellationToken cancellationToken)
    {
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            List<PersonDetailDTO>? details =
                await swapiPersons.GetAllPersonDetailsAsync(cancellationToken);

            if (details is null)
            {
                logger.LogWarning("The Star Wars characters could not be loaded.");
                ErrorMessage = "The Star Wars characters could not be loaded.";
                return;
            }

            People = details.Select(person => new PersonDTO(person.Name)).ToList();
            personDetails = details;
            UpdateStatistics();
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Loading the Star Wars characters failed.");
            ErrorMessage = "The Star Wars characters could not be loaded.";
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnCurrentYearChanged(string value) => UpdateStatistics();

    private void UpdateStatistics()
    {
        if (!PersonStatisticsCalculator.TryCalculate(
                personDetails,
                CurrentYear,
                out PersonStatistics statistics))
        {
            CurrentYearError = "Use a year such as 24ABY or 19BBY.";
            AverageAge = "—";
            return;
        }

        CurrentYearError = null;
        AverageHeight = $"{statistics.AverageHeight:N2} cm";
        AverageAge = $"{statistics.AverageAge:N2} years";
        MalePercentage = $"{statistics.MalePercentage:N2}%";
        FemalePercentage = $"{statistics.FemalePercentage:N2}%";
    }

    public void Dispose()
    {
        LoadPeopleCommand.Cancel();
        swapiPersons.Dispose();
        GC.SuppressFinalize(this);
    }
}
