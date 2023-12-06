using AutoMapper;
using BrazilGeographicalData.src.Application.Common.Exceptions;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser
{
    public sealed class FindUserByIdHandler: IRequestHandler<FindUserByIdRequest, FindUserByIdResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<FindUserByIdRequest> _validator;

        public FindUserByIdHandler(IUserRepository userRepository, IMapper mapper, IValidator<FindUserByIdRequest> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<FindUserByIdResponse> Handle(FindUserByIdRequest request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);
            var user = await _userRepository.GetById(request.Id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return _mapper.Map<FindUserByIdResponse>(user);

        }
    }
}
