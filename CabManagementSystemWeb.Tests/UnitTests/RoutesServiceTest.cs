using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class RoutesServiceTest
{
    private readonly int _id = 1;

    private readonly IRoutesService _routesService;

    private readonly Mock<IRepository<Route>> _routesRepositoryMock;
    private readonly Mock<IRepository<Employee>> _employeesRepositoryMock;
    private readonly Mock<IRepository<User>> _usersRepositoryMock;

    private readonly IFixture _fixture;

    public RoutesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _routesRepositoryMock = new Mock<IRepository<Route>>();
        _employeesRepositoryMock = new Mock<IRepository<Employee>>();
        _usersRepositoryMock = new Mock<IRepository<User>>();
        _routesService = new RoutesService(
            _routesRepositoryMock.Object,
            _employeesRepositoryMock.Object,
            _usersRepositoryMock.Object
        );
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        Route route = _fixture.Create<Route>();
        var expectedResult = new List<Route>() {route};

        _routesRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(expectedResult);

        var result = await _routesService.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedRoute()
    {
        var expectedResult = _fixture.Create<Route>();
        expectedResult.Id = _id;

        _routesRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _routesService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedRoute()
    {
        Func<Task> act = () => _routesService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedRoute()
    {
        var expectedResult = _fixture.Create<Route>();
        var expectedEmployeeResult = _fixture.Create<Employee>();
        var expectedUserResult = _fixture.Create<User>();
        RouteCreateDto routeCreateDto = _fixture.Build<RouteCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedEmployeeResult);
        _usersRepositoryMock.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(expectedUserResult);
        _routesRepositoryMock.Setup(r => r.Create(It.IsAny<Route>())).ReturnsAsync(expectedResult);

        var result = await _routesService.Create(routeCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenDriverNotFound()
    {
        RouteCreateDto routeCreateDto = _fixture.Create<RouteCreateDto>();

        Func<Task> act = () => _routesService.Create(routeCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenTravelerNotFound()
    {
        RouteCreateDto routeCreateDto = _fixture.Create<RouteCreateDto>();
        Employee employee = _fixture.Create<Employee>();

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);

        Func<Task> act = () => _routesService.Create(routeCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedRoute()
    {
        var expectedResult = _fixture.Create<Route>();
        Employee employee = _fixture.Create<Employee>();
        User user = _fixture.Create<User>();
        RouteUpdateDto routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        expectedResult.Id = _id;

        _routesRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);
        _usersRepositoryMock.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(user);
        _routesRepositoryMock.Setup(r => r.Update(It.IsAny<Route>())).ReturnsAsync(expectedResult);

        var result = await _routesService.Update(It.IsAny<int>(), routeUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionRouteNotFound()
    {
        RouteUpdateDto routeUpdateDto = _fixture.Create<RouteUpdateDto>();

        Func<Task> act = () => _routesService.Update(It.IsAny<int>(), routeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenTravelerNotFound()
    {
        RouteUpdateDto routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        Employee employee = _fixture.Create<Employee>();

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);

        Func<Task> act = () => _routesService.Update(It.IsAny<int>(), routeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenBranchNotFound()
    {
        Route route = _fixture.Create<Route>();
        RouteUpdateDto routeUpdateDto = _fixture.Create<RouteUpdateDto>();

        _routesRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(route);
        Func<Task> act = () => _routesService.Update(It.IsAny<int>(), routeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        Route route = _fixture.Create<Route>();
        route.Id = _id;

        _routesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(route);
        _routesRepositoryMock.Setup(e => e.Delete(It.IsAny<Route>())).ReturnsAsync(route);

        var result = await _routesService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenRouteNotFound()
    {
        Func<Task> act = () => _routesService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}