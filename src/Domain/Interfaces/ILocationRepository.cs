using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Domain.Interfaces
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        Task<ListDataPagination<Location>> GetAll(int page, int size, int? IBGECode, string? state, string? city, string? orderBy);

    }
}
