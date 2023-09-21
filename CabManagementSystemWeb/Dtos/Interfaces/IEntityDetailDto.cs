namespace CabManagementSystemWeb.Dtos.Interfaces;

public interface IEntityDetailDto<T>
{

    public T ConvertToEntity();
}