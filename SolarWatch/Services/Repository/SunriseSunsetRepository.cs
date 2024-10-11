using SolarWatch.Data;
using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Services.Repository;

public class SunriseSunsetRepository : ISunriseSunsetRepository
{
    private SunriseSunsetWeatherApiContext _context;

    public SunriseSunsetRepository(SunriseSunsetWeatherApiContext context)
    {
        _context = context;
    }

    public IEnumerable<SunriseSunset> GetAll()
    {
        return _context.SunriseSunsets.ToList();
    }

    public void Add(SunriseSunset sunriseSunset)
    {
        _context.Add(sunriseSunset);
        _context.SaveChanges();
    }

    public void Update(SunriseSunset sunriseSunset)
    {
        _context.Update(sunriseSunset);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var sunriseSunset = GetAll().FirstOrDefault(s => s.Id == id);
        _context.Remove(sunriseSunset);
        _context.SaveChanges();
    }
}