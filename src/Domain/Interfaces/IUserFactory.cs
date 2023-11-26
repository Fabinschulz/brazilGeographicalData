using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Domain.Interfaces
{
    public interface IUserFactory
    {
        User CreateUser(string username, string email, string password);
    }
}
