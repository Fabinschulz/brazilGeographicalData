using AutoMapper;
using BrazilGeographicalData.src.Application.Services.TokenServices;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using BrazilGeographicalData.src.Infra.Repositories;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly IValidator<CreateUserRequest> _validator;

        public CreateUserHandler(IMapper mapper, UserRepository userRepository, IUserFactory userFactory, IValidator<CreateUserRequest> validator)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userFactory = userFactory;
            _validator = validator;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);

            var createUser = CreateUserFromRequest(request);
            var createdUser = await CreateUserInRepository(createUser);
            var userResponse = MapUserToResponse(createdUser);
            return userResponse;

        }

        private async Task ValidateRequest(CreateUserRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private User CreateUserFromRequest(CreateUserRequest request)
        {
            return _userFactory.CreateUser(request.Username, request.Email, request.Password);
        }

        private async Task<User> CreateUserInRepository(User user)
        {
            return await _userRepository.Create(user);
        }

        private CreateUserResponse MapUserToResponse(User user)
        {
            return _mapper.Map<CreateUserResponse>(user);
        }

    }
}
