using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetAllLocation
{

    public sealed class GetAllLocationMapper : Profile
    {
        public GetAllLocationMapper()
        {
            CreateMap<GetAllLocationRequest, Location>();
            CreateMap<Location, GetAllLocationResponse>();
        }
    }
}
