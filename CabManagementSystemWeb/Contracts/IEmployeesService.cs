using CabManagementSystemWeb.Dtos;
using CabManagementSystemWeb.Entities;

namespace CabManagementSystemWeb.Contracts;

public interface IEmployeesService
{
    public Task<IEnumerable<EmployeeDetailDto>> GetAll();

    public Task<EmployeeDetailDto?> GetById(int id);

    public Task<EmployeeDetailDto> Create(EmployeeCreateDto employeeCreateDto);

    public Task<EmployeeDetailDto> Update(int id, EmployeeUpdateDto employeeUpdateDto);

    public Task<EmployeeDetailDto> Delete(int id);
}