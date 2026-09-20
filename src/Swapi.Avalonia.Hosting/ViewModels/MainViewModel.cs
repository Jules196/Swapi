using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SwapiBackend;
using SwapiBackend.DTOs;

namespace Swapi.Avalonia.Hosting.ViewModels;

/// <summary>
/// View model of the main window. It loads the Star Wars characters from the
/// backend and exposes them together with a few statistics to the view.
/// </summary>
/// <remarks>
/// In this sample the view model is created by the dependency injection
/// container of the generic host, therefore every dependency is passed into the
/// constructor instead of being created here.
/// </remarks>
public partial class MainViewModel : ViewModelBase, IDisposable
{
    /// <summary>
    /// Names of all loaded characters. Bound to the list box of the view.
    /// </summary>
    [ObservableProperty]
    public partial IReadOnlyList<PersonDTO> People { get; private set; } = [];

    /// <summary>
    /// Indicates that a load operation is currently running. The view uses it
    /// to show the indeterminate progress bar.
    /// </summary>
    [ObservableProperty]
    public partial bool IsBusy { get; private set; }

    /// <summary>
    /// Message shown when loading the characters failed; <see langword="null"/> when everything is fine.
    /// </summary>
    [ObservableProperty]
    public partial string? ErrorMessage { get; private set; }

    /// <summary>
    /// Reference year used for the age calculation, written in the Star Wars
    /// notation (for example <c>24ABY</c> or <c>19BBY</c>). The value is edited by the user.
    /// </summary>
    [ObservableProperty]
    public partial string CurrentYear { get; set; } = "24ABY";

    /// <summary>
    /// Validation message for <see cref="CurrentYear"/>; <see langword="null"/> when the input is valid.
    /// </summary>
    [ObservableProperty]
    public partial string? CurrentYearError { get; private set; }

    /// <summary>
    /// Average height of all loaded characters, already formatted for display.
    /// </summary>
    [ObservableProperty]
    public partial string AverageHeight { get; private set; } = "—";

    /// <summary>
    /// Average age of all loaded characters relative to <see cref="CurrentYear"/>.
    /// </summary>
    [ObservableProperty]
    public partial string AverageAge { get; private set; } = "—";

    /// <summary>
    /// Share of male characters, already formatted as a percentage.
    /// </summary>
    [ObservableProperty]
    public partial string MalePercentage { get; private set; } = "—";

    /// <summary>
    /// Share of female characters, already formatted as a percentage.
    /// </summary>
    [ObservableProperty]
    public partial string FemalePercentage { get; private set; } = "—";

    /// <summary>
    /// Backend service that provides the Star Wars data. It is injected by the host container.
    /// </summary>
    private readonly ISwapiPersons swapiPersons;

    /// <summary>
    /// Logger provided by the host, used to trace failed load operations.
    /// </summary>
    private readonly ILogger<MainViewModel> logger;

    /// <summary>
    /// Raw character details of the last successful load. They are kept so that
    /// the statistics can be recalculated without another network request.
    /// </summary>
    private IReadOnlyList<PersonDetailDTO> personDetails = [];

    /// <summary>
    /// Creates the view model with the services resolved by the host container.
    /// </summary>
    /// <param name="swapiPersons">Backend service used to read the characters.</param>
    /// <param name="logger">Logger created by the logging infrastructure of the host.</param>
    public MainViewModel(ISwapiPersons swapiPersons, ILogger<MainViewModel> logger)
    {
        this.swapiPersons = swapiPersons;
        this.logger = logger;
    }

    /// <summary>
    /// Loads all characters asynchronously and refreshes the statistics.
    /// </summary>
    /// <param name="cancellationToken">
    /// Token supplied by the generated <c>LoadPeopleCommand</c>. Cancelling the
    /// command (for example on shutdown) cancels the pending request.
    /// </param>
    /// <remarks>
    /// The <see cref="RelayCommandAttribute"/> generates the public
    /// <c>LoadPeopleCommand</c> property that the view binds to.
    /// </remarks>
    [RelayCommand]
    private async Task LoadPeopleAsync(CancellationToken cancellationToken)
    {
        // Show the progress bar and clear the result of a previous attempt.
        IsBusy = true;
        ErrorMessage = null;

        try
        {
            List<PersonDetailDTO>? details =
                await swapiPersons.GetAllPersonDetailsAsync(cancellationToken);

            // The backend returns null instead of throwing when the request fails.
            if (details is null)
            {
                logger.LogWarning("The Star Wars characters could not be loaded.");
                ErrorMessage = "The Star Wars characters could not be loaded.";
                return;
            }

            // Project the details onto the light-weight DTO used by the list and
            // keep the details for the statistics.
            People = details.Select(person => new PersonDTO(person.Name)).ToList();
            personDetails = details;
            UpdateStatistics();
        }
        catch (OperationCanceledException)
        {
            // Cancellation is expected during shutdown and is not an error.
        }
        catch (Exception exception)
        {
            // Never let an exception escape into the UI thread; log it and show a message instead.
            logger.LogError(exception, "Loading the Star Wars characters failed.");
            ErrorMessage = "The Star Wars characters could not be loaded.";
        }
        finally
        {
            // Hide the progress bar regardless of the outcome.
            IsBusy = false;
        }
    }

    /// <summary>
    /// Generated hook that runs whenever <see cref="CurrentYear"/> changes, so
    /// the statistics always match the year currently entered by the user.
    /// </summary>
    /// <param name="value">The new year value (not used, the property is read again).</param>
    partial void OnCurrentYearChanged(string value) => UpdateStatistics();

    /// <summary>
    /// Recalculates all statistics from the cached character details and formats
    /// them for display.
    /// </summary>
    private void UpdateStatistics()
    {
        // An unparsable year only invalidates the age; the remaining values stay untouched.
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

    /// <summary>
    /// Cancels a pending load operation and disposes the backend service.
    /// </summary>
    /// <remarks>
    /// The view model is registered as a singleton, so it is disposed together
    /// with the application when the desktop lifetime exits.
    /// </remarks>
    public void Dispose()
    {
        LoadPeopleCommand.Cancel();
        swapiPersons.Dispose();
        GC.SuppressFinalize(this);
    }
}
