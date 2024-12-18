namespace SolarWatchApi.Services;

public class SunriseSunsetDataProvider : ISunriseSunsetDataProvider
{
    private readonly ILogger<SunriseSunsetDataProvider> _logger;

    public SunriseSunsetDataProvider(ILogger<SunriseSunsetDataProvider> logger)
    {
        _logger = logger;
    }
    public async Task<string> GetCurrent(double lat, double lng, DateTime? date = null)
    {
        var actualDate = date?.Date ?? DateTime.Now;
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lng}&date={actualDate:yyyy-MM-dd}&formatted=0";

        using var client = new HttpClient();

        _logger.LogInformation("Calling Sunrise sunset API with url: {url}", url);

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            _logger.LogError(e, "Error occured while calling Sunrise sunset API.");
            throw;
        }
    }
}