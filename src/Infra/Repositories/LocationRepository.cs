using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Context;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(DataContext context) : base(context)
        {
        }
    }
}
