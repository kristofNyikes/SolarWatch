using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Contracts;
using SolarWatch.Models.City;
using SolarWatch.Services.Repository;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCities([Required, FromQuery] bool sunriseSunset)
    {
        IEnumerable<City> cities;
        if (sunriseSunset)
        {
            cities = await _cityRepository.GetAllWithSunriseSunsetAsync();
        }

        cities = await _cityRepository.GetAllAsync();

    foreach (var city in cities)
        {
            Console.WriteLine(city.Name);
        }
        return Ok(cities);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] CityDto city)
    {
        var currentCity = await _cityRepository.GetByNameAsync(city.Name);
        if (currentCity != null)
        {
            return Conflict($"A city called {city.Name} already exists.");
        }

        currentCity = new City
        {
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            Country = city.Country
        };
        await _cityRepository.AddAsync(currentCity);
        return Ok(currentCity);
    }
}