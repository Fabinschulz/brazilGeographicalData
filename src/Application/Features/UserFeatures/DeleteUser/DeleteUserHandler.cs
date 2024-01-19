using AutoMapper;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.DeleteUser
{
    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserRequest, DeleteUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<DeleteUserRequest> _validator;

        public DeleteUserHandler(IUserRepository userRepository, IValidator<DeleteUserRequest> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            var isDeleted = await DeleteUserInRepository(request.Id);
            return new DeleteUserResponse(isDeleted);
        }

        private async Task ValidateRequest(DeleteUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private async Task<bool> DeleteUserInRepository(Guid id)
        {
            return await _userRepository.Delete(id);
        }

    }
}
