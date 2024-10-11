using Microsoft.EntityFrameworkCore;
using SolarWatch.Models.City;
using SolarWatch.Models.SunriseSunset;

namespace SolarWatch.Data;

public class SunriseSunsetWeatherApiContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunset> SunriseSunsets { get; set; }

    public SunriseSunsetWeatherApiContext(DbContextOptions<SunriseSunsetWeatherApiContext> options) :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<SunriseSunset>()
            .HasOne(s => s.City)
            .WithMany(c => c.SunriseSunsets)
            .HasForeignKey(s => s.CityId);
    }
}