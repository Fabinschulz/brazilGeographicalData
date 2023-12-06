using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser
{
    public sealed record FindUserByIdRequest(Guid Id) : IRequest<FindUserByIdResponse>;

}
