using System.Net;
using System.Text;
using FluentAssertions;
using FluentAssertions.Extensions;
using Newtonsoft.Json;
using SolarWatch.Contracts;

namespace SolarWatchTests.IntegrationTests;

public class SunriseSunsetControllerTests
{
    private SolarWatchWebApplicationFactory _factory;
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _factory = new SolarWatchWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task SunriseSunsetController_Get_ReturnsOk()
    {
        const string testEmail = "testuser@user.com";
        const string testPassword = "user123";
        const string city = "Budapest";
        const string date = "2024-11-22";

        var loginRequest = new AuthRequest(testEmail, testPassword);
        var loginContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");


        var loginResponse = await _client.PostAsync("api/Auth/Login", loginContent);
        loginResponse.Should().BeSuccessful();

        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
        _client.DefaultRequestHeaders.Add("Cookie", string.Join("; ", cookies));


        var sunriseSunsetGetResponse =
            await _client.GetAsync($"api/SunriseSunset/GetSunriseAndSunset?cityName={city}&date={date}");

        var jsonResponse = await sunriseSunsetGetResponse.Content.ReadAsStringAsync();

        var sunriseSunset = JsonConvert.DeserializeObject<SunriseSunsetResponseDto>(jsonResponse);

        sunriseSunsetGetResponse.Should().BeSuccessful();
        sunriseSunset.CityId.Should().Be(1);
        sunriseSunset!.City.Name.Should().Be(city);
        sunriseSunset.Sunrise.Should().Be(22.November(2024).At(6,56,09));
        sunriseSunset.Sunset.Should().Be(22.November(2024).At(16,4,19));

    }

    [Test]
    public async Task SunriseSunsetController_Get_ReturnsError()
    {
        const string city = "Budapest";
        const string date = "2024-11-22";


        var sunriseSunsetGetResponse =
            await _client.GetAsync($"api/SunriseSunset/GetSunriseAndSunset?cityName={city}&date={date}");

        sunriseSunsetGetResponse.Should().HaveError("Unauthorized without login in");

    }
}