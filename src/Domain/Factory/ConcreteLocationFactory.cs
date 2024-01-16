using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;

namespace BrazilGeographicalData.src.Domain.Factory
{
    public class ConcreteLocationFactory : ILocationFactory
    {
        public Location CreateLocation(int ibgeCode, string state, string city)
        {
            return new Location(ibgeCode, state, city);
        }
    }
}
