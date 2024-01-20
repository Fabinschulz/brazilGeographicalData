using BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation;
using BrazilGeographicalData.src.Application.Features.LocationFeatures.DeleteLocation;
using BrazilGeographicalData.src.Application.Features.LocationFeatures.GetAllLocation;
using BrazilGeographicalData.src.Application.Features.LocationFeatures.GetLocation;
using BrazilGeographicalData.src.Application.Features.LocationFeatures.PutLocation;
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

            }).WithTags("IBGE").WithSummary("Create a new location").WithOpenApi().RequireAuthorization(IdentityData.AdminPolicy);

            app.MapGet("/v1/location", async (IMediator mediator, int page, int size, int? IBGECode, string? State, string? City, string? orderBy) =>
            {
                var getAllUserRequest = new GetAllLocationRequest(page, size, IBGECode, State, City, orderBy);
                var locations = await mediator.Send(getAllUserRequest);
                return Results.Ok(locations);

            }).WithTags("IBGE").WithSummary("Get all location").WithOpenApi();

            app.MapGet("/v1/location/{id}", async (IMediator mediator, Guid id) =>
            {
                var command = new FindLocationByIdRequest(id);
                var location = await mediator.Send(command);
                return Results.Ok(location);

            }).WithTags("IBGE").WithSummary("Find a location by id").WithOpenApi().RequireAuthorization(IdentityData.AdminPolicy);

            app.MapDelete("/v1/location/{id}", async (IMediator mediator, Guid id) =>
            {

                var command = new DeleteLocationRequest(id);
                var isDeleted = await mediator.Send(command);
                return Results.Ok(isDeleted);

            }).WithTags("IBGE").WithSummary("Delete a location").WithOpenApi().RequireAuthorization(IdentityData.AdminPolicy);

            app.MapPut("/v1/location/{id}", async (IMediator mediator, Guid id, PutLocationRequest command) =>
            {

                var putLocationRequest = new PutLocationRequest(id, command.IBGECode, command.City, command.State);
                var location = await mediator.Send(putLocationRequest);
                return Results.Ok(location);

            }).WithTags("IBGE").WithSummary("Update a location").WithOpenApi().RequireAuthorization(IdentityData.AdminPolicy);
        }
    }
}
