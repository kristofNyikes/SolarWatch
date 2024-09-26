namespace SolarWatch.Models.SunriseSunset;

public class SunriseSunset
{
    public DateTime Sunrise { get; set; }
    public DateTime Sunset { get; set; }
    public DateTime SolarNoon { get; set; }
    public TimeSpan DayLength { get; set; }
    public DateTime CivilTwilightBegin { get; set; }
    public DateTime CivilTwilightEnd { get; set; }
    public DateTime NautricalTwilightBegin { get; set; }
    public DateTime NautricalTwilightEnd { get; set; }
    public DateTime AstronomicalTwilightBegin { get; set; }
    public DateTime AstronomicalTwilightEnd { get; set; }
}