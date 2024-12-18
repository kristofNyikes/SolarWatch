using Microsoft.EntityFrameworkCore;
using SolarWatchApi.Data;
using SolarWatchApi.Models.City;

namespace SolarWatchApi.Services.Repository;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _context.Cities.ToListAsync();
    }

    public async Task<IEnumerable<City>> GetAllWithSunriseSunsetAsync()
    {
        return await _context.Cities.Include(c => c.SunriseSunsets).ToListAsync();
    }

    public async Task<City?> GetByNameAsync(string name)
    {
        return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task AddAsync(City city)
    {
        await _context.AddAsync(city);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(City city)
    {
        _context.Update(city);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(City city)
    {
        _context.Remove(city);
        await _context.SaveChangesAsync();
    }
}