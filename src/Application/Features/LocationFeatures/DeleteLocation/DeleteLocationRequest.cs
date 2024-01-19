using BrazilGeographicalData.src.Application.Common.BaseResponse;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.DeleteLocation
{
    public sealed class DeleteLocationRequest : DeleteRequestBase<DeleteLocationResponse>
    {
        public DeleteLocationRequest(Guid id) : base(id) { }
    }
   
}
