using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser
{
    public sealed record GetAllUserRequest(int Page, int Size, string? Username, string? Email, bool IsDeleted, string? OrderBy, string? Role) : IRequest<GetAllUserResponse>;
}
