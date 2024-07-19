using CabManagementSystemWeb.Dtos.Interfaces;

namespace CabManagementSystemWeb.Data;

public interface IRepository<T>
    where T : class, IEntity
{
    public Task<List<T>> GetAll();
    public Task<T?> GetById(int id);
    public Task<T?> GetBy(string property, object value);
    public Task<T> Create(T entity);
    public Task<T> Update(T entity);
    public Task<T> Delete(T entity);
}