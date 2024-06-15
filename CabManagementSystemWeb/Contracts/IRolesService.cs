using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Contracts;

public interface IRolesService
{
    public Task<IEnumerable<RoleDetailDto>> GetAll();

    public Task<RoleDetailDto?> GetById(int id);

    public Task<RoleDetailDto> Create(RoleCreateDto userCreateDto);

    public Task<RoleDetailDto> Update(int id, RoleUpdateDto userUpdateDto);

    public Task<RoleDetailDto> Delete(int id);
}