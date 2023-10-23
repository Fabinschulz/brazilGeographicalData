using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserRequest(string Username, string Password, string Email, string Role, bool IsDeleted) : IRequest<CreateUserResponse>;
}
