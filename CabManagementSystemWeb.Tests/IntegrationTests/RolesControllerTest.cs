using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class RolesControllerTest : BaseIntegrationTest
{
    private string _roleRoute = "/roles";
    private string _roleRouteUrl;

    public RolesControllerTest() : base()
    {
        _roleRouteUrl = _routePrefix + _roleRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesARole()
    {
        await InitializeClient();

        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);

        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);

        var response = await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);
        var content = await response.Content.ReadAsStringAsync();
        RoleDetailDto deserializedContent = JsonSerializer.Deserialize<RoleDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<RoleDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfRolees()
    {
        await InitializeClient();

        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);
        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);

        var response = _client.GetAsync($"{_roleRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<RoleDetailDto> deserializedContent = JsonSerializer.Deserialize<List<RoleDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsARole()
    {
        await InitializeClient();

        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);
        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);

        var response = _client.GetAsync($"{_roleRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        RoleDetailDto deserializedContent = JsonSerializer.Deserialize<RoleDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<RoleDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesARole()
    {
        await InitializeClient();

        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);
        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);

        string updatedName = "UpdatedName";
        RoleUpdateDto roleUpdateDto = _fixture.Build<RoleUpdateDto>()
            .With(e => e.Name, updatedName)
            .Create();
        JsonContent employeePutContent = JsonContent.Create(roleUpdateDto);

        var response = await _client.PutAsync($"{_roleRouteUrl}/1", employeePutContent);
        var getByIdResponse = _client.GetAsync($"{_roleRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        RoleDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<RoleDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedName, getByIdDeserializedContent.Name);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesAnRole()
    {
        await InitializeClient();

        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);
        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);

        var response = _client.DeleteAsync($"{_roleRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_roleRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<RoleDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<RoleDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }
}
