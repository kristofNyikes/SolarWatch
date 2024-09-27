using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch.Controllers;
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
}