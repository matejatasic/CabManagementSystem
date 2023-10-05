using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class ClientsControllerTest : BaseIntegrationTest
{
    private string _clientRoute = "/clients";
    private string _clientRouteUrl;

    public ClientsControllerTest() : base()
    {
        _clientRouteUrl = _routePrefix + _clientRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesAnClient()
    {
        await InitializeClient();

        ClientCreateDto clientCreateDto = _fixture.Build<ClientCreateDto>()
            .Create();
        JsonContent clientPostContent = JsonContent.Create(clientCreateDto);

        var response = await _client.PostAsync($"{_clientRouteUrl}", clientPostContent);
        var content = await response.Content.ReadAsStringAsync();

        ClientDetailDto deserializedContent = JsonSerializer.Deserialize<ClientDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<ClientDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfClients()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_clientRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<ClientDetailDto> deserializedContent = JsonSerializer.Deserialize<List<ClientDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsAnClient()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.GetAsync($"{_clientRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        ClientDetailDto deserializedContent = JsonSerializer.Deserialize<ClientDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<ClientDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesAnClient()
    {
        await InitializeClient();

        await CreateNeededEntities();
        string updatedUsername = "UpdatedUsername";
        ClientUpdateDto clientUpdateDto = _fixture.Build<ClientUpdateDto>()
            .With(e => e.Username, updatedUsername)
            .Create();
        JsonContent clientPutContent = JsonContent.Create(clientUpdateDto);

        var response = await _client.PutAsync($"{_clientRouteUrl}/1", clientPutContent);
        var getByIdResponse = _client.GetAsync($"{_clientRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        ClientDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<ClientDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedUsername, getByIdDeserializedContent.Username);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesAnClient()
    {
        await InitializeClient();

        await CreateNeededEntities();

        var response = _client.DeleteAsync($"{_clientRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_clientRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<ClientDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<ClientDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }

    private async Task CreateNeededEntities()
    {
        JsonContent clientPostContent = GetPostContent();

        await _client.PostAsync($"{_clientRouteUrl}", clientPostContent);
    }

    private JsonContent GetPostContent()
    {
        ClientCreateDto clientCreateDto = _fixture.Build<ClientCreateDto>()
            .Create();
        JsonContent clientPostContent = JsonContent.Create(clientCreateDto);

        return clientPostContent;
    }
}
