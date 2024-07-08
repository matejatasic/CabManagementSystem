using Microsoft.IdentityModel.Tokens;
using CabManagementSystemWeb.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CabManagementSystemWeb.Options;
using Microsoft.Extensions.Options;

namespace CabManagementSystemWeb.Services;

public class JwtProviderService : IJwtProviderService
{
    private readonly JwtOptions _jwtOptions;

    public JwtProviderService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string Generate(string userId, string email, string role)
    {
        Claim[] claims = new Claim[] {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim("role", role)
        };

        SigningCredentials signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)
            ),
            SecurityAlgorithms.HmacSha256
        );

        JwtSecurityToken token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials
        );

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}