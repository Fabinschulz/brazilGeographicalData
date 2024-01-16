using BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Services.Extensions
{
    public static class LocationExtension
    {
        public static void MapLocationEndpoints(this WebApplication app)
        {
            app.MapPost("/v1/location", async (IMediator mediator, CreateLocationRequest command) =>
            {

                var resp = await mediator.Send(command);
                return Results.Ok(resp);

            }).WithTags("IBGE").WithSummary("Create a new location").WithOpenApi();
        }
    }
}
