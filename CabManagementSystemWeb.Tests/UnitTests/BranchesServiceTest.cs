using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class BranchesServiceTest
{
    private readonly int _id = 1;

    private readonly IBranchesService _branchesService;

    private readonly Mock<IRepository<Branch>> _branchesRepositoryMock;
    private readonly Mock<IRepository<Employee>> _employeesRepositoryMock;

    private readonly IFixture _fixture;

    public BranchesServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _branchesRepositoryMock = new Mock<IRepository<Branch>>();
        _employeesRepositoryMock = new Mock<IRepository<Employee>>();
        _branchesService = new BranchesService(_branchesRepositoryMock.Object, _employeesRepositoryMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        Branch branch = _fixture.Create<Branch>();
        var expectedResult = new List<Branch>() {branch};

        _branchesRepositoryMock.Setup(b => b.GetAll()).ReturnsAsync(expectedResult);

        var result = await _branchesService.GetAll();

        Assert.Single(result);
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedBranch()
    {
        var expectedResult = _fixture.Create<Branch>();
        expectedResult.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _branchesService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedBranch()
    {
        Func<Task> act = () => _branchesService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedBranch()
    {
        var expectedResult = _fixture.Create<Branch>();
        var expectedEmployeeResult = _fixture.Create<Employee>();
        BranchCreateDto branchCreateDto = _fixture.Build<BranchCreateDto>()
            .Create();

        expectedResult.Id = _id;

        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedEmployeeResult);
        _branchesRepositoryMock.Setup(b => b.Create(It.IsAny<Branch>())).ReturnsAsync(expectedResult);

        var result = await _branchesService.Create(branchCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestCreateThrowingExceptionWhenManagerNotFound()
    {
        BranchCreateDto branchCreateDto = _fixture.Create<BranchCreateDto>();

        Func<Task> act = () => _branchesService.Create(branchCreateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedBranch()
    {
        var expectedResult = _fixture.Create<Branch>();

        BranchUpdateDto branchUpdateDto = _fixture.Create<BranchUpdateDto>();
        Employee employee = _fixture.Create<Employee>();
        expectedResult.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _employeesRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(employee);
        _branchesRepositoryMock.Setup(e => e.Update(It.IsAny<Branch>())).ReturnsAsync(expectedResult);

        var result = await _branchesService.Update(It.IsAny<int>(), branchUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenBranchNotFound()
    {
        BranchUpdateDto branchUpdateDto = _fixture.Create<BranchUpdateDto>();

        Func<Task> act = () => _branchesService.Update(It.IsAny<int>(), branchUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenEmployeeNotFound()
    {
        Branch branch = _fixture.Create<Branch>();
        BranchUpdateDto branchUpdateDto = _fixture.Create<BranchUpdateDto>();

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(branch);
        Func<Task> act = () => _branchesService.Update(It.IsAny<int>(), branchUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        Branch branch = _fixture.Create<Branch>();
        branch.Id = _id;

        _branchesRepositoryMock.Setup(b => b.GetById(It.IsAny<int>())).ReturnsAsync(branch);
        _branchesRepositoryMock.Setup(b => b.Delete(It.IsAny<Branch>())).ReturnsAsync(branch);

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