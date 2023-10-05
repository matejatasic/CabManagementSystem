using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Contracts;

public interface IClientsService
{
    public Task<IEnumerable<ClientDetailDto>> GetAll();

    public Task<ClientDetailDto?> GetById(int id);

    public Task<ClientDetailDto> Create(ClientCreateDto clientCreateDto);

    public Task<ClientDetailDto> Update(int id, ClientUpdateDto clientUpdateDto);

    public Task<ClientDetailDto> Delete(int id);
}