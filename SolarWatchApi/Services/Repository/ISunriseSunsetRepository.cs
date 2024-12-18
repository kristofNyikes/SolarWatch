using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Services.Repository;

public interface ISunriseSunsetRepository
{
    Task<IEnumerable<SunriseSunset>> GetAllAsync();
    Task AddAsync(SunriseSunset sunriseSunset);
    Task UpdateAsync(SunriseSunset sunriseSunset);
    Task DeleteAsync(int id);
}