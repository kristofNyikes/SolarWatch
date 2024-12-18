using SolarWatchApi.Models.City;
using SolarWatchApi.Models.SunriseSunset;

namespace SolarWatchApi.Services;

public interface IJsonProcessor
{
    City ProcessWeather(string data);
    SunriseSunset ProcessSunriseSunset(string data);
}