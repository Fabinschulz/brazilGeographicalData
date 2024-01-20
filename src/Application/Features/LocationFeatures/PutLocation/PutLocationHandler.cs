using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.PutLocation
{
    public sealed class PutLocationHandler : IRequestHandler<PutLocationRequest, PutLocationResponse>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PutLocationHandler> _logger;
        private readonly IValidator<PutLocationRequest> _validator;

        public PutLocationHandler(ILocationRepository locationRepository, IMapper mapper, ILogger<PutLocationHandler> logger, IValidator<PutLocationRequest> validator)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<PutLocationResponse> Handle(PutLocationRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var locationId = request.id;
            var location = await FindLocationById(locationId);
            EnsureLocationExists(location, request.id);
            
            UpdateLocationProperties(location, request);
            await _locationRepository.Update(location);
            
            var locationResponse = MapToLocationResponse(location);
            return locationResponse;

        }

        private async Task ValidateRequest(PutLocationRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private void EnsureLocationExists(Location location, Guid locationId)
        {
            if (location == null)
            {
                string errorMessage = $"A Localização com id: {locationId} não foi encontrado no banco de dados.";
                _logger.LogError(errorMessage);
                throw new NotFoundException(errorMessage);
            }
        }

        private async Task<Location> FindLocationById(Guid locationId)
        {
            var location = await _locationRepository.GetById(locationId);
            return location;
        }

        private async Task<Location> PutLocationInRepository(Location location)
        {
            var putLocation = await _locationRepository.Update(location);
            return putLocation;
        }

        private PutLocationResponse MapToLocationResponse(Location location)
        {
            return _mapper.Map<PutLocationResponse>(location);
        }

        private void UpdateLocationProperties(Location location, PutLocationRequest request)
        {
            location.IBGECode = request.IBGECode;
            location.State = request.State;
            location.City = request.City;
        }
    }
}
