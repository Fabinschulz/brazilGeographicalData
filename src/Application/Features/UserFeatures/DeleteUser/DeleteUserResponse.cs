using System;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserResponse
    {
        public bool IsSuccess { get; }

        public DeleteUserResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
}
