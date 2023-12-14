using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser
{
    public class ConcreteUserFactory : IUserFactory
    {
        public User CreateUser(string username, string email, string password)
        {
            return new User(username, email, password);
        }
    }
}
