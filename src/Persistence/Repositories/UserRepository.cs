using BrazilGeographicalData.src.Application.Interfaces;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<User> GetAuthenticatedUser(ClaimsPrincipal user)
        {
            var authenticatedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Identity.Name);
            return authenticatedUser;
        }

    }
}
