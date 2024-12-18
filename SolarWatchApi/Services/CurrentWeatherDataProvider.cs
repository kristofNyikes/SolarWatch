using DotNetEnv;

namespace SolarWatchApi.Services;

public class CurrentWeatherDataProvider : ICurrentWeatherDataProvider
{
    private readonly ILogger<CurrentWeatherDataProvider> _logger;

    public CurrentWeatherDataProvider(ILogger<CurrentWeatherDataProvider> logger)
    {
        _logger = logger;
    }
    public async Task<string> GetCurrent(string cityName)
    {
        Env.Load();
        var apiKey = Env.GetString("API_KEY"); 
        var url =
            $"http://api.openweathermap.org/geo/1.0/direct?q={cityName.Trim()}&appid={apiKey}";

        using var client = new HttpClient();

        _logger.LogInformation("Calling Geocoding API with url: {url}", url);

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Error occured while calling Geocoding API.");
            throw;
        }
    }
}