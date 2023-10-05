using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class EmployeesControllerTest : BaseIntegrationTest
{
    private string _employeeRoute = "/employees";
    private string _branchRoute = "/branches";
    private string _employeeRouteUrl;
    private string _branchRouteUrl;

    public EmployeesControllerTest() : base()
    {
        _employeeRouteUrl = _routePrefix + _employeeRoute;
        _branchRouteUrl = _routePrefix + _branchRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesAnEmployee()
    {
        await InitializeClient();

        var (branchPostContent, employeePostContent) = GetPostContent();

        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);

        var response = await _client.PostAsync($"{_employeeRouteUrl}", employeePostContent);
        var content = await response.Content.ReadAsStringAsync();

        EmployeeDetailDto deserializedContent = JsonSerializer.Deserialize<EmployeeDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<EmployeeDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfEmployees()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_employeeRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<EmployeeDetailDto> deserializedContent = JsonSerializer.Deserialize<List<EmployeeDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsAnEmployee()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_employeeRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        EmployeeDetailDto deserializedContent = JsonSerializer.Deserialize<EmployeeDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<EmployeeDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesAnEmployee()
    {
        await InitializeClient();

        await CreateNeededEntities();
        string updatedUsername = "UpdatedUsername";
        EmployeeUpdateDto employeeUpdateDto = _fixture.Build<EmployeeUpdateDto>()
            .With(e => e.Username, updatedUsername)
            .With(e => e.BranchId, 1)
            .Create();
        JsonContent employeePutContent = JsonContent.Create(employeeUpdateDto);

        var response = await _client.PutAsync($"{_employeeRouteUrl}/1", employeePutContent);
        var getByIdResponse = _client.GetAsync($"{_employeeRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        EmployeeDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<EmployeeDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedUsername, getByIdDeserializedContent.Username);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesAnEmployee()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.DeleteAsync($"{_employeeRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_employeeRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<EmployeeDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<EmployeeDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }

    private async Task CreateNeededEntities()
    {
        var (branchPostContent, employeePostContent) = GetPostContent();

        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);
        await _client.PostAsync($"{_employeeRouteUrl}", employeePostContent);
    }

    private Tuple<JsonContent, JsonContent> GetPostContent()
    {
        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        EmployeeCreateDto employeeCreateDto = _fixture.Build<EmployeeCreateDto>()
            .With(e => e.BranchId, 1).Create();
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        JsonContent employeePostContent = JsonContent.Create(employeeCreateDto);

        return new Tuple<JsonContent, JsonContent>(branchPostContent, employeePostContent);
    }
}
