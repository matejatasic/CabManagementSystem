using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class CarsServiceTest
{
    private readonly int _id = 1;

    private readonly ICarsService _carsService;

    private readonly Mock<IRepository<Car>> _carsRepositoryMock;
    private readonly Mock<IRepository<Employee>> _employeesRepositoryMock;

    private readonly IFixture _fixture;

    public CarsServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _carsRepositoryMock = new Mock<IRepository<Car>>();
        _employeesRepositoryMock = new Mock<IRepository<Employee>>();
        _carsService = new CarsService(_carsRepositoryMock.Object, _employeesRepositoryMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        Car car = _fixture.Create<Car>();
        var expectedResult = new List<Car>() {car};

        _carsRepositoryMock.Setup(c => c.GetAll()).ReturnsAsync(expectedResult);

        var result = await _carsService.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedCar()
    {
        var expectedResult = _fixture.Create<Car>();
        expectedResult.Id = _id;

        _carsRepositoryMock.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _carsService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedCar()
    {
        Func<Task> act = () => _carsService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedCar()
    {
        var expectedResult = _fixture.Create<Car>();
        var expectedEmployeeResult = _fixture.Create<Employee>();
        CarCreateDto carCreateDto = _fixture.Build<CarCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedEmployeeResult);
        _carsRepositoryMock.Setup(c => c.Create(It.IsAny<Car>())).ReturnsAsync(expectedResult);

        var result = await _carsService.Create(carCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenDriverNotFound()
    {
        CarCreateDto carCreateDto = _fixture.Create<CarCreateDto>();

        Func<Task> act = () => _carsService.Create(carCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedCar()
    {
        var expectedResult = _fixture.Create<Car>();
        Employee employee = _fixture.Create<Employee>();
        CarUpdateDto carUpdateDto = _fixture.Create<CarUpdateDto>();
        expectedResult.Id = _id;

        _carsRepositoryMock.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);
        _carsRepositoryMock.Setup(c => c.Update(It.IsAny<Car>())).ReturnsAsync(expectedResult);

        var result = await _carsService.Update(It.IsAny<int>(), carUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionCarNotFound()
    {
        CarUpdateDto carUpdateDto = _fixture.Create<CarUpdateDto>();

        Func<Task> act = () => _carsService.Update(It.IsAny<int>(), carUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenBranchNotFound()
    {
        Car car = _fixture.Create<Car>();
        CarUpdateDto carUpdateDto = _fixture.Create<CarUpdateDto>();

        _carsRepositoryMock.Setup(c => c.GetById(It.IsAny<int>())).ReturnsAsync(car);
        Func<Task> act = () => _carsService.Update(It.IsAny<int>(), carUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        Car car = _fixture.Create<Car>();
        car.Id = _id;

        _carsRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(car);
        _carsRepositoryMock.Setup(e => e.Delete(It.IsAny<Car>())).ReturnsAsync(car);

        var result = await _carsService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenCarNotFound()
    {
        Func<Task> act = () => _carsService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}