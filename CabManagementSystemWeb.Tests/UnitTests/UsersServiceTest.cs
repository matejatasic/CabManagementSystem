using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class UsersServiceTest
{
    private readonly int _id = 1;

    private readonly IUsersService _usersService;
    private readonly Mock<IRepository<User>> _usersRepositoryMock;
    private readonly Mock<IRepository<Role>> _rolesRepositoryMock;
    private readonly Mock<IHashService> _hashServiceMock;

    private readonly IFixture _fixture;

    public UsersServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _usersRepositoryMock = new Mock<IRepository<User>>();
        _rolesRepositoryMock = new Mock<IRepository<Role>>();
        _hashServiceMock = new Mock<IHashService>();
        _usersService = new UsersService(
            _usersRepositoryMock.Object,
            _rolesRepositoryMock.Object,
            _hashServiceMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        User User = _fixture.Create<User>();
        var expectedResult = new List<User>() {User};

        _usersRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(expectedResult);

        var result = await _usersService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedUser()
    {
        var expectedResult = _fixture.Create<User>();
        expectedResult.Id = _id;

        _usersRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _usersService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedUser()
    {
        Func<Task> act = () => _usersService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedUser()
    {
        var expectedResult = _fixture.Create<User>();
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        expectedResult.Id = _id;
        Role role = _fixture.Build<Role>()
            .Create();

        _rolesRepositoryMock.Setup(r => r.GetById(userCreateDto.RoleId)).ReturnsAsync(role);
        _usersRepositoryMock.Setup(e => e.Create(It.IsAny<User>())).ReturnsAsync(expectedResult);

        var result = await _usersService.Create(userCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenUsernameExists()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        User User = _fixture.Build<User>()
            .Create();
        _usersRepositoryMock
            .Setup(u => u.GetBy("username", userCreateDto.Username))
            .ReturnsAsync(User);
        Func<Task> act = () => _usersService.Create(userCreateDto);

        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenEmailExists()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        User User = _fixture.Build<User>()
            .Create();
        _usersRepositoryMock
            .Setup(u => u.GetBy("email", userCreateDto.Email))
            .ReturnsAsync(User);
        Func<Task> act = () => _usersService.Create(userCreateDto);

        await Assert.ThrowsAsync<ArgumentException>(act);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenNotRetrievedRole()
    {
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        Func<Task> act = () => _usersService.Create(userCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedUser()
    {
        var expectedResult = _fixture.Create<User>();
        UserUpdateDto userUpdateDto = _fixture.Create<UserUpdateDto>();
        expectedResult.Id = _id;
        Role role = _fixture.Build<Role>()
            .Create();

        _rolesRepositoryMock.Setup(r => r.GetById((int)userUpdateDto.RoleId)).ReturnsAsync(role);

        _usersRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _usersRepositoryMock.Setup(e => e.Update(It.IsAny<User>())).ReturnsAsync(expectedResult);

        var result = await _usersService.Update(It.IsAny<int>(), userUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenUserNotFound()
    {
        UserUpdateDto userUpdateDto = _fixture.Create<UserUpdateDto>();

        Func<Task> act = () => _usersService.Update(It.IsAny<int>(), userUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenNotRetrievedRole()
    {
        UserUpdateDto userUpdateDto = _fixture.Build<UserUpdateDto>()
            .Create();
        User User = _fixture.Build<User>().Create();
        _usersRepositoryMock.Setup(u => u.GetById(1)).ReturnsAsync(User);
        Func<Task> act = () => _usersService.Update(1, userUpdateDto);

        var exception = await Assert.ThrowsAsync<NotFoundException>(act);
        Assert.Contains("role", exception.Message);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        User User = _fixture.Create<User>();
        User.Id = _id;

        _usersRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(User);
        _usersRepositoryMock.Setup(e => e.Delete(It.IsAny<User>())).ReturnsAsync(User);

        var result = await _usersService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenUserNotFound()
    {
        Func<Task> act = () => _usersService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}