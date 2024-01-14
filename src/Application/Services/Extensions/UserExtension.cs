using BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.DeleteUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.PutUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.UserLogin;
using MediatR;

namespace BrazilGeographicalData.src.Application.Services.Extensions
{
    public static class UserExtension
    {

        public static void MapUserEndpoints(this WebApplication app)
        {

            // app.MapGet("/v1/user/authenticated", async (UserRepository _userRepository, ClaimsPrincipal user) =>
            // {
            //     try
            //     {
            //         var authenticatedUser = await _userRepository.GetAuthenticatedUser(user);
            //         return Results.Ok(authenticatedUser);
            //     }
            //     catch (Exception ex)
            //     {
            //         throw new BadRequestException(ex.Message);
            //     }
            // }).RequireAuthorization("Admin");

             app.MapPost("/v1/user/login", async (IMediator mediator, string Email, string Password) =>
             {
                 var request = new UserLoginRequest(Email, Password);
               var userLoginResponse = await mediator.Send(request);
                 return Results.Ok(userLoginResponse);

             }).WithTags("USER").WithSummary("Login a user").WithOpenApi();

            app.MapGet("/v1/user", async (IMediator mediator, int page, int size, string? username, string? email, bool? isDeleted, string? orderBy, string? role) =>
            {
                var getAllUserRequest = new GetAllUserRequest(page, size, username, email, isDeleted ?? false, orderBy, role);
                var users = await mediator.Send(getAllUserRequest);
                return Results.Ok(users);
            }).WithTags("USER").WithSummary("Get all users").WithOpenApi();

            app.MapGet("/v1/user/{id}", async (IMediator mediator, Guid id) =>
            {
                var command = new FindUserByIdRequest(id);
                var user = await mediator.Send(command);
                return Results.Ok(user);

            }).WithTags("USER").WithSummary("Find a user by id").WithOpenApi();


            app.MapPost("/v1/user", async (IMediator mediator, CreateUserRequest command) =>
            {

                var createdUser = await mediator.Send(command);
                var resp = new { User = createdUser };
                return Results.Ok(resp);

            }).WithTags("USER").WithSummary("Create a new user").WithOpenApi();

            app.MapPut("/v1/user/{id}", async (IMediator mediator, Guid id, PutUserRequest command) =>
            {

                var putUserRequest = new PutUserRequest(id, command.Username, command.Email, command.Role, command.IsDeleted);
                var user = await mediator.Send(putUserRequest);
                return Results.Ok(user);

            }).WithTags("USER").WithSummary("Update a user").WithOpenApi();

            app.MapDelete("/v1/user/{id}", async (IMediator mediator, Guid id) =>
           {

               var command = new DeleteUserRequest(id);

               var isDeleted = await mediator.Send(command);
               return Results.Ok(isDeleted);
               
           }).WithTags("USER").WithSummary("Delete a user").WithOpenApi().RequireAuthorization(IdentityData.AdminPolicy);

        }

    }
}
