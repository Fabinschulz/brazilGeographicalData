using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Interfaces;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BrazilGeographicalData.src.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<User> Create(User user, string password)
        {
            await _context.Set<User>().Where(x => x.Username.ToLower() == user.Username).FirstOrDefaultAsync();

            var userCreated = await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();

            return userCreated.Entity;

        }
    }
}
