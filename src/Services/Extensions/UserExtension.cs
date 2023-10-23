using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Infra.Repositories;
using BrazilGeographicalData.src.Services.Token;
using System.Security.Claims;

namespace BrazilGeographicalData.src.Services.Extensions
{
    public static class UserExtension
    {
        public static void MapUserEndpoints(this WebApplication app)
        {

            app.MapGet("/v1/user/authenticated", async (UserRepository _userRepository, ClaimsPrincipal user) =>
            {
                try
                {
                    var authenticatedUser = await _userRepository.GetAuthenticatedUser(user);
                    return Results.Ok(authenticatedUser);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            }).RequireAuthorization("Admin");

            app.MapGet("/v1/user", async (UserRepository _userRepository, int page, int size, string? searchString, string? email, bool isDeleted, string? orderBy) =>
            {
                try
                {
                    var users = await _userRepository.GetAll(page, size, searchString, email, isDeleted, orderBy);
                    return Results.Ok(users);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            });

            app.MapGet("/v1/user/{id}", async (UserRepository _userRepository, Guid id) =>
            {
                try
                {
                    var user = await _userRepository.GetById(id);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            });

            app.MapPost("/v1/user/login", async (UserRepository _userRepository, CreateUserRequest request) =>
            {
                try
                {

                    var user = new User
                    {
                        Email = request.Email,
                        Username = request.Username,
                        Role = request.Role,
                        Password = request.Password,
                        IsDeleted = false
                    };

                    var createdUser = await _userRepository.Create(user);

                    if (createdUser == null)
                    {
                        throw new NotFoundException("Username or password is incorrect");
                    }

                    var token = TokenService.GenerateToken(createdUser);
                    createdUser.Password = "";

                    return Results.Ok(new { user = createdUser, token = token });
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            });

            app.MapPost("/v1/user", async (UserRepository _userRepository, User model) =>
            {
                try
                {
                    var user = await _userRepository.Create(model);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            });

            app.MapPut("/v1/user/{id}", async (UserRepository _userRepository, Guid id, User model) =>
            {
                try
                {
                    var user = await _userRepository.GetById(id);
                    if (user == null)
                    {
                        throw new NotFoundException("Usuário não encontrado.");
                    }

                    await _userRepository.Update(model);
                    await _userRepository.SaveChangesAsync();
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            });

            app.MapDelete("/v1/user/{id}", async (UserRepository _userRepository, Guid id) =>
            {
                try
                {
                    var user = await _userRepository.GetById(id);
                    if (user != null)
                    {
                        if (user.IsDeleted)
                        {
                            return Results.NoContent();
                        }

                        user.IsDeleted = true;
                        await _userRepository.SaveChangesAsync();
                    }
                    else
                    {
                        throw new NotFoundException("Usuário não encontrado.");
                    }

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            });
        }

    }
}
