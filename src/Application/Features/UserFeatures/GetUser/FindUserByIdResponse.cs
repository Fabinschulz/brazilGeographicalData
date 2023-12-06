namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser
{
    public class FindUserByIdResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }

    }
}
