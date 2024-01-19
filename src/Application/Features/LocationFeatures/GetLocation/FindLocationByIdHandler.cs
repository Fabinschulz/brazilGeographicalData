using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Domain.Interfaces;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetLocation
{
    public sealed class FindLocationByIdHandler : IRequestHandler<FindLocationByIdRequest, FindLocationByIdResponse>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public FindLocationByIdHandler(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<FindLocationByIdResponse> Handle(FindLocationByIdRequest request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetById(request.Id);
            if (location == null)
            {
                throw new NotFoundException("Localition not found");
            }
            return _mapper.Map<FindLocationByIdResponse>(location);

        }
    }
}
