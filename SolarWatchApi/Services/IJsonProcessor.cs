using SolarWatch.Models.City;
using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Services;

public interface IJsonProcessor
{
    City ProcessWeather(string data);
    SunriseSunset ProcessSunriseSunset(string data);
}