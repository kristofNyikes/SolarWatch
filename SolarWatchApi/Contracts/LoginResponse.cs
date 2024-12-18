namespace SolarWatchApi.Contracts;

public record LoginResponse(AuthResponse Response, IList<string> Role);