using Microsoft.EntityFrameworkCore;
using SolarWatchApi.Data;
using SolarWatchApi.Models.SunriseSunset;

namespace SolarWatchApi.Services.Repository;

public class SunriseSunsetRepository : ISunriseSunsetRepository
{
    private readonly AppDbContext _context;

    public SunriseSunsetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SunriseSunset>> GetAllAsync()
    {
        return await _context.SunriseSunsets.ToListAsync();
    }

    public async Task AddAsync(SunriseSunset sunriseSunset)
    {
        await _context.AddAsync(sunriseSunset);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SunriseSunset sunriseSunset)
    {
        _context.Update(sunriseSunset);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sunriseSunset = await _context.SunriseSunsets.FirstOrDefaultAsync(s => s.Id == id);
        if (sunriseSunset != null)
        {
            _context.Remove(sunriseSunset);
            await _context.SaveChangesAsync();
        }
    }
}