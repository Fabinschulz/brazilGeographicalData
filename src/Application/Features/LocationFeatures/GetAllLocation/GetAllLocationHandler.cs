using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetAllLocation
{

    public sealed class GetAllLocationHandler : IRequestHandler<GetAllLocationRequest, GetAllLocationResponse>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IValidator<GetAllLocationRequest> _validator;
        private readonly IMapper _mapper;

        public GetAllLocationHandler(ILocationRepository locationRepository, IValidator<GetAllLocationRequest> validator, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _validator = validator;
            _mapper = mapper;

        }

        public async Task<GetAllLocationResponse> Handle(GetAllLocationRequest request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var location = await _locationRepository.GetAll(request.Page, request.Size, request.IBGECode, request.State, request.City, request.OrderBy);
            var locationMapped = _mapper.Map<ListDataPagination<Location>>(location);
            return new GetAllLocationResponse(locationMapped);

        }
    }
}
