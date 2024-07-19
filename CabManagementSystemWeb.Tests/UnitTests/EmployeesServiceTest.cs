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

    private readonly Mock<IRepository<Employee>> _employeesRepositoryMock;
    private readonly Mock<IRepository<Branch>> _branchesRepositoryMock;
    private readonly Mock<IRepository<User>> _usersRepositoryMock;

    private readonly IFixture _fixture;

    public EmployeesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _employeesRepositoryMock = new Mock<IRepository<Employee>>();
        _branchesRepositoryMock = new Mock<IRepository<Branch>>();
        _usersRepositoryMock = new Mock<IRepository<User>>();
        _employeesService = new EmployeesService(
            _employeesRepositoryMock.Object,
            _branchesRepositoryMock.Object,
            _usersRepositoryMock.Object
        );
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        Employee employee = _fixture.Create<Employee>();
        var expectedResult = new List<Employee>() {employee};

        _employeesRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(expectedResult);

        var result = await _employeesService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedEmployee()
    {
        var expectedResult = _fixture.Create<Employee>();
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
        var expectedResult = _fixture.Create<Employee>();
        var expectedBranchResult = _fixture.Create<Branch>();
        var expectedUserResult = _fixture.Create<User>();
        EmployeeCreateDto employeeCreateDto = _fixture.Build<EmployeeCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedBranchResult);
        _employeesRepositoryMock.Setup(e => e.Create(It.IsAny<Employee>())).ReturnsAsync(expectedResult);
        _usersRepositoryMock.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(expectedUserResult);

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
    public async void TestCreateThrowingExceptionWhenUserNotFound()
    {
        EmployeeCreateDto employeeCreateDto = _fixture.Create<EmployeeCreateDto>();
        var expectedBranchResult = _fixture.Create<Branch>();

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedBranchResult);

        Func<Task> act = () => _employeesService.Create(employeeCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedEmployee()
    {
        var expectedResult = _fixture.Create<Employee>();
        var expectedUserResult = _fixture.Create<User>();
        Branch branch = _fixture.Create<Branch>();
        EmployeeUpdateDto employeeUpdateDto = _fixture.Create<EmployeeUpdateDto>();
        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(branch);
        _employeesRepositoryMock.Setup(e => e.Update(It.IsAny<Employee>())).ReturnsAsync(expectedResult);
        _usersRepositoryMock.Setup(u => u.GetById(It.IsAny<int>())).ReturnsAsync(expectedUserResult);

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
        Employee employee = _fixture.Create<Employee>();
        EmployeeUpdateDto employeeUpdateDto = _fixture.Create<EmployeeUpdateDto>();

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);
        Func<Task> act = () => _employeesService.Update(It.IsAny<int>(), employeeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenUserNotFound()
    {
        Employee employee = _fixture.Create<Employee>();
        EmployeeUpdateDto employeeUpdateDto = _fixture.Create<EmployeeUpdateDto>();
        var expectedBranchResult = _fixture.Create<Branch>();

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedBranchResult);

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);
        Func<Task> act = () => _employeesService.Update(It.IsAny<int>(), employeeUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        Employee employee = _fixture.Create<Employee>();
        employee.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);
        _employeesRepositoryMock.Setup(e => e.Delete(It.IsAny<Employee>())).ReturnsAsync(employee);

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