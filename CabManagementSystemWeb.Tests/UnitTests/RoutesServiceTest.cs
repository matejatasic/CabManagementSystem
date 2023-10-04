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

    private readonly Mock<IRepository<Route, RouteCreateDto, RouteDetailDto>> _routesRepositoryMock;
    private readonly Mock<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>> _employeesRepositoryMock;

    private readonly IFixture _fixture;

    public RoutesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _routesRepositoryMock = new Mock<IRepository<Route, RouteCreateDto, RouteDetailDto>>();
        _employeesRepositoryMock = new Mock<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>>();
        _routesService = new RoutesService(_routesRepositoryMock.Object, _employeesRepositoryMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        RouteDetailDto routeDetailDto = _fixture.Create<RouteDetailDto>();
        var expectedResult = new List<RouteDetailDto>() {routeDetailDto};

        _routesRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(expectedResult);

        var result = await _routesService.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedRoute()
    {
        var expectedResult = _fixture.Create<RouteDetailDto>();
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
        var expectedResult = _fixture.Create<RouteDetailDto>();
        var expectedEmployeeResult = _fixture.Create<EmployeeDetailDto>();
        RouteCreateDto routeCreateDto = _fixture.Build<RouteCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedEmployeeResult);
        _routesRepositoryMock.Setup(r => r.Create(It.IsAny<RouteCreateDto>())).ReturnsAsync(expectedResult);

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
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedRoute()
    {
        var expectedResult = _fixture.Create<RouteDetailDto>();
        EmployeeDetailDto employeeDetailDto = _fixture.Create<EmployeeDetailDto>();
        RouteUpdateDto routeUpdateDto = _fixture.Create<RouteUpdateDto>();
        expectedResult.Id = _id;

        _routesRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employeeDetailDto);
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
    public async void TestUpdateThrowingExceptionWhenBranchNotFound()
    {
        RouteDetailDto routeDetailDto = _fixture.Create<RouteDetailDto>();
        RouteUpdateDto routeUpdateDto = _fixture.Create<RouteUpdateDto>();

        _routesRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync(routeDetailDto);
        Func<Task> act = () => _routesService.Update(It.IsAny<int>(), routeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        RouteDetailDto routeDetailDto = _fixture.Create<RouteDetailDto>();
        routeDetailDto.Id = _id;

        _routesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(routeDetailDto);
        _routesRepositoryMock.Setup(e => e.Delete(It.IsAny<Route>())).ReturnsAsync(routeDetailDto);

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