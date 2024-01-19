
using BrazilGeographicalData.src.Application.Common.BaseResponse;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.DeleteLocation
{
    public sealed class DeleteLocationResponse : DeleteResponseBase
    {
        public DeleteLocationResponse(bool isSuccess) : base(isSuccess) { }
    }
}
