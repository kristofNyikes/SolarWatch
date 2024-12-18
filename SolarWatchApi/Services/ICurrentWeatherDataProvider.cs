namespace SolarWatchApi.Services;

public interface ICurrentWeatherDataProvider
{
    Task<string> GetCurrent(string cityName);
}