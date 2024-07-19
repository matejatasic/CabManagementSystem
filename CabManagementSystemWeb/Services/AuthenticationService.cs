using AuthenticationApi.Services;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Exceptions;

namespace CabManagementSystemWeb.Services;

public class AuthenticationService : IAuthenticationService
{
    IRepository<User> _usersRepository;
    IRepository<Role> _rolesRepository;
    IUsersService _usersService;
    IHashService _hashService;
    IJwtProviderService _jwtProviderService;

    public AuthenticationService(
        IRepository<User> usersRepository,
        IRepository<Role> rolesRepository,
        IUsersService usersService,
        IHashService hashService,
        IJwtProviderService jwtProviderService
    )
    {
        _usersRepository = usersRepository;
        _rolesRepository = rolesRepository;
        _usersService = usersService;
        _hashService = hashService;
        _jwtProviderService = jwtProviderService;
    }

    public async Task<AuthenticationResponseDto> Register(RegisterDto request)
    {
        Role? userRole = await _rolesRepository.GetBy("name", "User");

        if (userRole == null) {
            throw new NotFoundException("User role does not exist");
        }

        UserCreateDto userCreateDto = new UserCreateDto()
        {
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            RoleId = userRole.Id
        };

        await _usersService.Create(userCreateDto);

        return await Login(
            request.ConvertToLoginDto()
        );
    }

    public async Task<AuthenticationResponseDto> Login(LoginDto request)
    {
        User? user = await _usersRepository.GetBy("username", request.Username);
        bool userExists = user != null;

        if (!userExists)
        {
            throw new NotFoundException($"User with the username {request.Username} does not exist");
        }

        bool passwordsMatch = _hashService.Verify(
            request.Password,
            user.Password
        );

        if (!passwordsMatch)
        {
            throw new ArgumentException("Your password is incorrect");
        }

        Role role = await _rolesRepository.GetById(user.RoleId);
        string token = _jwtProviderService.Generate(user.Id.ToString(), user.Email, role.Name);

        return new AuthenticationResponseDto()
        {
            UserId = user.Id,
            Username = user.UserName,
            Token = token,
            Role = role.Name
        };
    }
}