using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Models.SunriseSunset;
using SolarWatch.Services;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class SunriseSunsetController : Controller
{
    private readonly IJsonProcessor _jsonProcessor;
    private readonly ICurrentWeatherDataProvider _currentWeatherDataProvider;
    private readonly ISunriseSunsetDataProvider _sunriseSunsetDataProvider;

    public SunriseSunsetController(IJsonProcessor jsonProcessor, ICurrentWeatherDataProvider currentWeatherDataProvider, ISunriseSunsetDataProvider sunriseSunsetDataProvider)
    {
        _jsonProcessor = jsonProcessor;
        _currentWeatherDataProvider = currentWeatherDataProvider;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
    }

    [HttpGet("GetSunriseAndSunset")]
    public async Task<IActionResult> Get([Required]string cityName, DateTime date)
    {
        try
        {
            var weather = await _currentWeatherDataProvider.GetCurrent(cityName);
            var weatherModel = _jsonProcessor.ProcessWeather(weather);

            Console.WriteLine($"weatherModel.Latitude: {weatherModel.Latitude}");
            Console.WriteLine($"weatherModel.Longitude: {weatherModel.Longitude}");

            var sunriseSunset = await _sunriseSunsetDataProvider.GetCurrent(weatherModel.Latitude, weatherModel.Longitude, date);
            var sunriseSunsetModel = _jsonProcessor.ProcessSunriseSunset(sunriseSunset);

            Console.WriteLine($"sunriseSunsetModel.Sunrise: {sunriseSunsetModel.Sunrise}");
            Console.WriteLine($"sunriseSunsetModel.Sunset: {sunriseSunsetModel.Sunset}");

            return Ok(sunriseSunsetModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}