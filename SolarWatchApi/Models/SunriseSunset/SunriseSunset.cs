namespace SolarWatchApi.Models.SunriseSunset;

public class SunriseSunset
{
    public int Id { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public int CityId { get; set; }
    public City.City City { get; set; }
}