using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SolarWatch.Contracts;
using SolarWatch.Models.City;
using SolarWatch.Services.Repository;

namespace SolarWatch.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CityController(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    [HttpGet, Authorize(Roles = "Admin")]
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

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateCity([FromBody] CityDto city)
    {
        var currentCity = await _cityRepository.GetByNameAsync(city.Name);
        if (currentCity != null)
        {
            return Conflict($"A city called {city.Name} already exists.");
        }

        if (city.Latitude == null || city.Longitude == null)
        {
            return BadRequest("Latitude and Longitude must be provided.");
        }

        currentCity = new City
        {
            Name = city.Name,
            Latitude = city.Latitude.Value,
            Longitude = city.Longitude.Value,
            Country = city.Country!
        };
        await _cityRepository.AddAsync(currentCity);
        return Ok(currentCity);
    }

    [HttpPut, Authorize(Roles="Admin")]
    public async Task<IActionResult> UpdateCity([FromBody] CityDto city)
    {
        var currentCity = await _cityRepository.GetByNameAsync(city.Name);
        if (currentCity == null)
        {
            return NotFound($"No city called {city.Name} exists.");
        }

        currentCity.Latitude = city.Latitude ?? currentCity.Latitude;
        currentCity.Longitude = city.Longitude ?? currentCity.Longitude;
        currentCity.Country = city.Country ?? currentCity.Country;


        await _cityRepository.UpdateAsync(currentCity);

        return Ok(currentCity);
    }

    [HttpDelete, Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCity([Required, FromQuery]string name)
    {
        var city = await _cityRepository.GetByNameAsync(name);
        if (city == null)
        {
            return NotFound($"No city called {name} exists.");
        }

        await _cityRepository.DeleteAsync(city);
        return NoContent();
    }
}