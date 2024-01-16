using BrazilGeographicalData.src.Domain.Common;

namespace BrazilGeographicalData.src.Domain.Entities
{
    public class Location : BaseEntity
    {
        public int IBGECode { get; set; }
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        // Default parameterless constructor (required for EF Core)
        private Location() { }
        
        public Location(int ibgeCode, string state, string city)
        {
            IBGECode = ibgeCode;
            State = state;
            City = city;
        }

    }
}
