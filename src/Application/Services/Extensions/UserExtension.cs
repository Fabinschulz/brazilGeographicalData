using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser;
using BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser;
using BrazilGeographicalData.src.Application.Services.TokenServices;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            // app.MapPost("/v1/user/login", async (UserRepository _userRepository, CreateUserRequest request) =>
            // {
            //     try
            //     {

            //         var user = new User
            //         {
            //             Email = request.Email,
            //             Password = request.Password,
            //         };

            //         var createdUser = await _userRepository.Create(user);

            //         if (createdUser == null)
            //         {
            //             throw new NotFoundException("Username or password is incorrect");
            //         }

            //         var token = TokenService.GenerateToken(createdUser);
            //         createdUser.Password = "";

            //         return Results.Ok(new { user = createdUser, token });
            //     }
            //     catch (Exception ex)
            //     {
            //         throw new BadRequestException(ex.Message);
            //     }
            // });

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

            //     app.MapPut("/v1/user/{id}", async (CreateUserRequest request, Guid id, User model) =>
            //     {
            //         try
            //         {
            //             var user = await _userRepository.GetById(id);
            //             if (user == null)
            //             {
            //                 throw new NotFoundException("Usuário não encontrado.");
            //             }
            //             await _userRepository.Update(model);
            //             await _userRepository.SaveChangesAsync();
            //             return Results.Ok(user);
            //         }
            //         catch (Exception ex)
            //         {
            //             throw new BadRequestException(ex.Message);
            //         }
            //     });

            //     app.MapDelete("/v1/user/{id}", async (UserRepository _userRepository, Guid id) =>
            //     {
            //         try
            //         {
            //             var user = await _userRepository.GetById(id);
            //             if (user != null)
            //             {
            //                 if (user.IsDeleted)
            //                 {
            //                     return Results.NoContent();
            //                 }

            //                 user.IsDeleted = true;
            //                 await _userRepository.SaveChangesAsync();
            //             }
            //             else
            //             {
            //                 throw new NotFoundException("Usuário não encontrado.");
            //             }

            //             return Results.NoContent();
            //         }
            //         catch (Exception ex)
            //         {
            //             throw new BadRequestException(ex.Message);
            //         }
            //     });
        }

    }
}
