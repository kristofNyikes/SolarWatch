namespace SolarWatch.Services;

public interface ICurrentWeatherDataProvider
{
    Task<string> GetCurrent(string cityName);
}