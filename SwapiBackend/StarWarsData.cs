using SwapiBackend.DTOs;
using SwapiBackend.IO;
using SwapiBackend.Mapping;
using SwapiBackend.Models;
using System.Diagnostics;

namespace SwapiBackend;

public class StarWarsData : ISwapiPersons
{
    private string sourceUrl = "";

    private IStarWarsDataRepository? dataReader;

    /// <summary>
    /// Data cache.
    /// </summary>
    private List<PersonModel>? personModelsCache;

    private bool invalidData = false;

    /// <summary>
    /// Internal cancellation token source to cancel all pending requests.
    /// </summary>
    private readonly CancellationTokenSource _internalCancellationTokenSource = new();

    public StarWarsData(string? source = null)
    {
        SourceUri = source ?? "";
    }

    /// <inheritdoc />
    public string SourceUri
    {
        // Is needed to detect source URL changes in the future
        get => sourceUrl;
        set
        {
            sourceUrl = value;
            dataReader = LoadStarWarsDataReader(sourceUrl);
        }
    }

    /// <inheritdoc />
    public bool InvalidData
    {
        get => invalidData;
        set => invalidData = value;
    }

    /// <inheritdoc />
    public async Task<List<PersonDTO>?> GetAllPersonNamesAsync() => await GetAllPersonNamesAsync(CancellationToken.None);

    /// <inheritdoc />
    public async Task<List<PersonDTO>?> GetAllPersonNamesAsync(CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);
        try
        {
            List<PersonModel>? allPersons = await GetAllPersonModelsAsync(linkedCts.Token);
            return allPersons?.ToPersonEntities();
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
    public async Task<List<PersonDetailDTO>?> GetAllPersonDetailsAsync() => await GetAllPersonDetailsAsync(CancellationToken.None);

    /// <inheritdoc />
    public async Task<List<PersonDetailDTO>?> GetAllPersonDetailsAsync(CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            List<PersonModel>? allPersons = await GetAllPersonModelsAsync(linkedCts.Token);
            return allPersons?.ToPersonDetailEntities();
        }
        catch (OperationCanceledException)
        {
            Debug.WriteLine("The operation was canceled.");
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task<PersonDTO?> GetPersonAsync(string name) => await GetPersonAsync(name, CancellationToken.None);

    /// <inheritdoc />
    public async Task<PersonDTO?> GetPersonAsync(string name, CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            List<PersonDTO>? allPersons = await GetAllPersonNamesAsync(linkedCts.Token);
            return allPersons?.FirstOrDefault(p => p.Name == name);
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
    public async Task<PersonDTO?> GetPersonAsync(int id) => await GetPersonAsync(id, CancellationToken.None);

    /// <inheritdoc />
    public async Task<PersonDTO?> GetPersonAsync(int id, CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            PersonModel? personModel = await GetPersonModelAsync(id, linkedCts.Token);
            return personModel?.ToPersonEntitiy();
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
    public async Task<PersonDetailDTO?> GetPersonDetailAsync(string name) => await GetPersonDetailAsync(name, CancellationToken.None);

    /// <inheritdoc />
    public async Task<PersonDetailDTO?> GetPersonDetailAsync(string name, CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            List<PersonDetailDTO>? allPersons = await GetAllPersonDetailsAsync(linkedCts.Token);
            return allPersons?.FirstOrDefault(p => p.Name == name);
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
    public async Task<PersonDetailDTO?> GetPersonDetailAsync(int id) => await GetPersonDetailAsync(id, CancellationToken.None);

    /// <inheritdoc />
    public async Task<PersonDetailDTO?> GetPersonDetailAsync(int id, CancellationToken token)
    {
        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            PersonModel? personModel = await GetPersonModelAsync(id, linkedCts.Token);
            return personModel?.ToPersonDetailEntity();
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
    public async Task<Result> UpdatePersonAsync(PersonUpdateDTO updatePerson) => await UpdatePersonAsync(updatePerson, CancellationToken.None);

    /// <inheritdoc />
    public async Task<Result> UpdatePersonAsync(PersonUpdateDTO updatePerson, CancellationToken token)
    {
        if (dataReader is null) return new Result(false, "No data reader available.");

        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, _internalCancellationTokenSource.Token);

        try
        {
            Result result = await dataReader.UpdatePersonData(updatePerson, linkedCts.Token);
            if (result.Success) return result;

            // Update the cache
            List<PersonModel>? cachedPersonModels = await GetAllPersonModelsAsync(linkedCts.Token);
            if (cachedPersonModels is null) return new Result(false, "No cache available");
            // #1 find the person in the cache
            PersonModel? personModel = cachedPersonModels.FirstOrDefault(person => person.IsSamePerson(updatePerson));
            // No success if the person does not exist
            if (personModel is null) return new Result(false, "Person not found.");

            // #2 update the referenced data
            _ = personModel.UpdatePersonModel(updatePerson);
            return new Result(true, "");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("The operation was canceled.");
            return new Result(false, "The operation was canceled.");
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return new Result(false, $"An error occurred: {e.Message}");
        }
    }

    /// <summary>
    /// Get a person model async by an <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>If succes <see cref="PersonModel"/> otherwise <see langword="null"/>.</returns>
    private async Task<PersonModel?> GetPersonModelAsync(int id, CancellationToken token)
    {
        if (dataReader == null) return null;

        try
        {
            return await dataReader.GetPersonModelAsync(id, token);
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

    /// <summary>
    /// Get all person models from data source async.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>If succes <see cref="List{PersonModel}"/> otherwise <see langword="null"/>.</returns>
    private async Task<List<PersonModel>?> GetAllPersonModelsAsync(CancellationToken token)
    {
        if (dataReader == null) return null;

        if (invalidData || personModelsCache is null)
        {
            personModelsCache = await dataReader.GetAllPersonModelsAsync(token)
                                                .ToListAsync(token);
            invalidData = false;
        }
        return personModelsCache;
    }

    /// <summary>
    /// Get person model async by <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="token"></param>
    /// <returns>If succes <see cref="PersonModel"/> otherwise <see langword="null"/>.</returns>
    private async Task<PersonModel?> GetPersonModelAsync(string name, CancellationToken token)
    {
        if (dataReader == null) return null;

        try
        {
            return await dataReader.GetPersonModelAsync(name, token);
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

    /// <summary>
    /// Load the Star Wars data reader.
    /// </summary>
    /// <param name="sourceUri"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException">Is fired because it supports only web URIs currently.</exception>
    private static IStarWarsDataRepository LoadStarWarsDataReader(string sourceUri)
    {
        if (string.IsNullOrWhiteSpace(sourceUri)) return new SWWebApiPeople(); // Default

        Uri uri = new(sourceUri);
        if (uri.IsFile)
        {
            throw new NotImplementedException("Currently only web URIs are supported.");
        }
        else
        {
            // There is only one possibility to load this data from the web
            return new SWWebApiPeople();
        }
    }

    /// <inheritdoc />
    public void CancelPendingRequests()
    {
        _internalCancellationTokenSource.Cancel();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        CancelPendingRequests();
        _internalCancellationTokenSource.Dispose();
        dataReader?.Dispose();
    }

}
