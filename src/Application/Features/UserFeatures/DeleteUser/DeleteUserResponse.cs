using BrazilGeographicalData.src.Application.Common.BaseResponse;
using System;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserResponse : DeleteResponseBase
    {
        public DeleteUserResponse(bool isSuccess) : base(isSuccess) { }
    }
}
