namespace BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserResponse
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
