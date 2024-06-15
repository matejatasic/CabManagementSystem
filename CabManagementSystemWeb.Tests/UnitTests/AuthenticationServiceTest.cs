using AuthenticationApi.Services;
using AutoFixture;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Services;
using Moq;

namespace CabManagementSystemWeb.Tests.Services;

public class AuthenticationServiceTest
{
    private readonly IAuthenticationService _authenticationService;

    private Mock<IRepository<User, UserCreateDto, UserDetailDto>> _usersRepositoryMock;
    private Mock<IRepository<Role, RoleCreateDto, RoleDetailDto>> _rolesRepositoryMock;
    private Mock<IUsersService> _usersServiceMock;
    private Mock<IHashService> _hashServiceMock;
    private Mock<IJwtProviderService> _jwtProviderServiceMock;
    private readonly IFixture _fixture;

    public AuthenticationServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _usersRepositoryMock = new Mock<IRepository<User, UserCreateDto, UserDetailDto>>();
        _rolesRepositoryMock = new Mock<IRepository<Role, RoleCreateDto, RoleDetailDto>>();
        _usersServiceMock = new Mock<IUsersService>();
        _hashServiceMock = new Mock<IHashService>();
        _jwtProviderServiceMock = new Mock<IJwtProviderService>();

        _authenticationService = new AuthenticationService(
            _usersRepositoryMock.Object,
            _rolesRepositoryMock.Object,
            _usersServiceMock.Object,
            _hashServiceMock.Object,
            _jwtProviderServiceMock.Object
        );
    }

    [Fact]
    public async void TestLoginReturningTokenWhenSuccessfullyLogin()
    {
        LoginDto loginDto = _fixture.Create<LoginDto>();
        UserDetailDto userDetailDto = _fixture.Create<UserDetailDto>();

        _usersRepositoryMock
            .Setup(u => u.GetBy("username", loginDto.Username))
            .ReturnsAsync(userDetailDto);
        _hashServiceMock
            .Setup(h => h.Verify(loginDto.Password, userDetailDto.Password))
            .Returns(true);
        _jwtProviderServiceMock
            .Setup(j => j.Generate(userDetailDto.Id.ToString(), userDetailDto.Email))
            .Returns("some token");

        string result = await _authenticationService.Login(loginDto);

        Assert.NotNull(result);
    }

    [Fact]
    public async void TestLoginThrowingErrorWhenUserNotExists()
    {
        LoginDto loginDto = _fixture.Create<LoginDto>();

        Func<Task> act = () => _authenticationService.Login(loginDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestLoginThrowingErrorWhenPasswordsDoNotMatch()
    {
        LoginDto loginDto = _fixture.Create<LoginDto>();
        UserDetailDto userDetailDto = _fixture.Create<UserDetailDto>();

        _usersRepositoryMock
            .Setup(u => u.GetBy("username", loginDto.Username))
            .ReturnsAsync(userDetailDto);

        Func<Task> act = () => _authenticationService.Login(loginDto);

        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async void TestRegisterReturningTokenWhenRegisterSuccessfully()
    {
        Mock<RegisterDto> registerDto = new Mock<RegisterDto>();
        UserCreateDto userCreateDto = _fixture.Create<UserCreateDto>();
        RoleDetailDto roleDetailDto = _fixture.Create<RoleDetailDto>();
        LoginDto loginDto = _fixture.Create<LoginDto>();
        UserDetailDto userDetailDto = _fixture.Create<UserDetailDto>();

        _usersServiceMock
            .Setup(u => u.Create(userCreateDto))
            .ReturnsAsync(_fixture.Create<UserDetailDto>());
        _rolesRepositoryMock
            .Setup(r => r.GetBy("name", "User"))
            .ReturnsAsync(roleDetailDto);
        registerDto
            .Setup(r => r.ConvertToLoginDto())
            .Returns(loginDto);
        _usersRepositoryMock
            .Setup(u => u.GetBy("username", loginDto.Username))
            .ReturnsAsync(userDetailDto);
        _hashServiceMock
            .Setup(h => h.Verify(loginDto.Password, userDetailDto.Password))
            .Returns(true);
        _jwtProviderServiceMock
            .Setup(j => j.Generate(userDetailDto.Id.ToString(), userDetailDto.Email))
            .Returns("some token");

        string result = await _authenticationService.Register(registerDto.Object);

        Assert.NotNull(result);
    }
}