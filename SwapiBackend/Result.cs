namespace SwapiBackend;

/// <summary>
/// Result object to get more information.
/// </summary>
/// <param name="success"><see langword="true"/> if there was success. Otherwise <see langword="false"/></param>
/// <param name="message">Optional Error message.</param>
public class Result(bool success, string message)
{
    public bool Success { get; } = success;
    public string Message { get; } = message;
}
