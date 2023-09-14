using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Contracts;

public interface IEmployeesService
{
    public Task<IEnumerable<Employee>> GetAll();

    public Task<Employee?> GetById(int id);

    public Task<Employee> Create(Employee employee);

    public Task<Employee?> Update(int id, Employee employee);

    public Task<Employee?> Delete(int id);
}