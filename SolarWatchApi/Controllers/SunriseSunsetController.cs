using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatchApi.Contracts;
using SolarWatchApi.Models.City;
using SolarWatchApi.Models.SunriseSunset;
using SolarWatchApi.Services;
using SolarWatchApi.Services.Repository;

namespace SolarWatchApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SunriseSunsetController : Controller
{
    private readonly IJsonProcessor _jsonProcessor;
    private readonly ICurrentWeatherDataProvider _currentWeatherDataProvider;
    private readonly ISunriseSunsetDataProvider _sunriseSunsetDataProvider;
    private readonly ILogger<SunriseSunsetController> _logger;
    private readonly ICityRepository _cityRepository;
    private readonly ISunriseSunsetRepository _sunriseSunsetRepository;

    public SunriseSunsetController(IJsonProcessor jsonProcessor, ICurrentWeatherDataProvider currentWeatherDataProvider, ISunriseSunsetDataProvider sunriseSunsetDataProvider, ILogger<SunriseSunsetController> logger, ICityRepository cityRepository, ISunriseSunsetRepository sunriseSunsetRepository)
    {
        _jsonProcessor = jsonProcessor;
        _currentWeatherDataProvider = currentWeatherDataProvider;
        _sunriseSunsetDataProvider = sunriseSunsetDataProvider;
        _logger = logger;
        _cityRepository = cityRepository;
        _sunriseSunsetRepository  = sunriseSunsetRepository;
    }

    [HttpGet("GetSunriseAndSunset"), Authorize(Roles="User, Admin")]
    public async Task<IActionResult> Get([Required] string cityName, DateTime? date)
    {
        try
        {
            var city = await _cityRepository.GetByNameAsync(cityName);
            if (city == null)
            {
                var weather = await _currentWeatherDataProvider.GetCurrent(cityName);
                var weatherModel = _jsonProcessor.ProcessWeather(weather);

                city = new City
                {
                    Name = cityName,
                    Latitude = weatherModel.Latitude,
                    Longitude = weatherModel.Longitude,
                    Country = weatherModel.Country
                };

                await _cityRepository.AddAsync(city);
            }

            var actualDate = date ?? DateTime.Now;
            var sunriseSunsetList = await _sunriseSunsetRepository.GetAllAsync();

            var sunriseSunset = sunriseSunsetList
                .FirstOrDefault(s => s.CityId == city.Id && s.Sunrise.Date.ToString("yyyy-MM-dd") == actualDate.ToString("yyyy-MM-dd"));

            if (sunriseSunset == null)
            {
                var externalSunriseSunset = await _sunriseSunsetDataProvider
                    .GetCurrent(city.Latitude, city.Longitude, actualDate);

                var sunriseSunsetModel = _jsonProcessor
                    .ProcessSunriseSunset(externalSunriseSunset);

                sunriseSunset = new SunriseSunset
                {
                    CityId = city.Id,
                    Sunrise = sunriseSunsetModel.Sunrise,
                    Sunset = sunriseSunsetModel.Sunset
                };

                await _sunriseSunsetRepository.AddAsync(sunriseSunset);
            }

            var cityDto = new CityDto(city.Name, city.Latitude, city.Longitude, city.Country);
                var response = new SunriseSunsetResponseDto
                { CityId = sunriseSunset.CityId, Sunrise = sunriseSunset.Sunrise, Sunset = sunriseSunset.Sunset, City = cityDto};
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting sunrise/sunset data for city: {CityName}", cityName);
            return NotFound("Error getting sunrise/sunset data");
        }
    }
}