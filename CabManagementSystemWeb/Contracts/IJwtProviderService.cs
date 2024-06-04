using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Contracts;

public interface IJwtProviderService
{
    string Generate(User user);
}