using BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation;
using BrazilGeographicalData.src.Application.Features.LocationFeatures.DeleteLocation;
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

            app.MapDelete("/v1/location/{id}", async (IMediator mediator, Guid id) =>
            {

                var command = new DeleteLocationRequest(id);

                var isDeleted = await mediator.Send(command);
                return Results.Ok(isDeleted);

            }).WithTags("IBGE").WithSummary("Delete a location").WithOpenApi().RequireAuthorization(IdentityData.AdminPolicy);
        }
    }
}
