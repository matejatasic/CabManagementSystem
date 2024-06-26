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
    private string _roleRoute = "/roles";
    private string _userRoute = "/users";

    private string _employeeRouteUrl;
    private string _branchRouteUrl;
    private string _roleRouteUrl;
    private string _userRouteUrl;

    public EmployeesControllerTest() : base()
    {
        _employeeRouteUrl = _routePrefix + _employeeRoute;
        _branchRouteUrl = _routePrefix + _branchRoute;
        _roleRouteUrl = _routePrefix + _roleRoute;
        _userRouteUrl = _routePrefix + _userRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesAnEmployee()
    {
        await InitializeClient();

        var response = await CreateNeededEntities();
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
        await CreateNeededEntities();

        EmployeeUpdateDto employeeUpdateDto = _fixture.Build<EmployeeUpdateDto>()
            .With(e => e.UserId, 2)
            .With(e => e.BranchId, 1)
            .Create();
        JsonContent employeePutContent = JsonContent.Create(employeeUpdateDto);

        var response = await _client.PutAsync($"{_employeeRouteUrl}/1", employeePutContent);

        var getByIdResponse = _client.GetAsync($"{_employeeRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        EmployeeDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<EmployeeDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(2, getByIdDeserializedContent.UserId);
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

    private async Task<HttpResponseMessage> CreateNeededEntities()
    {
        var (branchPostContent, rolePostContent, userPostContent, employeePostContent) = GetPostContent();

        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);
        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);
        await _client.PostAsync($"{_userRouteUrl}", userPostContent);
        var response = await _client.PostAsync($"{_employeeRouteUrl}", employeePostContent);

        return response;
    }

    private Tuple<JsonContent, JsonContent, JsonContent, JsonContent> GetPostContent()
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

        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);
        JsonContent userPostContent = JsonContent.Create(userCreateDto);
        JsonContent employeePostContent = JsonContent.Create(employeeCreateDto);

        return new Tuple<JsonContent, JsonContent, JsonContent, JsonContent>(branchPostContent, rolePostContent, userPostContent, employeePostContent);
    }
}
