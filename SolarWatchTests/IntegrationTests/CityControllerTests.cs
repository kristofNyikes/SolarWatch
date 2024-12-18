using FluentAssertions;
using Newtonsoft.Json;
using SolarWatch.Contracts;
using SolarWatch.Models.City;
using System.Text;

namespace SolarWatchTests.IntegrationTests;

public class CityControllerTests
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
    public async Task CityController_CreateCity_ReturnsCreatedCity()
    {
        const string testEmail = "testuser@user.com";
        const string testPassword = "user123";

        var loginRequest = new AuthRequest(testEmail, testPassword);
        var loginContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");


        var loginResponse = await _client.PostAsync("api/Auth/Login", loginContent);
        loginResponse.Should().BeSuccessful();

        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
        _client.DefaultRequestHeaders.Add("Cookie", string.Join("; ", cookies));

        var postRequest = new CityDto("London", 51.514405, -0.120846, "GB");
        var postContent =
            new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json");

        var postResponse = await _client.PostAsync("api/City", postContent);

        postResponse.Should().BeSuccessful();
    }

    [Test]
    public async Task CityController_GetAllCities_ReturnsListOfCities()
    {
        const string testEmail = "testuser@user.com";
        const string testPassword = "user123";

        var loginRequest = new AuthRequest(testEmail, testPassword);
        var loginContent = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");


        var loginResponse = await _client.PostAsync("api/Auth/Login", loginContent);
        loginResponse.Should().BeSuccessful();

        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
        _client.DefaultRequestHeaders.Add("Cookie", string.Join("; ", cookies));

        var postRequest = new CityDto("London", 51.514405, -0.120846, "GB");
        var postContent =
            new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json");

        var postResponse = await _client.PostAsync("api/City", postContent);

        var response = await _client.GetAsync("/api/City?sunriseSunset=false");

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var cities = JsonConvert.DeserializeObject<List<City>>(jsonResponse);

        response.Should().BeSuccessful();
        response.Should().NotBeNull();
        cities.Count.Should().BeGreaterOrEqualTo(1);
        cities[0].Name.Should().Be("London");
        cities[0].Country.Should().Be("GB");

    }
}