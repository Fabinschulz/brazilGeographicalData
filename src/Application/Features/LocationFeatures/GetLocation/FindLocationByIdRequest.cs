using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetLocation
{
    public sealed record FindLocationByIdRequest(Guid Id) : IRequest<FindLocationByIdResponse>;
}
