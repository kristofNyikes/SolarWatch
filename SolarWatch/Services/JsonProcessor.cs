using System.Text.Json;
using SolarWatch.Models.CurrentWeather;
using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public Weather ProcessWeather(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement coord = json.RootElement.GetProperty("coord");

        Weather weather = new Weather
        {
            Latitude = coord.GetProperty("lat").GetDouble(),
            Longitude = coord.GetProperty("lon").GetDouble()
        };

        return weather;
    }

    public SunriseSunset ProcessSunriseSunset(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement results = json.RootElement.GetProperty("results");

        SunriseSunset sunriseSunset = new SunriseSunset
        {
            Sunrise = results.GetProperty("sunrise").GetDateTime(),
            Sunset = results.GetProperty("sunset").GetDateTime()
        };
        return sunriseSunset;
    }
}