using AutoMapper;
using BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.Create
{
    public sealed class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
        }
    }
}
