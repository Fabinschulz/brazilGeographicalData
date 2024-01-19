using BrazilGeographicalData.src.Application.Common.BaseResponse;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserRequest : DeleteRequestBase<DeleteUserResponse>
    {
        public DeleteUserRequest(Guid id) : base(id) { }
    }
}
