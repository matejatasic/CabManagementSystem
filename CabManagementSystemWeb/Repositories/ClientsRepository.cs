using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabManagementSystemWeb.Repositories;

public class ClientsRepository : IRepository<Client, ClientCreateDto, ClientDetailDto>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ClientDetailDto>> GetAll()
    {
        List<Client> clients = await _dbContext.Clients.ToListAsync();

        return clients.Select(c => c.ConvertToDetailDto()).ToList();
    }

    public async Task<ClientDetailDto?> GetById(int id)
    {
        Client? clients = await _dbContext.Clients.FindAsync(id);
        _dbContext.ChangeTracker.Clear();

        return clients?.ConvertToDetailDto();
    }

    public async Task<ClientDetailDto> Create(ClientCreateDto clientDto)
    {
        Client client = clientDto.ConvertToEntity();

        client.Created = DateTime.UtcNow;
        _dbContext.Add(client);
        await _dbContext.SaveChangesAsync();

        return client.ConvertToDetailDto();
    }

    public async Task<ClientDetailDto> Update(Client client)
    {
        client.Updated = DateTime.UtcNow;
        _dbContext.Clients.Update(client);
        await _dbContext.SaveChangesAsync();

        return client.ConvertToDetailDto();
    }

    public async Task<ClientDetailDto> Delete(Client client)
    {
        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();

        return client.ConvertToDetailDto();
    }
}