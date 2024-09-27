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
    private readonly ILogger<SunriseSunsetController> _logger;

    public SunriseSunsetController(IJsonProcessor jsonProcessor, ICurrentWeatherDataProvider currentWeatherDataProvider, ISunriseSunsetDataProvider sunriseSunsetDataProvider, ILogger<SunriseSunsetController> logger)
    {
        _jsonProcessor = jsonProcessor;
        _currentWeatherDataProvider = currentWeatherDataProvider;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
        _logger = logger;
    }

    [HttpGet("GetSunriseAndSunset")]
    public async Task<IActionResult> Get([Required]string cityName, DateTime date)
    {
        try
        {
            var weather = await _currentWeatherDataProvider.GetCurrent(cityName);
            var weatherModel = _jsonProcessor.ProcessWeather(weather);

            var sunriseSunset = await _sunriseSunsetDataProvider.GetCurrent(weatherModel.Latitude, weatherModel.Longitude, date);
            var sunriseSunsetModel = _jsonProcessor.ProcessSunriseSunset(sunriseSunset);

            return Ok(sunriseSunsetModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("Error getting sunrise sunset data");
        }
    }
}