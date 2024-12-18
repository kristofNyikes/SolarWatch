using SolarWatchApi.Models.SunriseSunset;

namespace SolarWatchApi.Services.Repository;

public interface ISunriseSunsetRepository
{
    Task<IEnumerable<SunriseSunset>> GetAllAsync();
    Task AddAsync(SunriseSunset sunriseSunset);
}