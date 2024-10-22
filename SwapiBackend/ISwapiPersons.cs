using SwapiBackend.DTOs;

namespace SwapiBackend;

/// <summary>
/// Public Api interface.
/// </summary>
public interface ISwapiPersons : IDisposable
{
    /// <summary>
    /// Source Uri of the database.
    /// </summary>
    string SourceUri { get; set; }

    /// <summary>
    /// Invalidate data.
    /// </summary>
    bool InvalidData { get; set; }

    /// <summary>
    /// Get all <see cref="PersonDTO"/> async.
    /// </summary>
    /// <returns>Returns <see langword="null"/> if the task failed, otherwise it returns a list of <see cref="PersonDTO"/>.</returns>
    Task<List<PersonDTO>?> GetAllPersonNamesAsync();

    /// <summary>
    /// Get all <see cref="PersonDTO"/> async.
    /// </summary>
    /// <param name="token">Cancellation token,</param>
    /// <returns>Returns <see langword="null"/> if the task failed or cancelled, otherwise it returns a list of <see cref="PersonDTO"/>.</returns>
    Task<List<PersonDTO>?> GetAllPersonNamesAsync(CancellationToken token);


    /// <summary>
    /// Get all <see cref="PersonDetailDTO"/> async.
    /// </summary>
    /// <returns>Returns <see langword="null"/> if the task failed, otherwise it returns a list of <see cref="PersonDetailDTO"/>.</returns>
    Task<List<PersonDetailDTO>?> GetAllPersonDetailsAsync();

    /// <summary>
    /// Get all <see cref="PersonDetailDTO"/> async.
    /// </summary>
    /// <returns>Returns <see langword="null"/> if the task failed or cancelled, otherwise it returns a list of <see cref="PersonDetailDTO"/>.</returns>
    Task<List<PersonDetailDTO>?> GetAllPersonDetailsAsync(CancellationToken token);


    /// <summary>
    /// Get a person async by a <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>If succes <see cref="PersonDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDTO?> GetPersonAsync(string name);

    /// <summary>
    /// Get a person async by a <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>If succes <see cref="PersonDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDTO?> GetPersonAsync(string name, CancellationToken token);


    /// <summary>
    /// Get a person async by an <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>If succes <see cref="PersonDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDTO?> GetPersonAsync(int id);

    /// <summary>
    /// Get a person async by an <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>If succes <see cref="PersonDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDTO?> GetPersonAsync(int id, CancellationToken token);


    /// <summary>
    /// Get a person detail async by a <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>If succes <see cref="PersonDetailDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDetailDTO?> GetPersonDetailAsync(string name);

    /// <summary>
    /// Get a person detail async by a <paramref name="name"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="token"></param>
    /// <returns>If succes <see cref="PersonDetailDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDetailDTO?> GetPersonDetailAsync(string name, CancellationToken token);


    /// <summary>
    /// Get a person detail async by an <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>If succes <see cref="PersonDetailDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDetailDTO?> GetPersonDetailAsync(int id);

    /// <summary>
    /// Get a person detail async by an <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>If succes <see cref="PersonDetailDTO"/> otherwise <see langword="null"/>.</returns>
    Task<PersonDetailDTO?> GetPersonDetailAsync(int id, CancellationToken token);

    /// <summary>
    /// Update a person async. The person is identified by the <paramref name="person"/>.Name parameter.
    /// </summary>
    /// <param name="person"></param>
    /// <returns>If success it returns <see cref="Result"/>Success = <see langword="true"/>. Otherwise see cref="Result"/>Success = <see langword="false"/> and the reason.</returns>
    Task<Result> UpdatePersonAsync(PersonUpdateDTO person);

    /// <summary>
    /// Update a person async. The person is identified by the <paramref name="person"/>.Name parameter.
    /// </summary>
    /// <param name="person"></param>
    /// <param name="token"></param>
    /// <returns>If success it returns <see cref="Result"/>Success = <see langword="true"/>. Otherwise see cref="Result"/>Success = <see langword="false"/> and the reason.</returns>
    Task<Result> UpdatePersonAsync(PersonUpdateDTO person, CancellationToken token);

    /// <summary>
    /// Cancel all pending requests.
    /// </summary>
    void CancelPendingRequests();
}
