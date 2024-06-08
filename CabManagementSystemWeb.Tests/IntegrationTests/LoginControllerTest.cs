using System.Net;
using System.Net.Http.Json;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class LoginControllerTest : BaseIntegrationTest
{
    private string _loginRoute = "/login";
    private string _userRoute = "/users";


    private string _loginRouteUrl;
    private string _userRouteUrl;

    public LoginControllerTest() : base()
    {
        _loginRouteUrl = _routePrefix + _loginRoute;
        _userRouteUrl = _routePrefix + _userRoute;
    }

    [Fact]
    public async void TestLoginReturnsAppropriateResultWhenLoginSuccessfully()
    {
        await InitializeClient();

        UserCreateDto userCreateDto = await CreateUser();

        LoginDto loginDto = _fixture.Build<LoginDto>().Create();
        loginDto.Username = userCreateDto.Username;
        loginDto.Password = userCreateDto.Password;
        JsonContent loginPostContent = JsonContent.Create(loginDto);

        var response = await _client.PostAsync($"{_loginRouteUrl}", loginPostContent);
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }

    [Fact]
    public async void TestLoginReturnsBadRequestStatusCodeWhenUserNotExists()
    {
        await InitializeClient();

        LoginDto loginDto = _fixture.Build<LoginDto>().Create();
        JsonContent loginPostContent = JsonContent.Create(loginDto);

        var response = await _client.PostAsync($"{_loginRouteUrl}", loginPostContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async void TestLoginReturnsBadRequestStatusCodeWhenPasswordDoNotMatch()
    {
        await InitializeClient();

        UserCreateDto userCreateDto = await CreateUser();

        LoginDto loginDto = _fixture.Build<LoginDto>().Create();
        loginDto.Username = userCreateDto.Username;
        loginDto.Password = "invalid password";
        JsonContent loginPostContent = JsonContent.Create(loginDto);

        var response = await _client.PostAsync($"{_loginRouteUrl}", loginPostContent);

        Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);
    }

    private async Task<UserCreateDto> CreateUser()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>().Create();
        JsonContent userPostContent = JsonContent.Create(userCreateDto);
        await _client.PostAsync($"{_userRouteUrl}", userPostContent);

        return userCreateDto;
    }
}