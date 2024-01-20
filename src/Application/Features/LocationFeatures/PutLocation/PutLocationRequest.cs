using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.PutLocation
{
    public sealed record PutLocationRequest(Guid id, int IBGECode, string State, string City) : IRequest<PutLocationResponse>;
}
