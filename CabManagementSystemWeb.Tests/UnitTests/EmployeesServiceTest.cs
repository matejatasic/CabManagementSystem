using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class EmployeesServiceTest
{
    private readonly int _id = 1;

    private readonly IEmployeesService _employeesService;

    private readonly Mock<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>> _employeesRepositoryMock;
    private readonly Mock<IRepository<Branch, BranchCreateDto, BranchDetailDto>> _branchesRepositoryMock;
    private readonly Mock<IHashService> _hashServiceMock;

    private readonly IFixture _fixture;

    public EmployeesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _employeesRepositoryMock = new Mock<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>>();
        _branchesRepositoryMock = new Mock<IRepository<Branch, BranchCreateDto, BranchDetailDto>>();
        _hashServiceMock = new Mock<IHashService>();
        _employeesService = new EmployeesService(_employeesRepositoryMock.Object, _branchesRepositoryMock.Object, _hashServiceMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        EmployeeDetailDto employeeDetailDto = _fixture.Create<EmployeeDetailDto>();
        var expectedResult = new List<EmployeeDetailDto>() {employeeDetailDto};

        _employeesRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(expectedResult);

        var result = await _employeesService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedEmployee()
    {
        var expectedResult = _fixture.Create<EmployeeDetailDto>();
        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _employeesService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedEmployee()
    {
        Func<Task> act = () => _employeesService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedEmployee()
    {
        var expectedResult = _fixture.Create<EmployeeDetailDto>();
        var expectedBranchResult = _fixture.Create<BranchDetailDto>();
        EmployeeCreateDto employeeCreateDto = _fixture.Build<EmployeeCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedBranchResult);
        _employeesRepositoryMock.Setup(e => e.Create(It.IsAny<EmployeeCreateDto>())).ReturnsAsync(expectedResult);

        var result = await _employeesService.Create(employeeCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenBranchNotFound()
    {
        EmployeeCreateDto employeeCreateDto = _fixture.Create<EmployeeCreateDto>();

        Func<Task> act = () => _employeesService.Create(employeeCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedEmployee()
    {
        var expectedResult = _fixture.Create<EmployeeDetailDto>();
        BranchDetailDto branchDetailDto = _fixture.Create<BranchDetailDto>();
        EmployeeUpdateDto employeeUpdateDto = _fixture.Create<EmployeeUpdateDto>();
        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(branchDetailDto);
        _employeesRepositoryMock.Setup(e => e.Update(It.IsAny<Employee>())).ReturnsAsync(expectedResult);

        var result = await _employeesService.Update(It.IsAny<int>(), employeeUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenEmployeeNotFound()
    {
        EmployeeUpdateDto employeeUpdateDto = _fixture.Create<EmployeeUpdateDto>();

        Func<Task> act = () => _employeesService.Update(It.IsAny<int>(), employeeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenBranchNotFound()
    {
        EmployeeDetailDto employeeDetailDto = _fixture.Create<EmployeeDetailDto>();
        EmployeeUpdateDto employeeUpdateDto = _fixture.Create<EmployeeUpdateDto>();

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employeeDetailDto);
        Func<Task> act = () => _employeesService.Update(It.IsAny<int>(), employeeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        EmployeeDetailDto employeeDetailDto = _fixture.Create<EmployeeDetailDto>();
        employeeDetailDto.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employeeDetailDto);
        _employeesRepositoryMock.Setup(e => e.Delete(It.IsAny<Employee>())).ReturnsAsync(employeeDetailDto);

        var result = await _employeesService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenEmployeeNotFound()
    {
        Func<Task> act = () => _employeesService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}