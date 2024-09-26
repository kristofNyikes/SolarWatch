using SolarWatch.Models.CurrentWeather;
using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Services;

public interface IJsonProcessor
{
    Weather ProcessWeather(string data);
    SunriseSunset ProcessSunriseSunset(string data);
}