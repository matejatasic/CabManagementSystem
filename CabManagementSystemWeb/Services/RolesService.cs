using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class RolesService : IRolesService
{
    private readonly IRepository<Role, RoleCreateDto, RoleDetailDto> _repository;

    public RolesService(
        IRepository<Role, RoleCreateDto, RoleDetailDto> repository
    )
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoleDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<RoleDetailDto?> GetById(int id)
    {
        RoleDetailDto? roleDetailDto = await _repository.GetById(id);

        if (roleDetailDto == null)
        {
            throw new NotFoundException();
        }

        return roleDetailDto;
    }

    public async Task<RoleDetailDto> Create(RoleCreateDto roleCreateDto)
    {
        return await _repository.Create(roleCreateDto);
    }

    public async Task<RoleDetailDto> Update(int id, RoleUpdateDto roleUpdateDto)
    {
        RoleDetailDto? roleDetailDto = await _repository.GetById(id);

        if (roleDetailDto == null)
        {
            throw new NotFoundException($"The role with id {id} does not exist");
        }

        roleDetailDto = await _repository.Update(roleUpdateDto.ConvertToEntity(id));

        return roleDetailDto;
    }

    public async Task<RoleDetailDto> Delete(int id)
    {
        RoleDetailDto? roleDetailDto = await _repository.GetById(id);

        if (roleDetailDto == null)
        {
            throw new NotFoundException($"The role with id {id} does not exist");
        }

        await _repository.Delete(roleDetailDto.ConvertToEntity());

        return roleDetailDto;
    }
}