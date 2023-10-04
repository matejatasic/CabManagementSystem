using CabManagementSystemWeb.Dtos;

namespace CabManagementSystemWeb.Contracts;

public interface IRoutesService
{
    public Task<IEnumerable<RouteDetailDto>> GetAll();

    public Task<RouteDetailDto?> GetById(int id);

    public Task<RouteDetailDto> Create(RouteCreateDto branchCreateDto);

    public Task<RouteDetailDto> Update(int id, RouteUpdateDto branchUpdateDto);

    public Task<RouteDetailDto> Delete(int id);
}