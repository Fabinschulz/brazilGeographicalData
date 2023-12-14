using System.Security.Claims;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetAuthenticatedUser(ClaimsPrincipal user);
        Task<ListDataPagination<User>> GetAll(int Page, int Size, string? Username, string? Email, bool IsDeleted, string? OrderBy, string Role);
    }
}
