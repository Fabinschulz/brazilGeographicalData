using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetLocation
{
    public sealed class FindLocationByIdMapper : Profile
    {
        public FindLocationByIdMapper()
        {
            CreateMap<FindLocationByIdRequest, Location>();
            CreateMap<Location, FindLocationByIdResponse>();
        }
    }
}
