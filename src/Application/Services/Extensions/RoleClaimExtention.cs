using BrazilGeographicalData.src.Domain.Entities;
using System.Security.Claims;

namespace BrazilGeographicalData.src.Application.Services.Extensions
{
    public static class RoleClaimExtention
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                    new ("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),

            };
            return result;
        }
    }
}
