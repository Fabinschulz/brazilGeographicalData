namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.PutLocation
{
    public sealed record PutLocationResponse
    {
        public Guid id { get; set; }
        public int IBGECode { get; set; }
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
