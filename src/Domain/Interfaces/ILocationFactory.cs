using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Domain.Interfaces
{
    public interface ILocationFactory
    {
        Location CreateLocation(int ibgeCode, string state, string city);
    }

}
