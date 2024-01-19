using BrazilGeographicalData.src.Domain.Interfaces;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.DeleteLocation
{
    public sealed class DeleteLocationHandler : IRequestHandler<DeleteLocationRequest, DeleteLocationResponse>
    {
        private readonly ILocationRepository _locationRepository;

        public DeleteLocationHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<DeleteLocationResponse> Handle(DeleteLocationRequest request, CancellationToken cancellationToken)
        {
            var isDeleted = await DeleteLocationInRepository(request.Id);
            return new DeleteLocationResponse(isDeleted);
        }

        private async Task<bool> DeleteLocationInRepository(Guid id)
        {
            return await _locationRepository.Delete(id);
        }

    }
}
