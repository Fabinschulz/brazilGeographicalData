using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserRequest(string Username, string Email, string Password) : IRequest<CreateUserResponse>;
}
