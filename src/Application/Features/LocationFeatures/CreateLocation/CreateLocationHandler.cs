using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Repositories;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation
{
    public class CreateLocationHandler : IRequestHandler<CreateLocationRequest, CreateLocationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly ILocationFactory _locationFactory;
        private readonly IValidator<CreateLocationRequest> _validator;

        public CreateLocationHandler(IMapper mapper, LocationRepository locationRepository, ILocationFactory locationFactory, IValidator<CreateLocationRequest> validator)
        {
            _mapper = mapper;
            _locationRepository = locationRepository;
            _locationFactory = locationFactory;
            _validator = validator;
        }

        public async Task<CreateLocationResponse> Handle(CreateLocationRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var createLocation = CreateLocationFromRequest(request);
            var createdLocation = await CreateLocationInRepository(createLocation);

            var resp = MapLocationToResponse(createdLocation);
            return resp;

        }

        private async Task ValidateRequest(CreateLocationRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private Location CreateLocationFromRequest(CreateLocationRequest request)
        {
            return _locationFactory.CreateLocation(request.IBGECode, request.State, request.City);
        }

        private async Task<Location> CreateLocationInRepository(Location location)
        {
            return await _locationRepository.Create(location);
        }


        private CreateLocationResponse MapLocationToResponse(Location location)
        {
            return _mapper.Map<CreateLocationResponse>(location);
        }
    }
}
