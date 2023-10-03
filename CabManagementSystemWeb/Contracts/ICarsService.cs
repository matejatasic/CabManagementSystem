using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Contracts;

public interface ICarsService
{
    public Task<IEnumerable<CarDetailDto>> GetAll();

    public Task<CarDetailDto?> GetById(int id);

    public Task<CarDetailDto> Create(CarCreateDto carCreateDto);

    public Task<CarDetailDto> Update(int id, CarUpdateDto carUpdateDto);

    public Task<CarDetailDto> Delete(int id);
}