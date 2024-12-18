namespace SolarWatch.Contracts;

public record AuthResponse(bool Success, string Email, string UserName/*, IList<string> Role*/);