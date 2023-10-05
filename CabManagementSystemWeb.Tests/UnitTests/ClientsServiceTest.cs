using Moq;
using AutoFixture;

using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Services;

namespace CabManagementSystemWeb.Tests.Services;

public class ClientsServiceTest
{
    private readonly int _id = 1;

    private readonly IClientsService _clientsService;

    private readonly Mock<IRepository<Client, ClientCreateDto, ClientDetailDto>> _clientsRepositoryMock;
    private readonly Mock<IHashService> _hashServiceMock;

    private readonly IFixture _fixture;

    public ClientsServiceTest()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _clientsRepositoryMock = new Mock<IRepository<Client, ClientCreateDto, ClientDetailDto>>();
        _hashServiceMock = new Mock<IHashService>();
        _clientsService = new ClientsService(_clientsRepositoryMock.Object, _hashServiceMock.Object);
    }

    [Fact]
    public async void TestGetAllReturningAppropriateResult()
    {
        ClientDetailDto clientDetailDto = _fixture.Create<ClientDetailDto>();
        var expectedResult = new List<ClientDetailDto>() {clientDetailDto};

        _clientsRepositoryMock.Setup(e => e.GetAll()).ReturnsAsync(expectedResult);

        var result = await _clientsService.GetAll();

        Assert.Equal(1, result.Count());
    }

    [Fact]
    public async void TestGetByIdReturningAppropriateResultWhenSuccessfullyRetrievedClient()
    {
        var expectedResult = _fixture.Create<ClientDetailDto>();
        expectedResult.Id = _id;

        _clientsRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);

        var result = await _clientsService.GetById(It.IsAny<int>());

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestGetByIdThrowingExceptionWhenNotRetrievedClient()
    {
        Func<Task> act = () => _clientsService.GetById(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestCreateReturningAppropriateResultWhenSuccessfullyCreatedClient()
    {
        var expectedResult = _fixture.Create<ClientDetailDto>();
        ClientCreateDto clientCreateDto = _fixture.Build<ClientCreateDto>()
            .Create();
        expectedResult.Id = _id;

        _clientsRepositoryMock.Setup(e => e.Create(It.IsAny<ClientCreateDto>())).ReturnsAsync(expectedResult);

        var result = await _clientsService.Create(clientCreateDto);

        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateReturningAppropriateResultWhenSuccessfullyUpdatedClient()
    {
        var expectedResult = _fixture.Create<ClientDetailDto>();
        ClientUpdateDto clientUpdateDto = _fixture.Create<ClientUpdateDto>();
        expectedResult.Id = _id;

        _clientsRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(expectedResult);
        _clientsRepositoryMock.Setup(e => e.Update(It.IsAny<Client>())).ReturnsAsync(expectedResult);

        var result = await _clientsService.Update(It.IsAny<int>(), clientUpdateDto);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestUpdateThrowingExceptionWhenClientNotFound()
    {
        ClientUpdateDto clientUpdateDto = _fixture.Create<ClientUpdateDto>();

        Func<Task> act = () => _clientsService.Update(It.IsAny<int>(), clientUpdateDto);

        await Assert.ThrowsAsync<NotFoundException>(act);
    }

    [Fact]
    public async void TestDeleteReturningAppropriateResultWhenDeleteSuccessful()
    {
        ClientDetailDto clientDetailDto = _fixture.Create<ClientDetailDto>();
        clientDetailDto.Id = _id;

        _clientsRepositoryMock.Setup(e => e.GetById(It.IsAny<int>())).ReturnsAsync(clientDetailDto);
        _clientsRepositoryMock.Setup(e => e.Delete(It.IsAny<Client>())).ReturnsAsync(clientDetailDto);

        var result = await _clientsService.Delete(It.IsAny<int>());
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public async void TestDeleteThrowingExceptionWhenClientNotFound()
    {
        Func<Task> act = () => _clientsService.Delete(It.IsAny<int>());

        await Assert.ThrowsAsync<NotFoundException>(act);
    }
}