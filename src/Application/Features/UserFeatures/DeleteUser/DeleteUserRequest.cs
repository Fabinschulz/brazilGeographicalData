using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.DeleteUser
{
    public sealed record DeleteUserRequest(Guid Id) : IRequest<DeleteUserResponse>;
}
