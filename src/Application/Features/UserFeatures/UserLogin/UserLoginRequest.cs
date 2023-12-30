using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.UserLogin
{
    public sealed record UserLoginRequest(string Email, string Password) : IRequest<UserLoginResponse>;
}
