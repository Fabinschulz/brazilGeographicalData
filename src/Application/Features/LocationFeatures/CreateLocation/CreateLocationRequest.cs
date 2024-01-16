using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation
{
    public sealed record CreateLocationRequest(int IBGECode, string State, string City) : IRequest<CreateLocationResponse>;

}
