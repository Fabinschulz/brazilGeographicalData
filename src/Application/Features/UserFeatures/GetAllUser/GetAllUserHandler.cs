using AutoMapper;
using BrazilGeographicalData.src.Domain.Entities;
using BrazilGeographicalData.src.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser
{
    public sealed class GetAllUserHandler: IRequestHandler<GetAllUserRequest, GetAllUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<GetAllUserRequest> _validator;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUserRepository userRepository, IValidator<GetAllUserRequest> validator, IMapper mapper)
        {
            _userRepository = userRepository;
            _validator = validator;
            _mapper = mapper;

        }

        public async Task<GetAllUserResponse> Handle(GetAllUserRequest request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            var users = await _userRepository.GetAll(request.Page, request.Size, request.Username, request.Email, request.IsDeleted, request.OrderBy, request.Role);
            var usersMapped = _mapper.Map<ListDataPagination<User>>(users);
            return new GetAllUserResponse(usersMapped);
           
        }
    }
}
