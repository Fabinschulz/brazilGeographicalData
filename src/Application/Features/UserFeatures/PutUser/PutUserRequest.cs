using BrazilGeographicalData.src.Domain.Entities;
using MediatR;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.PutUser
{
    public sealed record PutUserRequest(Guid id, string Username, string Email, string Role, bool IsDeleted) : IRequest<PutUserResponse>;

}
