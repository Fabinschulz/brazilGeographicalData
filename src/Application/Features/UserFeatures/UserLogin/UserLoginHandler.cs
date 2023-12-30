using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.UserLogin
{
    public sealed class UserLoginHandler : IRequestHandler<UserLoginRequest, UserLoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserLoginRequest> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserLoginHandler> _logger;

        public UserLoginHandler(IUserRepository userRepository, IValidator<UserLoginRequest> validator, IMapper mapper, ILogger<UserLoginHandler> logger)
        {
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserLoginResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
        {
            await ValidateRequest(request);
            var user = await GetUserFromRepository(request);
            var userResponse = MapUserToResponse(user);
            return userResponse;
        }

        private async Task ValidateRequest(UserLoginRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);
        }

        private async Task<LoggedUser> GetUserFromRepository(UserLoginRequest request)
        {
            try
            {
                var user = await _userRepository.Login(request.Email, request.Password);
                if (user != null)
                {
                    _logger.LogInformation($"Login do usuário: {request.Email} foi realizado com sucesso.");
                    return user;
                }
                else
                {
                    _logger.LogInformation($"Login do usuário: {request.Email} falhou devido a credenciais inválidas.");
                    throw new BadRequestException(new[] { "Credenciais de login inválidas." });
                }
            }
            catch (NotFoundException ex)
            {
                _logger.LogError($"Erro ao tentar fazer login do usuário: {ex.Message}");
                throw;
            }
            catch (InternalServerErrorException ex)
            {
                _logger.LogError($"Erro ao tentar fazer login do usuário: {ex.Message}");

                throw new InternalServerErrorException("Ocorreu um erro interno ao processar a requisição.");
            }
        }


        private UserLoginResponse MapUserToResponse(LoggedUser user)
        {
            return _mapper.Map<UserLoginResponse>(user);
        }

    }
}
