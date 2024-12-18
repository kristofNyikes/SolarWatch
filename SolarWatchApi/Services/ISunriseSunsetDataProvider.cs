namespace SolarWatchApi.Services;

public interface ISunriseSunsetDataProvider
{
    Task<string> GetCurrent(double lat, double lng, DateTime? date = null);
}