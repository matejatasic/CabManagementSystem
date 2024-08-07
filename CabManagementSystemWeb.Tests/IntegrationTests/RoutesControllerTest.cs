using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class RoutesControllerTest : BaseIntegrationTest
{
    private string _routeRoute = "/routes";
    private string _employeeRoute = "/employees";
    private string _branchRoute = "/branches";
    private string _roleRoute = "/roles";
    private string _userRoute = "/users";

    private string _routeRouteUrl;
    private string _roleRouteUrl;
    private string _employeeRouteUrl;
    private string _branchRouteUrl;
    private string _userRouteUrl;

    private int driverId = 1;
    private int travelerId = 1;

    public RoutesControllerTest() : base()
    {
        _routeRouteUrl = _routePrefix + _routeRoute;
        _employeeRouteUrl = _routePrefix + _employeeRoute;
        _branchRouteUrl = _routePrefix + _branchRoute;
        _roleRouteUrl = _routePrefix + _roleRoute;
        _userRouteUrl = _routePrefix + _userRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesARoute()
    {
        await InitializeClient();

        var response = await CreateNeededEntities();

        var content = await response.Content.ReadAsStringAsync();
        RouteDetailDto deserializedContent = JsonSerializer.Deserialize<RouteDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<RouteDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfRoutes()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_routeRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<RouteDetailDto> deserializedContent = JsonSerializer.Deserialize<List<RouteDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsARoute()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_routeRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        RouteDetailDto deserializedContent = JsonSerializer.Deserialize<RouteDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<RouteDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesARoute()
    {
        await InitializeClient();

        await CreateNeededEntities();

        string updatedFromAddress = "UpdatedFromAddress";
        RouteUpdateDto routeUpdateDto = _fixture.Build<RouteUpdateDto>()
            .With(r => r.FromAddress, updatedFromAddress)
            .With(r => r.DriverId, driverId)
            .With(r => r.TravelerId, travelerId)
            .Create();
        JsonContent routePutContent = JsonContent.Create(routeUpdateDto);

        var response = await _client.PutAsync($"{_routeRouteUrl}/1", routePutContent);
        var getByIdResponse = _client.GetAsync($"{_routeRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        RouteDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<RouteDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedFromAddress, getByIdDeserializedContent.FromAddress);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesARoute()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.DeleteAsync($"{_routeRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_routeRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<RouteDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<RouteDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }

    private async Task<HttpResponseMessage> CreateNeededEntities()
    {
        var (branchPostContent, rolePostContent, userPostContent, employeePostContent, routePostContent) = GetPostContent();

        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);
        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);
        await _client.PostAsync($"{_userRouteUrl}", userPostContent);
        await _client.PostAsync($"{_employeeRouteUrl}", employeePostContent);
        var response = await _client.PostAsync($"{_routeRouteUrl}", routePostContent);

        return response;
    }

    private Tuple<JsonContent, JsonContent, JsonContent, JsonContent, JsonContent> GetPostContent()
    {
        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .With(u => u.RoleId, 1)
            .Create();
        EmployeeCreateDto employeeCreateDto = _fixture.Build<EmployeeCreateDto>()
            .With(e => e.BranchId, 1)
            .With(e => e.UserId, 1)
            .Create();
        RouteCreateDto routeCreateDto = _fixture.Build<RouteCreateDto>()
            .With(r => r.DriverId, driverId)
            .With(r => r.TravelerId, travelerId)
            .Create();

        JsonContent routePostContent = JsonContent.Create(routeCreateDto);
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);
        JsonContent userPostContent = JsonContent.Create(userCreateDto);
        JsonContent employeePostContent = JsonContent.Create(employeeCreateDto);

        return new Tuple<JsonContent, JsonContent, JsonContent, JsonContent, JsonContent>(branchPostContent, rolePostContent, userPostContent, employeePostContent, routePostContent);
    }
}