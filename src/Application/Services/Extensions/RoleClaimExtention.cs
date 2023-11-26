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
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),

            };
            return result;
        }
    }
}
