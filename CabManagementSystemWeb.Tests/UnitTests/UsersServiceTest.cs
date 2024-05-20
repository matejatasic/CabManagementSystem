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

    private readonly Mock<IRepository<User, UserCreateDto, UserDetailDto>> _usersRepositoryMock;
    private readonly Mock<IHashService> _hashServiceMock;

    private readonly IFixture _fixture;

    public UsersServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _usersRepositoryMock = new Mock<IRepository<User, UserCreateDto, UserDetailDto>>();
        _hashServiceMock = new Mock<IHashService>();
        _usersService = new UsersService(_usersRepositoryMock.Object, _hashServiceMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        UserDetailDto userDetailDto = _fixture.Create<UserDetailDto>();
        var expectedResult = new List<UserDetailDto>() {userDetailDto};

        _usersRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(expectedResult);

        var result = await _usersService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedUser()
    {
        var expectedResult = _fixture.Create<UserDetailDto>();
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
        var expectedResult = _fixture.Create<UserDetailDto>();
        UserCreateDto userCreateDto = _fixture.Build<UserCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _usersRepositoryMock.Setup(e => e.Create(It.IsAny<UserCreateDto>())).ReturnsAsync(expectedResult);

        var result = await _usersService.Create(userCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedUser()
    {
        var expectedResult = _fixture.Create<UserDetailDto>();
        UserUpdateDto userUpdateDto = _fixture.Create<UserUpdateDto>();
        expectedResult.Id = _id;

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
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        UserDetailDto userDetailDto = _fixture.Create<UserDetailDto>();
        userDetailDto.Id = _id;

        _usersRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(userDetailDto);
        _usersRepositoryMock.Setup(e => e.Delete(It.IsAny<User>())).ReturnsAsync(userDetailDto);

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