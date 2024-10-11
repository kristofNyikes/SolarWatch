using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Services.Repository;

public interface ISunriseSunsetRepository
{
    IEnumerable<SunriseSunset> GetAll();
    void Add(SunriseSunset sunriseSunset);
    void Update(SunriseSunset sunriseSunset);
    void Delete(int id);
}