using BrazilGeographicalData.src.Application.Common.CQBase;
using BrazilGeographicalData.src.Domain.Entities;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser
{
    public class GetAllUserResponse : GetAllResponseBase<User>
    {
        public GetAllUserResponse(ListDataPagination<User> entity) : base(entity)
        {
        }
    }
}
