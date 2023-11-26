using AutoMapper;
using BrazilGeographicalData.src.Application.Services.TokenServices;
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
            _validator.ValidateAndThrow(request);
            var createUser = _userFactory.CreateUser(request.Username, request.Email, request.Password);
            var createdUser = await _userRepository.Create(createUser);

            var token = TokenService.GenerateToken(createdUser);
            createdUser.Password = "";
            var userResponse = _mapper.Map<CreateUserResponse>(createdUser);
            userResponse.Token = token;
            return userResponse;

        }

    }
}
