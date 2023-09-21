namespace CabManagementSystemWeb.Dtos.Interfaces;

public interface IEntityUpdateDto<T>
{
    public T ConvertToEntity(int id);
}