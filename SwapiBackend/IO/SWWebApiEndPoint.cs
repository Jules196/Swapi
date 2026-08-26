using SwapiBackend.DTOs;
using SwapiBackend.Mapping;
using SwapiBackend.Models;
using System.Runtime.CompilerServices;

namespace SwapiBackend.IO;

/// <summary>
/// Star wars data repository for the web api 'https://swapi.dev/api/people/'.
/// </summary>
internal class SWWebApiPeople : IStarWarsDataRepository
{
    private const string _baseUrl = "https://swapi.dev/api/people/";
    private static string GetSinglePersonUrl(int id) => $"{_baseUrl}{id}/";

    private HttpClient _client = new();

    private readonly CancellationTokenSource _internalCancellationTokenSource = new();

    public SWWebApiPeople()
    { }

    /// <summary>
    /// Cancels also the internal requests.
    /// </summary>
    public void Dispose()
    {
        _client.CancelPendingRequests();
        _client.Dispose();
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<PersonModel> GetAllPersonModelsAsync([EnumeratorCancellation] CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        AllPersonsModel? currentAllPersonPage = await GetAllPersonFromPage(_baseUrl, token);
        if (currentAllPersonPage is null || currentAllPersonPage.Persons is null) yield break;
        foreach (PersonModel p in currentAllPersonPage.Persons) { yield return p; }

        while (currentAllPersonPage.Next is not null)
        {
            currentAllPersonPage = await GetAllPersonFromPage(currentAllPersonPage.Next, token);
            if (currentAllPersonPage is null || currentAllPersonPage.Persons is null) yield break;
            foreach (PersonModel pm in currentAllPersonPage.Persons)
            {
                yield return pm;
            }
        }
    }

    /// <inheritdoc />
    public async Task<PersonModel?> GetPersonModelAsync(int id, CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        string personUrl = GetSinglePersonUrl(id);
        try
        {
            HttpResponseMessage? response = await _client.GetAsync(personUrl, linkedCts.Token);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync(token);
            return responseBody.ToPersonModel();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("The operation was canceled.");
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<PersonModel?> GetPersonModelAsync(string name, CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            await foreach (PersonModel pm in GetAllPersonModelsAsync(linkedCts.Token))
            {
                if (pm.Name == name) return pm;
            }
            return null;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("The operation was canceled.");
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public IAsyncEnumerable<PersonModel> GetAllPersonModelsAsync() => GetAllPersonModelsAsync(CancellationToken.None);

    /// <inheritdoc />
    public async Task<PersonModel?> GetPersonModelAsync(int id) => await GetPersonModelAsync(id, CancellationToken.None);

    /// <inheritdoc />
    public async Task<PersonModel?> GetPersonModelAsync(string name) => await GetPersonModelAsync(name, CancellationToken.None);

    /// <inheritdoc />
    public Task<Result> UpdatePersonData(PersonUpdateDTO person) => UpdatePersonData(person, CancellationToken.None);

    /// <inheritdoc />
    public Task<Result> UpdatePersonData(PersonUpdateDTO person, CancellationToken token)
    {
        return Task.FromResult(new Result(false, "It is not allowed to update the Star Wars API Data."));
    }

    /// <summary>
    /// Gets all persons from a database page.
    /// </summary>
    /// <param name="url">Url of the page.</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Returns <see cref="AllPersonsModel"/> if the <paramref name="url"/> was found, otherwise it returns <see langword="null"/>.
    /// Calling the <paramref name="token"/> while processing, it returns <see langword="null" />.</returns>
    private async Task<AllPersonsModel?> GetAllPersonFromPage(string url, CancellationToken token)
    {
        try
        {
            HttpResponseMessage? response = await _client.GetAsync(url, token);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync(token);
            return responseBody.ToAllPersonModels();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("The operation was canceled.");
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
    }
}
