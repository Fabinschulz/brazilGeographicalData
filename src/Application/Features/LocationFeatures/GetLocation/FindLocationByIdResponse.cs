namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetLocation
{
    public sealed record FindLocationByIdResponse
    {
        public Guid Id { get; set; }
        public int IBGECode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsDeleted { get; set; }
    }
}
