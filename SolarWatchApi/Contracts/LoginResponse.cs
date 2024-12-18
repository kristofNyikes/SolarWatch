namespace SolarWatch.Contracts;

public record LoginResponse(AuthResponse Response, IList<string> Role);