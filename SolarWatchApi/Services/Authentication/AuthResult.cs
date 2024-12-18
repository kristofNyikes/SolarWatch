namespace SolarWatchApi.Services.Authentication;

public record AuthResult(
    bool Success,
    string Email,
    string UserName)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}