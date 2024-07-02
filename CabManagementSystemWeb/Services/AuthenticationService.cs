using AuthenticationApi.Services;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;
using CabManagementSystemWeb.Exceptions;

namespace CabManagementSystemWeb.Services;

public class AuthenticationService : IAuthenticationService
{
    IRepository<User, UserCreateDto, UserDetailDto> _usersRepository;
    IRepository<Role, RoleCreateDto, RoleDetailDto> _rolesRepository;
    IUsersService _usersService;
    IHashService _hashService;
    IJwtProviderService _jwtProviderService;

    public AuthenticationService(
        IRepository<User, UserCreateDto, UserDetailDto> usersRepository,
        IRepository<Role, RoleCreateDto, RoleDetailDto> rolesRepository,
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
        RoleDetailDto? userRole = await _rolesRepository.GetBy("name", "User");

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
        UserDetailDto? user = await _usersRepository.GetBy("username", request.Username);
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

        RoleDetailDto roleDetailDto = await _rolesRepository.GetById(user.RoleId);

        string token = _jwtProviderService.Generate(user.Id.ToString(), user.Email, roleDetailDto.Name);

        return new AuthenticationResponseDto()
        {
            UserId = user.Id,
            Username = user.Username,
            Token = token,
            Role = roleDetailDto.Name
        };
    }
}