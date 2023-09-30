using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class BranchesControllerTest : BaseIntegrationTest
{
    private string _branchRoute = "/branches";
    private string _branchRouteUrl;

    public BranchesControllerTest() : base()
    {
        _branchRouteUrl = _routePrefix + _branchRoute;
    }

    [Fact]
    public async Task TestCreateSuccessfullyCreatesABranch()
    {
        await InitializeClient();

        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);

        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);

        var response = await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);
        var content = await response.Content.ReadAsStringAsync();
        BranchDetailDto deserializedContent = JsonSerializer.Deserialize<BranchDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<BranchDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestGetAllReturnsAListOfBranches()
    {
        await InitializeClient();

        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);

        var response = _client.GetAsync($"{_branchRouteUrl}").Result;
        var content = await response.Content.ReadAsStringAsync();

        List<BranchDetailDto> deserializedContent = JsonSerializer.Deserialize<List<BranchDetailDto>>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(deserializedContent);
    }

    [Fact]
    public async Task TestGetByIdReturnsABranch()
    {
        await InitializeClient();

        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);

        var response = _client.GetAsync($"{_branchRouteUrl}/1").Result;
        var content = await response.Content.ReadAsStringAsync();
        BranchDetailDto deserializedContent = JsonSerializer.Deserialize<BranchDetailDto>(content, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.IsType<BranchDetailDto>(deserializedContent);
    }

    [Fact]
    public async Task TestUpdateSuccessfullyUpdatesABranch()
    {
        await InitializeClient();

        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);

        string updatedName = "UpdatedName";
        BranchUpdateDto branchUpdateDto = _fixture.Build<BranchUpdateDto>()
            .With(e => e.Name, updatedName)
            .Without(e => e.ManagerId)
            .Create();
        JsonContent employeePutContent = JsonContent.Create(branchUpdateDto);

        var response = await _client.PutAsync($"{_branchRouteUrl}/1", employeePutContent);
        var getByIdResponse = _client.GetAsync($"{_branchRouteUrl}/1").Result;
        string getByIdContent = await getByIdResponse.Content.ReadAsStringAsync();
        BranchDetailDto getByIdDeserializedContent = JsonSerializer
            .Deserialize<BranchDetailDto>(getByIdContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(updatedName, getByIdDeserializedContent.Name);
    }

    [Fact]
    public async Task TestDeleteSuccessfullyDeletesAnBranch()
    {
        await InitializeClient();

        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Without(b => b.ManagerId).Create();
        JsonContent branchPostContent = JsonContent.Create(branchCreateDto);
        await _client.PostAsync($"{_branchRouteUrl}", branchPostContent);

        var response = _client.DeleteAsync($"{_branchRouteUrl}/1").Result;
        var getAllResponse = _client.GetAsync($"{_branchRouteUrl}").Result;
        string getAllContent = await getAllResponse.Content.ReadAsStringAsync();

        List<BranchDetailDto> getAllDeserializedContent = JsonSerializer
            .Deserialize<List<BranchDetailDto>>(getAllContent, _jsonSerializerOptions);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Empty(getAllDeserializedContent);
    }
}
