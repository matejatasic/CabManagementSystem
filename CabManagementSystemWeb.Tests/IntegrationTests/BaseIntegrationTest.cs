using System.Text.Json;
using AutoFixture;

namespace CabManagementSystemWeb.Tests.Controllers;

public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program, ApplicationDbContext>>, IDisposable
{
    protected string _routePrefix = "api";
    protected CustomWebApplicationFactory<Program, ApplicationDbContext> _factory;
    protected Fixture _fixture;
    protected HttpClient _client;
    protected JsonSerializerOptions _jsonSerializerOptions;


    public BaseIntegrationTest()
    {
        _factory = new CustomWebApplicationFactory<Program, ApplicationDbContext>();
        _fixture = new Fixture();
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async void Dispose()
    {
        await _factory.DisposeAsync();
    }

    protected async Task InitializeClient()
    {
        await _factory.InitializeAsync();

        _client = _factory.CreateClient();
    }
}