using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class CarsControllerTest : BaseIntegrationTest
{
    private string _carRoute = "/cars";
    private string _employeeRoute = "/employees";
    private string _branchRoute = "/branches";

    private string _carRouteUrl;
    private string _employeeRouteUrl;
    private string _branchRouteUrl;

    public CarsControllerTest() : base()
    {
        _carRouteUrl = _routePrefix + _carRoute;
        _employeeRouteUrl = _routePrefix + _employeeRoute;
        _branchRouteUrl = _routePrefix + _branchRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesACar()
    {
        await InitializeClient();

        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        EmployeeCreateDto employeeCreateDto = _fixture.Build<EmployeeCreateDto>()
            .With(e => e.BranchId, 1).Create();
        CarCreateDto carCreateDto = _fixture.Build<CarCreateDto>()
            .With(c => c.DriverId, 1)
            .Without(c => c.RegisteredUntil)
            .Create();

        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        JsonContent employeePostContent = JsonContent.Create(employeeCreateDto);
        JsonContent carPostContent = JsonContent.Create(carCreateDto);

        var response1 = await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);
        var response2 = await _client.PostAsync($"{_employeeRouteUrl}", employeePostContent);

        var response = await _client.PostAsync($"{_carRouteUrl}", carPostContent);

        var content = await response.Content.ReadAsStringAsync();

        CarDetailDto deserializedContent = JsonSerializer.Deserialize<CarDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<CarDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfCars()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_carRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<CarDetailDto> deserializedContent = JsonSerializer.Deserialize<List<CarDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsAnCar()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_carRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        CarDetailDto deserializedContent = JsonSerializer.Deserialize<CarDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<CarDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesAnCar()
    {
        await InitializeClient();

        await CreateNeededEntities();
        string updatedName = "UpdatedName";
        CarUpdateDto carUpdateDto = _fixture.Build<CarUpdateDto>()
            .With(c => c.Name, updatedName)
            .With(c => c.DriverId, 1)
            .With(c => c.RegisteredUntil, DateTime.UtcNow)
            .Create();
        JsonContent carPutContent = JsonContent.Create(carUpdateDto);

        var response = await _client.PutAsync($"{_carRouteUrl}/1", carPutContent);
        var getByIdResponse = _client.GetAsync($"{_carRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        CarDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<CarDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedName, getByIdDeserializedContent.Name);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesAnCar()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.DeleteAsync($"{_carRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_carRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<CarDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<CarDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }

    private async Task CreateNeededEntities()
    {
        var (branchPostContent, employeePostContent, carPostContent) = GetPostContent();

        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);
        await _client.PostAsync($"{_employeeRouteUrl}", employeePostContent);
        await _client.PostAsync($"{_carRouteUrl}", carPostContent);
    }

    private Tuple<JsonContent, JsonContent, JsonContent> GetPostContent()
    {
        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        EmployeeCreateDto employeeCreateDto = _fixture.Build<EmployeeCreateDto>()
            .With(e => e.BranchId, 1).Create();
        CarCreateDto carCreateDto = _fixture.Build<CarCreateDto>()
            .With(c => c.DriverId, 1)
            .With(c => c.RegisteredUntil, DateTime.UtcNow)
            .Create();

        JsonContent carPostContent = JsonContent.Create(carCreateDto);
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        JsonContent employeePostContent = JsonContent.Create(employeeCreateDto);

        return new Tuple<JsonContent, JsonContent, JsonContent>(branchPostContent, employeePostContent, carPostContent);
    }
}