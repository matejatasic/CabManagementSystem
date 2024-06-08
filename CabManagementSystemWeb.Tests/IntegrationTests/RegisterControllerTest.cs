using System.Net.Http.Json;
using System.Net;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class RegisterControllerTest : BaseIntegrationTest
{
    private string _registerRoute = "/register";
    private string _userRoute = "/users";


    private string _registerRouteUrl;
    private string _userRouteUrl;

    public RegisterControllerTest()
    {
        _registerRouteUrl = _routePrefix + _registerRoute;
        _userRouteUrl = _routePrefix + _userRoute;
    }

    [Fact]
    public async void TestRegisterReturnsAppropriateResultWhenRegisterSuccessfully()
    {
        await InitializeClient();

        // UserCreateDto userCreateDto = await CreateUser();

        RegisterDto registerDto = _fixture.Build<RegisterDto>().Create();
        // registerDto.Username = userCreateDto.Username;
        // registerDto.Password = userCreateDto.Password;
        JsonContent registerPostContent = JsonContent.Create(registerDto);

        var response = await _client.PostAsync($"{_registerRouteUrl}", registerPostContent);
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async void TestRegisterReturnsBadRequestWhenUserExists()
    {
        await InitializeClient();

        UserCreateDto userCreateDto = await CreateUser();

        RegisterDto registerDto = _fixture.Build<RegisterDto>().Create();
        registerDto.Username = userCreateDto.Username;
        registerDto.Password = userCreateDto.Password;
        JsonContent registerPostContent = JsonContent.Create(registerDto);

        var response = await _client.PostAsync($"{_registerRouteUrl}", registerPostContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<UserCreateDto> CreateUser()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>().Create();
        JsonContent userPostContent = JsonContent.Create(userCreateDto);
        await _client.PostAsync($"{_userRouteUrl}", userPostContent);

        return userCreateDto;
    }
}