using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.PutUser
{
    public sealed class PutUserHandler : IRequestHandler<PutUserRequest, PutUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PutUserHandler> _logger;
        private readonly IValidator<PutUserRequest> _validator;

        public PutUserHandler(IUserRepository userRepository, IMapper mapper, ILogger<PutUserHandler> logger, IValidator<PutUserRequest> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<PutUserResponse> Handle(PutUserRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var user = await _userRepository.GetById(request.id);
            EnsureUserExists(user, request.id);

            UpdateUserProperties(user, request);
            await _userRepository.Update(user);

            var userResponse = MapToUserResponse(user);
            return userResponse;
        }

        private async Task ValidateRequest(PutUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private void EnsureUserExists(User user, Guid userId)
        {
            if (user == null)
            {
                string errorMessage = $"Usuário com id: {userId} não foi encontrado no banco de dados.";
                _logger.LogError(errorMessage);
                throw new NotFoundException(errorMessage);
            }
        }

        private void UpdateUserProperties(User user, PutUserRequest request)
        {
            user.Username = request.Username;
            user.Email = request.Email;
            user.Role = request.Role;
            user.IsDeleted = request.IsDeleted;
        }

        private PutUserResponse MapToUserResponse(User user)
        {
            return _mapper.Map<PutUserResponse>(user);
        }


    }
}
