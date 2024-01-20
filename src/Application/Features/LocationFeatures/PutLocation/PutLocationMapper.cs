using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.PutLocation
{

    public sealed class PutLocationMapper : Profile
    {
        public PutLocationMapper()
        {
            CreateMap<PutLocationRequest, Location>();
            CreateMap<Location, PutLocationResponse>();
        }
    }

}
