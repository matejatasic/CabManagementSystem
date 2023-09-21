namespace CabManagementSystemWeb.Dtos.Interfaces;

public interface IEntityCreateDto<T>
{
    public T ConvertToEntity();
}