using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class UsersService : IUsersService
{
    private readonly IRepository<User, UserCreateDto, UserDetailDto> _repository;
    private readonly IRepository<Role, RoleCreateDto, RoleDetailDto> _rolesRepository;
    private readonly IHashService _hashService;

    public UsersService(
        IRepository<User, UserCreateDto, UserDetailDto> repository,
        IRepository<Role, RoleCreateDto, RoleDetailDto> rolesRepository,
        IHashService hashService
    )
    {
        _repository = repository;
        _rolesRepository = rolesRepository;
        _hashService = hashService;
    }

    public async Task<IEnumerable<UserDetailDto>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<UserDetailDto?> GetById(int id)
    {
        UserDetailDto? userDetailDto = await _repository.GetById(id);

        if (userDetailDto == null)
        {
            throw new NotFoundException();
        }

        return userDetailDto;
    }

    public async Task<UserDetailDto> Create(UserCreateDto userCreateDto)
    {
        bool usernameExists = await _repository.GetBy("username", userCreateDto.Username) != null;
        bool emailExists = await _repository.GetBy("email", userCreateDto.Email) != null;
        bool roleExists = await _rolesRepository.GetById(userCreateDto.RoleId) != null;

        if (usernameExists)
        {
            throw new ArgumentException($"The user with the username {userCreateDto.Username} already exists.");
        }

        if (emailExists)
        {
            throw new ArgumentException($"The user with the email {userCreateDto.Email} already exists");
        }

        if (!roleExists)
        {
            throw new NotFoundException($"The role with {userCreateDto.RoleId} does not exist");
        }

        userCreateDto.Password = _hashService.HashPassword(userCreateDto.Password);

        return await _repository.Create(userCreateDto);
    }

    public async Task<UserDetailDto> Update(int id, UserUpdateDto userUpdateDto)
    {
        UserDetailDto? userDetailDto = await _repository.GetById(id);
        bool roleExists = await _rolesRepository.GetById(userUpdateDto.RoleId) != null;

        if (userDetailDto == null)
        {
            throw new NotFoundException($"The user with id {id} does not exist");
        }

        if (!roleExists)
        {
            throw new NotFoundException($"The role with {userUpdateDto.RoleId} does not exist");
        }

        userUpdateDto.Password = _hashService.HashPassword(userUpdateDto.Password);

        userDetailDto = await _repository.Update(userUpdateDto.ConvertToEntity(id));

        return userDetailDto;
    }

    public async Task<UserDetailDto> Delete(int id)
    {
        UserDetailDto? userDetailDto = await _repository.GetById(id);

        if (userDetailDto == null)
        {
            throw new NotFoundException($"The user with id {id} does not exist");
        }

        await _repository.Delete(userDetailDto.ConvertToEntity());

        return userDetailDto;
    }
}