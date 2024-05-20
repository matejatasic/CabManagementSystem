using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Contracts;

public interface IUsersService
{
    public Task<IEnumerable<UserDetailDto>> GetAll();

    public Task<UserDetailDto?> GetById(int id);

    public Task<UserDetailDto> Create(UserCreateDto userCreateDto);

    public Task<UserDetailDto> Update(int id, UserUpdateDto userUpdateDto);

    public Task<UserDetailDto> Delete(int id);
}