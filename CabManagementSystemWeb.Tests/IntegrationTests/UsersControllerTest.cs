using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class UsersControllerTest : BaseIntegrationTest
{
    private string _userRoute = "/users";
    private string _userRouteUrl;

    public UsersControllerTest() : base()
    {
        _userRouteUrl = _routePrefix + _userRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesAUser()
    {
        await InitializeClient();

        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        JsonContent userPostContent = JsonContent.Create(userCreateDto);

        var response = await _client.PostAsync($"{_userRouteUrl}", userPostContent);
        var content = await response.Content.ReadAsStringAsync();

        UserDetailDto deserializedContent = JsonSerializer.Deserialize<UserDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<UserDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfUsers()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_userRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<UserDetailDto> deserializedContent = JsonSerializer.Deserialize<List<UserDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsAUser()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_userRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        UserDetailDto deserializedContent = JsonSerializer.Deserialize<UserDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<UserDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesAUser()
    {
        await InitializeClient();

        await CreateNeededEntities();
        string updatedUsername = "UpdatedUsername";
        UserUpdateDto userUpdateDto = _fixture.Build<UserUpdateDto>()
            .With(e => e.Username, updatedUsername)
            .Create();
        JsonContent userPutContent = JsonContent.Create(userUpdateDto);

        var response = await _client.PutAsync($"{_userRouteUrl}/1", userPutContent);
        var getByIdResponse = _client.GetAsync($"{_userRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        UserDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<UserDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedUsername, getByIdDeserializedContent.Username);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesAUser()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.DeleteAsync($"{_userRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_userRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<UserDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<UserDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }

    private async Task CreateNeededEntities()
    {
        JsonContent userPostContent = GetPostContent();

        await _client.PostAsync($"{_userRouteUrl}", userPostContent);
    }

    private JsonContent GetPostContent()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        JsonContent userPostContent = JsonContent.Create(userCreateDto);

        return userPostContent;
    }
}
