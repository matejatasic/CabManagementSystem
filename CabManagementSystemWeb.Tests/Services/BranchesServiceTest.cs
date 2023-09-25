using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Dtos.Interfaces;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;
using Xunit;
using Moq;
using AutoFixture;

namespace CabManagementSystemWeb.Tests.Services;

public class BranchesServiceTest
{
    private readonly int _id = 1;

    private readonly IBranchesService _branchesService;

    private readonly Mock<IRepository<Branch, BranchCreateDto, BranchDetailDto>> _branchesRepositoryMock;
    private readonly Mock<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>> _employeesRepositoryMock;

    private readonly IFixture _fixture;

    public BranchesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _branchesRepositoryMock = new Mock<IRepository<Branch, BranchCreateDto, BranchDetailDto>>();
        _employeesRepositoryMock = new Mock<IRepository<Employee, EmployeeCreateDto, EmployeeDetailDto>>();
        _branchesService = new BranchesService(_branchesRepositoryMock.Object, _employeesRepositoryMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        BranchDetailDto branchDetailDto = _fixture.Create<BranchDetailDto>();
        var expectedResult = new List<BranchDetailDto>() {branchDetailDto};

        _branchesRepositoryMock.Setup(b => b.GetAll()).ReturnsAsync(expectedResult);

        var result = await _branchesService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedBranch()
    {
        var expectedResult = _fixture.Create<BranchDetailDto>();
        expectedResult.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _branchesService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenSuccessfullyRetrievedBranch()
    {
        Func<Task> act = () => _branchesService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedBranch()
    {
        var expectedResult = _fixture.Create<BranchDetailDto>();
        var expectedEmployeeResult = _fixture.Create<EmployeeDetailDto>();
        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Create();

        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedEmployeeResult);
        _branchesRepositoryMock.Setup(b => b.Create(It.IsAny<BranchCreateDto>())).ReturnsAsync(expectedResult);

        var result = await _branchesService.Create(branchCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenCreateNotSuccessful()
    {
        BranchCreateDto branchCreateDto = _fixture.Create<BranchCreateDto>();

        Func<Task> act = () => _branchesService.Create(branchCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedBranch()
    {
        var expectedResult = _fixture.Create<BranchDetailDto>();

        BranchUpdateDto branchUpdateDto = _fixture.Create<BranchUpdateDto>();
        EmployeeDetailDto employeeDetailDto = _fixture.Create<EmployeeDetailDto>();
        expectedResult.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employeeDetailDto);
        _branchesRepositoryMock.Setup(e => e.Update(It.IsAny<Branch>())).ReturnsAsync(expectedResult);

        var result = await _branchesService.Update(It.IsAny<int>(), branchUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionBranchNotFound()
    {
        BranchUpdateDto branchUpdateDto = _fixture.Create<BranchUpdateDto>();

        Func<Task> act = () => _branchesService.Update(It.IsAny<int>(), branchUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenBranchNotFound()
    {
        BranchDetailDto branchDetailDto = _fixture.Create<BranchDetailDto>();
        BranchUpdateDto branchUpdateDto = _fixture.Create<BranchUpdateDto>();

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(branchDetailDto);
        Func<Task> act = () => _branchesService.Update(It.IsAny<int>(), branchUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        BranchDetailDto branchDetailDto = _fixture.Create<BranchDetailDto>();
        branchDetailDto.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(branchDetailDto);
        _branchesRepositoryMock.Setup(b => b.Delete(It.IsAny<Branch>())).ReturnsAsync(branchDetailDto);

        var result = await _branchesService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenEmployeeNotFound()
    {
        Func<Task> act = () => _branchesService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}