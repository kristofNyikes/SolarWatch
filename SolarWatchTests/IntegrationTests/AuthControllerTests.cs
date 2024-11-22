using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using SolarWatch.Contracts;

namespace SolarWatchTests.IntegrationTests;

public class AuthControllerTests
{
    private HttpClient _client;
    private SolarWatchWebApplicationFactory _factory;


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
    public async Task Register_withValidData_ReturnsOk()
    {
        const string testEmail = "test@test.com";
        const string testUsername = "test";
        const string testPassword = "test123";

        //Arrange
        var request = new RegistrationRequest(testEmail, testUsername, testPassword);

        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync("/api/Auth/Register", content);
        //response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var (expectedEmail, expectedUserName) = JsonConvert.DeserializeObject<RegistrationResponse>(jsonResponse);

        //Assert
        //Assert.That(expectedEmail, Is.EqualTo(testEmail));
        //Assert.That(expectedUserName, Is.EqualTo(testUsername));

        response.Should().BeSuccessful();
        expectedEmail.Should().Be(testEmail);
        expectedUserName.Should().Be(testUsername);

    }

    [Test]
    public async Task Register_withShortPassword_ReturnsError()
    {
        const string testEmail = "test@test.com";
        const string testUsername = "test";
        const string testPassword = "test";
        //Arrange
        var request = new RegistrationRequest(testEmail, testUsername, testPassword);
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync("/api/Auth/Register", content);

        //Assert
        response.Should().HaveError();
    }

    [Test]
    public async Task AuthController_Login_ReturnsOk()
    {
        const string testEmail = "testuser@user.com";
        const string testPassword = "user123";

        var request = new AuthRequest(testEmail, testPassword);
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/Auth/Login", content);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var (responseObj, role) = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

        response.Should().BeSuccessful();
        responseObj.Success.Should().BeTrue();
        responseObj.UserName.Should().Be("testuser");
        role[0].Should().Be("Admin");

    }
}