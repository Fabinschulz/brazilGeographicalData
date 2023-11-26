using BrazilGeographicalData.src.Domain.Common;

namespace BrazilGeographicalData.src.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public  bool IsDeleted { get; set; } = false;

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

    }
}
