using System.Net;
using System.Net.Http.Headers;
using DemoApi.Tests.Infrastructure;
using Xunit;

namespace DemoApi.Tests.Users;

public class UsersAuthorizationTests
{
    private readonly HttpClient _client;

    public UsersAuthorizationTests()
    {
        var factory = new TestWebApplicationFactory();
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_WithoutToken_Returns401()
    {
        // Act
        var response = await _client.GetAsync("/users");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUsers_WithValidToken_Returns200()
    {
        // Arrange
        var token = TestJwtTokenFactory.CreateToken(
            role: "Admin",
            scopes: new[] { "users.read" });

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/users");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetUsers_WithWrongRole_Returns403()
    {
        // Arrange
        var token = TestJwtTokenFactory.CreateToken(
            role: "User",               // ❌ not Admin/Manager
            scopes: new[] { "users.read" });

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/users");

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

}
