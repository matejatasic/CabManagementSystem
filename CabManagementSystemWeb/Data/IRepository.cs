using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Data;

public interface IRepository<T, C, D>
    where T : class, IEntity
    where C : class, IEntityCreateDto<T>
    where D : class, IEntityDetailDto<T>
{
    public Task<List<D>> GetAll();
    public Task<D?> GetById(int id);
    public Task<D?> GetBy(string property, object value);
    public Task<D> Create(C entityCreateDto);
    public Task<D> Update(T entity);
    public Task<D> Delete(T entity);
}