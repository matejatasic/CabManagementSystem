using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class RolesService : IRolesService
{
    private readonly IRepository<Role> _repository;

    public RolesService(
        IRepository<Role> repository
    )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoleDetailDto>> GetAll()
    {
        List<Role> roles = await _repository.GetAll();
        return roles.Select(r => r.ConvertToDetailDto());
    }

    public async Task<RoleDetailDto?> GetById(int id)
    {
        Role? role = await _repository.GetById(id);

        if (role == null)
        {
            throw new NotFoundException();
        }

        return role.ConvertToDetailDto();
    }

    public async Task<RoleDetailDto> Create(RoleCreateDto roleCreateDto)
    {
        Role role = roleCreateDto.ConvertToEntity();
        role.Created = DateTime.UtcNow;
        Role newRole = await _repository.Create(role);

        return newRole.ConvertToDetailDto();
    }

    public async Task<RoleDetailDto> Update(int id, RoleUpdateDto roleUpdateDto)
    {
        Role? role = await _repository.GetById(id);

        if (role == null)
        {
            throw new NotFoundException($"The role with id {id} does not exist");
        }

        role = ChangeUpdatedValues(role, roleUpdateDto);
        role.Updated = DateTime.UtcNow;
        role = await _repository.Update(role);

        return role.ConvertToDetailDto();
    }

    private Role ChangeUpdatedValues(Role role, RoleUpdateDto roleUpdateDto)
    {
        if (roleUpdateDto.Name != null)
        {
            role.Name = roleUpdateDto.Name;
        }

        return role;
    }

    public async Task<RoleDetailDto> Delete(int id)
    {
        Role? role = await _repository.GetById(id);

        if (role == null)
        {
            throw new NotFoundException($"The role with id {id} does not exist");
        }

        await _repository.Delete(role);

        return role.ConvertToDetailDto();
    }
}