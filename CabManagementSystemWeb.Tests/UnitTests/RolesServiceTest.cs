using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class RolesServiceTest
{
    private readonly int _id = 1;

    private readonly IRolesService _rolesService;

    private readonly Mock<IRepository<Role, RoleCreateDto, RoleDetailDto>> _rolesRepositoryMock;

    private readonly IFixture _fixture;

    public RolesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _rolesRepositoryMock = new Mock<IRepository<Role, RoleCreateDto, RoleDetailDto>>();
        _rolesService = new RolesService(_rolesRepositoryMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        RoleDetailDto roleDetailDto = _fixture.Create<RoleDetailDto>();
        var expectedResult = new List<RoleDetailDto>() {roleDetailDto};

        _rolesRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(expectedResult);

        var result = await _rolesService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedRole()
    {
        var expectedResult = _fixture.Create<RoleDetailDto>();
        expectedResult.Id = _id;

        _rolesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _rolesService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedRole()
    {
        Func<Task> act = () => _rolesService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedRole()
    {
        var expectedResult = _fixture.Create<RoleDetailDto>();
        RoleCreateDto roleCreateDto = _fixture.Build<RoleCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _rolesRepositoryMock.Setup(e => e.Create(It.IsAny<RoleCreateDto>())).ReturnsAsync(expectedResult);

        var result = await _rolesService.Create(roleCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedRole()
    {
        var expectedResult = _fixture.Create<RoleDetailDto>();
        RoleUpdateDto roleUpdateDto = _fixture.Create<RoleUpdateDto>();
        expectedResult.Id = _id;

        _rolesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _rolesRepositoryMock.Setup(e => e.Update(It.IsAny<Role>())).ReturnsAsync(expectedResult);

        var result = await _rolesService.Update(It.IsAny<int>(), roleUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenRoleNotFound()
    {
        RoleUpdateDto roleUpdateDto = _fixture.Create<RoleUpdateDto>();

        Func<Task> act = () => _rolesService.Update(It.IsAny<int>(), roleUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        RoleDetailDto roleDetailDto = _fixture.Create<RoleDetailDto>();
        roleDetailDto.Id = _id;

        _rolesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(roleDetailDto);
        _rolesRepositoryMock.Setup(e => e.Delete(It.IsAny<Role>())).ReturnsAsync(roleDetailDto);

        var result = await _rolesService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenRoleNotFound()
    {
        Func<Task> act = () => _rolesService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}