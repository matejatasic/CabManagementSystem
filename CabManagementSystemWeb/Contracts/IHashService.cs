namespace CabManagementSystemWeb.Contracts;

public interface IHashService
{
    public string HashPassword(string password);

    public bool Verify(string password, string hashedPassword);
}