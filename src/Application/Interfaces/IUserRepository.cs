using System.Security.Claims;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAuthenticatedUser(ClaimsPrincipal user);
    }
}
