using SolarWatch.Data;
using SolarWatch.Models.City;

namespace SolarWatch.Services.Repository;

public class CityRepository : ICityRepository
{
    private SunriseSunsetWeatherApiContext _context;

    public CityRepository(SunriseSunsetWeatherApiContext context)
    {
        _context  = context;
    }

    public IEnumerable<City> GetAll()
    {
        return _context.Cities.ToList();
    }

    public City? GetByName(string name)
    {
        return _context.Cities.FirstOrDefault(c => c.Name == name);
    }

    public void Add(City city)
    {
        _context.Add(city);
        _context.SaveChanges();
    }

    public void Update(City city)
    {
        _context.Update(city);
        _context.SaveChanges();
    }

    public void Delete(City city)
    {
        _context.Remove(city);
        _context.SaveChanges();
    }
}