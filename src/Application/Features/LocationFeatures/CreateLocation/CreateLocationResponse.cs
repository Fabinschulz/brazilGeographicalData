using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation
{
    public sealed record CreateLocationResponse
    {
        public Guid Id { get; set; }
        public int IBGECode { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public static implicit operator CreateLocationResponse(Location location)
        {
            return new CreateLocationResponse
            {
                Id = location.Id,
                IBGECode = location.IBGECode,
                State = location.State,
                City = location.City
            };
        }
    }
}
