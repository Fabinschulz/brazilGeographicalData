using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser
{
    public sealed class FindUserByIdMapper : Profile
    {
        public FindUserByIdMapper()
        {
            CreateMap<FindUserByIdRequest, User>();
            CreateMap<User, FindUserByIdResponse>();
        }
    }
}
