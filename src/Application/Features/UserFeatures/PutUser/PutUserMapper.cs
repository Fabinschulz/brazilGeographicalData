using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.PutUser
{
    public sealed class PutUserMapper : Profile
    {
        public PutUserMapper()
        {
            CreateMap<PutUserRequest, User>();
            CreateMap<User, PutUserResponse>();
        }
    }
}
