using System.Text.Json;
using SolarWatchApi.Models.City;
using SolarWatchApi.Models.SunriseSunset;

namespace SolarWatchApi.Services;

public class JsonProcessor : IJsonProcessor
{
    public City ProcessWeather(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement coord = json.RootElement[0];

        City weather = new City
        {
            Latitude = coord.GetProperty("lat").GetDouble(),
            Longitude = coord.GetProperty("lon").GetDouble(),
            Name = coord.GetProperty("name").GetString()!,
            Country = coord.GetProperty("country").GetString()!
        };

        return weather;
    }

    public SunriseSunset ProcessSunriseSunset(string data)
    {
        try
        {
            if (data.Length == 0)
            {
                throw new ArgumentException("Incorrect name");
            }
            JsonDocument json = JsonDocument.Parse(data);
            JsonElement results = json.RootElement.GetProperty("results");

            var sunrise = DateTime.Parse(results.GetProperty("sunrise").GetString()!, null, System.Globalization.DateTimeStyles.AssumeUniversal);

            var sunset = DateTime.Parse(results.GetProperty("sunset").GetString()!, null, System.Globalization.DateTimeStyles.AssumeUniversal);


            SunriseSunset sunriseSunset = new SunriseSunset
            {
                Sunrise = sunrise,
                Sunset = sunset
            };
            return sunriseSunset;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        catch (FormatException e)
        {
            Console.WriteLine("Error parsing the time: " + e.Message);
            throw;
        }

    }
}