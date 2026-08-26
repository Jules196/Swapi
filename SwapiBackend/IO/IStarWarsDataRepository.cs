using SwapiBackend.DTOs;
using SwapiBackend.Models;

namespace SwapiBackend.IO;

/// <summary>
/// Interface for a repository that provides access to Star Wars data.
/// </summary>
internal interface IStarWarsDataRepository : IDisposable
{
    /// <summary>
    /// Iterator for all person models.
    /// </summary>
    /// <returns></returns>
    IAsyncEnumerable<PersonModel> GetAllPersonModelsAsync();

    /// <summary>
    /// Iterator for all person models.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns></returns>
    IAsyncEnumerable<PersonModel> GetAllPersonModelsAsync(CancellationToken token);

    /// <summary>
    /// Get a person model async by an <paramref name="id"/> from a providing Star Wars data repository.
    /// </summary>
    /// <param name="id">Id of the person in the database.</param>
    /// <returns>Returns <see cref="PersonModel"/> if the id was found, otherwise it returns <see langword="null"/>.</returns>
    Task<PersonModel?> GetPersonModelAsync(int id);

    /// <summary>
    /// Get a person model async by an <paramref name="id"/> from a providing Star Wars data repository.
    /// </summary>
    /// <param name="id">Id of the person in the database.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns <see cref="PersonModel"/> if the <paramref name="id"/> was found, otherwise it returns <see langword="null"/>.
    /// Calling the <paramref name="token"/> while processing, it returns <see langword="null" />.</returns>
    Task<PersonModel?> GetPersonModelAsync(int id, CancellationToken token);

    /// <summary>
    /// Get a person model async by a <paramref name="name"/> from a providing Star Wars data repository.
    /// </summary>
    /// <param name="name">Name of the person.</param>
    /// <returns>Returns <see cref="PersonModel"/> if the <paramref name="name"/> was found, otherwise it returns <see langword="null"/>.</returns>
    Task<PersonModel?> GetPersonModelAsync(string name);

    /// <summary>
    /// Get a person model async by a <paramref name="name"/> from a providing Star Wars data repository.
    /// </summary>
    /// <param name="name">Name of the person.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns <see cref="PersonModel"/> if the <paramref name="name"/> was found, otherwise it returns <see langword="null"/>.
    /// Calling the <paramref name="token"/> while processing, it returns <see langword="null" />.</returns>
    Task<PersonModel?> GetPersonModelAsync(string name, CancellationToken token);

    /// <summary>
    /// Updates a persons data. The person is identified by the <paramref name="person"/>.Name parameter.
    /// </summary>
    /// <param name="person">Person with the new attributes</param>
    /// <returns>Returns <see cref="Result"/> with the information of the success and potential error messages.</returns>
    Task<Result> UpdatePersonData(PersonUpdateDTO person);

    /// <summary>
    /// Updates a persons data. The person is identified by the <paramref name="person"/>.Name parameter.
    /// </summary>
    /// <param name="person">Person with the new attributes</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Returns <see cref="Result"/> with the information of the success and potential error messages.
    /// Calling the <paramref name="token"/> while processing, it returns <see cref="Result"/> with no success.</returns>
    Task<Result> UpdatePersonData(PersonUpdateDTO person, CancellationToken token);
}
