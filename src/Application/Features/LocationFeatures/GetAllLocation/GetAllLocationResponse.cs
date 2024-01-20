using BrazilGeographicalData.src.Application.Common.CQBase;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetAllLocation
{
    public class GetAllLocationResponse : GetAllResponseBase<Location>
    {
        public GetAllLocationResponse(ListDataPagination<Location> entity) : base(entity)
        {
        }
    }
}
