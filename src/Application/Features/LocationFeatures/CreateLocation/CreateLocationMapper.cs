using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation
{
    public sealed class CreateLocationMapper : Profile
    {
        public CreateLocationMapper()
        {
            CreateMap<CreateLocationRequest, Location>();
            CreateMap<Location, CreateLocationResponse>();
        }
    }
}
