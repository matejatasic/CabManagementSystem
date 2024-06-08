using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Contracts;

public interface IJwtProviderService
{
    string Generate(string userId, string email);
}