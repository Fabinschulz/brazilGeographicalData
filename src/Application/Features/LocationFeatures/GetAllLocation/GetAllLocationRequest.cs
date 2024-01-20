using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetAllLocation
{
    public sealed record GetAllLocationRequest(int Page, int Size, int? IBGECode, string? City, string? State, string? OrderBy) : IRequest<GetAllLocationResponse>;
}
