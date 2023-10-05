using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class ClientsService : IClientsService
{
    private readonly IRepository<Client, ClientCreateDto, ClientDetailDto> _repository;
    private readonly IHashService _hashService;

    public ClientsService(
        IRepository<Client, ClientCreateDto, ClientDetailDto> repository,
        IHashService hashService
    )
    {
        _repository = repository;
        _hashService = hashService;
    }

    public async Task<IEnumerable<ClientDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<ClientDetailDto?> GetById(int id)
    {
        ClientDetailDto? clientDetailDto = await _repository.GetById(id);

        if (clientDetailDto == null)
        {
            throw new NotFoundException();
        }

        return clientDetailDto;
    }

    public async Task<ClientDetailDto> Create(ClientCreateDto clientCreateDto)
    {
        clientCreateDto.Password = _hashService.HashPassword(clientCreateDto.Password);

        return await _repository.Create(clientCreateDto);
    }

    public async Task<ClientDetailDto> Update(int id, ClientUpdateDto clientUpdateDto)
    {
        ClientDetailDto? clientDetailDto = await _repository.GetById(id);

        if (clientDetailDto == null)
        {
            throw new NotFoundException($"The client with id {id} does not exist");
        }

        clientUpdateDto.Password = _hashService.HashPassword(clientUpdateDto.Password);

        clientDetailDto = await _repository.Update(clientUpdateDto.ConvertToEntity(id));

        return clientDetailDto;
    }

    public async Task<ClientDetailDto> Delete(int id)
    {
        ClientDetailDto? clientDetailDto = await _repository.GetById(id);

        if (clientDetailDto == null)
        {
            throw new NotFoundException($"The client with id {id} does not exist");
        }

        await _repository.Delete(clientDetailDto.ConvertToEntity(id));

        return clientDetailDto;
    }
}