using AuthenticationApi.Services;
using CabManagementSystemWeb.Contracts;
using CabManagementSystemWeb.Data;
using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Services;

public class AuthenticationService : IAuthenticationService
{
    IRepository<User, UserCreateDto, UserDetailDto> _usersRepository;
    IUsersService _usersService;
    IHashService _hashService;
    IJwtProviderService _jwtProviderService;

    public AuthenticationService(
        IRepository<User, UserCreateDto, UserDetailDto> usersRepository,
        IUsersService usersService,
        IHashService hashService,
        IJwtProviderService jwtProviderService
    )
    {
        _usersRepository = usersRepository;
        _usersService = usersService;
        _hashService = hashService;
        _jwtProviderService = jwtProviderService;
    }

    public async Task<string> Register(RegisterDto request)
    {
        UserCreateDto userCreateDto = new UserCreateDto()
        {
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber
        };

        await _usersService.Create(userCreateDto);

        return await Login(
            new LoginDto()
            {
                Username = request.Username,
                Password = request.Password
            }
        );
    }

    public async Task<string> Login(LoginDto request)
    {
        UserDetailDto? user = await _usersRepository.GetBy("username", request.Username);
        bool userExists = user != null;

        if (!userExists)
        {
            throw new ArgumentException($"User with the username {request.Username} does not exist");
        }

        bool passwordsMatch = _hashService.Verify(
            request.Password,
            user.Password
        );

        if (!passwordsMatch)
        {
            throw new ArgumentException("Your password is incorrect");
        }

        string token = _jwtProviderService.Generate(user.ConvertToEntity());

        return token;
    }
}