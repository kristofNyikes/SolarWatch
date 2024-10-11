using System.Security.Cryptography.X509Certificates;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Controllers;
using SolarWatch.Models.City;
using SolarWatch.Models.SunriseSunset;
using SolarWatch.Services;

namespace SolarWatchTest;

[TestFixture]
public class SunriseAndSunsetControllerTest
{
    private Mock<ILogger<SunriseSunsetController>> _loggerMock;
    private Mock<ISunriseSunsetDataProvider> _sunriseSunsetDataProviderMock;
    private Mock<ICurrentWeatherDataProvider> _currentWeatherDataProviderMock;
    private Mock<IJsonProcessor> _jsonProcessorMock;
    private SunriseSunsetController _controller;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<SunriseSunsetController>>();
        _sunriseSunsetDataProviderMock = new Mock<ISunriseSunsetDataProvider>();
        _currentWeatherDataProviderMock = new Mock<ICurrentWeatherDataProvider>();
        _jsonProcessorMock = new Mock<IJsonProcessor>();
        _controller = new SunriseSunsetController(_jsonProcessorMock.Object, _currentWeatherDataProviderMock.Object,
            _sunriseSunsetDataProviderMock.Object, _loggerMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
    }
    [Test]
    public async Task Get_ReturnsSunriseSunsetData_WhenValidCityAndDateProvided()
    {
        // Arrange
        var cityName = "ValidCity";
        var date = DateTime.Now;

        var weatherData = "WeatherJson";
        var sunriseSunsetData = "SunriseSunsetJson";

        var weatherModel = new City { Latitude = 10.0, Longitude = 20.0 };
        var sunriseSunsetModel = new SunriseSunset
        {
            Sunrise = DateTime.UtcNow,
            Sunset = DateTime.UtcNow.AddHours(12)
        };

        _currentWeatherDataProviderMock.Setup(m => m.GetCurrent(cityName)).ReturnsAsync(weatherData);
        _jsonProcessorMock.Setup(m => m.ProcessWeather(weatherData)).Returns(weatherModel);
        _sunriseSunsetDataProviderMock.Setup(m => m.GetCurrent(weatherModel.Latitude, weatherModel.Longitude, date)).ReturnsAsync(sunriseSunsetData);
        _jsonProcessorMock.Setup(m => m.ProcessSunriseSunset(sunriseSunsetData)).Returns(sunriseSunsetModel);

        // Act
        var result = await _controller.Get(cityName, date) as OkObjectResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(200));
        Assert.That(result.Value, Is.EqualTo(sunriseSunsetModel));
    }

    [Test]
    public async Task Get_ReturnsNotFound_WhenExceptionIsThrown()
    {
        // Arrange
        var cityName = "InvalidCity";
        var date = DateTime.Now;

        _currentWeatherDataProviderMock.Setup(m => m.GetCurrent(cityName)).ThrowsAsync(new Exception("City not found"));

        // Act
        var result = await _controller.Get(cityName, date) as NotFoundObjectResult;

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(404));
        Assert.That(result.Value, Is.EqualTo("Error getting sunrise sunset data"));
    }

}