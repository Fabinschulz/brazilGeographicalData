using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> Create(User user, string password);
    }
}
