namespace SolarWatchApi.Contracts;

public class SunriseSunsetResponseDto
{
    public int CityId { get; set; }
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public CityDto City { get; set; }
}