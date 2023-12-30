using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.UserLogin
{

    public sealed class UserLoginMapper : Profile
    {
        public UserLoginMapper()
        {
            CreateMap<UserLoginRequest, LoggedUser>();
            CreateMap<LoggedUser, UserLoginResponse>();
        }
    }
}
