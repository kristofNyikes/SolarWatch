namespace SolarWatchApi.Models.City;

public class City
{
    public int Id { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }

    public ICollection<SunriseSunset.SunriseSunset> SunriseSunsets { get; set; }
}