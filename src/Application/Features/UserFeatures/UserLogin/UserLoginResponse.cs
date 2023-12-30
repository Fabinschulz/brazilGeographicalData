using BrazilGeographicalData.src.Application.Services.TokenServices;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.UserLogin
{
    public sealed record UserLoginResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
       

        public UserLoginResponse() { }

        public UserLoginResponse(User user, string token)
        {
            Username = user.Username;
            Email = user.Email;
            Id = user.Id;
            Token = token;

        }
    }
}
