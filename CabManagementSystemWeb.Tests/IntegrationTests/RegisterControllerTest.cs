using System.Net.Http.Json;
using System.Net;
using AutoFixture;

using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Tests.Controllers;

public class RegisterControllerTest : BaseIntegrationTest
{
    private string _registerRoute = "/register";
    private string _roleRoute = "/roles";
    private string _userRoute = "/users";


    private string _registerRouteUrl;
    private string _roleRouteUrl;
    private string _userRouteUrl;

    public RegisterControllerTest()
    {
        _registerRouteUrl = _routePrefix + _registerRoute;
        _roleRouteUrl = _routePrefix + _roleRoute;
        _userRouteUrl = _routePrefix + _userRoute;
    }

    [Fact]
    public async void TestRegisterReturnsAppropriateResultWhenRegisterSuccessfully()
    {
        await InitializeClient();
        await CreateUserRole();

        RegisterDto registerDto = _fixture.Build<RegisterDto>().Create();
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

        UserCreateDto userCreateDto = await CreateNeededEntities();
        await CreateUserRole();

        RegisterDto registerDto = _fixture.Build<RegisterDto>().Create();
        registerDto.Username = userCreateDto.Username;
        registerDto.Password = userCreateDto.Password;
        JsonContent registerPostContent = JsonContent.Create(registerDto);

        var response = await _client.PostAsync($"{_registerRouteUrl}", registerPostContent);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private async Task<UserCreateDto> CreateNeededEntities()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .With(u => u.RoleId, 1)
            .Create();
        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();

        JsonContent userPostContent = JsonContent.Create(userCreateDto);
        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);

        var response1 = await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);
        var response2 =  await _client.PostAsync($"{_userRouteUrl}", userPostContent);

        return userCreateDto;
    }

    private async Task CreateUserRole()
    {
        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>().Create();
        roleCreateDto.Name = "User";

        JsonContent rolePostContent = JsonContent.Create(roleCreateDto);

        await _client.PostAsync($"{_roleRouteUrl}", rolePostContent);
    }
}