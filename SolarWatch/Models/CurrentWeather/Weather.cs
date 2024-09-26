namespace SolarWatch.Models.CurrentWeather;

public class Weather
{
    // Coord fields
    public double CoordLon { get; set; }
    public double CoordLat { get; set; }

    // Weather fields
    public int WeatherId { get; set; }
    public string WeatherMain { get; set; }
    public string WeatherDescription { get; set; }
    public string WeatherIcon { get; set; }

    // Base field
    public string Base { get; set; }

    // Main fields
    public double MainTemp { get; set; }
    public double MainFeelsLike { get; set; }
    public double MainTempMin { get; set; }
    public double MainTempMax { get; set; }
    public int MainPressure { get; set; }
    public int MainHumidity { get; set; }
    public int MainSeaLevel { get; set; }
    public int MainGrndLevel { get; set; }

    // Visibility field
    public int Visibility { get; set; }

    // Wind fields
    public double WindSpeed { get; set; }
    public int WindDeg { get; set; }
    public double WindGust { get; set; }

    // Clouds fields
    public int CloudsAll { get; set; }

    // Sys fields
    public int SysType { get; set; }
    public int SysId { get; set; }
    public string SysCountry { get; set; }
    public long SysSunrise { get; set; } // Unix timestamp
    public long SysSunset { get; set; }  // Unix timestamp

    // Other fields
    public long Dt { get; set; } // Unix timestamp
    public int Timezone { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cod { get; set; }
}