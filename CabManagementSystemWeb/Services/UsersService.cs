using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Exceptions;
using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Services;

public class UsersService : IUsersService
{
    private readonly IRepository<User> _repository;
    private readonly IRepository<Role> _rolesRepository;
    private readonly IHashService _hashService;

    public UsersService(
        IRepository<User> repository,
        IRepository<Role> rolesRepository,
        IHashService hashService
    )
    {
        _repository = repository;
        _rolesRepository = rolesRepository;
        _hashService = hashService;
    }

    public async Task<IEnumerable<UserDetailDto>> GetAll()
    {
        List<User> users = await _repository.GetAll();

        return users.Select(u => u.ConvertToDetailDto()).ToList();
    }

    public async Task<UserDetailDto?> GetById(int id)
    {
        User? user = await _repository.GetById(id);

        if (user == null)
        {
            throw new NotFoundException();
        }

        return user.ConvertToDetailDto();
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

        User user = userCreateDto.ConvertToEntity();
        user.Password = _hashService.HashPassword(userCreateDto.Password);
        user.Created = DateTime.UtcNow;
        User newUser = await _repository.Create(user);

        return newUser.ConvertToDetailDto();
    }

    public async Task<UserDetailDto> Update(int id, UserUpdateDto userUpdateDto)
    {
        User? user = await _repository.GetById(id);

        if (user == null)
        {
            throw new NotFoundException($"The user with id {id} does not exist");
        }

        if (userUpdateDto.RoleId != null)
        {
            bool roleExists = await _rolesRepository.GetById((int)userUpdateDto.RoleId) != null;

            if (!roleExists)
            {
                throw new NotFoundException($"The role with {userUpdateDto.RoleId} does not exist");
            }
        }

        user = ChangeUpdatedValues(user, userUpdateDto);
        user.Updated = DateTime.UtcNow;
        user = await _repository.Update(user);

        return user.ConvertToDetailDto();
    }

    private User ChangeUpdatedValues(User user, UserUpdateDto userUpdateDto)
    {
        if (userUpdateDto.Username != null)
        {
            user.UserName = userUpdateDto.Username;
        }

        if (userUpdateDto.Password != null)
        {
            user.Password = _hashService.HashPassword(userUpdateDto.Password);
        }

        if (userUpdateDto.Email != null)
        {
            user.Email = userUpdateDto.Email;
        }

        if (userUpdateDto.FirstName != null)
        {
            user.FirstName = userUpdateDto.FirstName;
        }

        if (userUpdateDto.LastName != null)
        {
            user.LastName = userUpdateDto.LastName;
        }

        if (userUpdateDto.RoleId != null)
        {
            user.RoleId = (int)userUpdateDto.RoleId;
        }

        return user;
    }

    public async Task<UserDetailDto> Delete(int id)
    {
        User? user = await _repository.GetById(id);

        if (user == null)
        {
            throw new NotFoundException($"The user with id {id} does not exist");
        }

        await _repository.Delete(user);

        return user.ConvertToDetailDto();
    }
}