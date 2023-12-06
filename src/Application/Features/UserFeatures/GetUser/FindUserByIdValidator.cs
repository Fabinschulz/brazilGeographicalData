using FluentValidation;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetUser
{
    public sealed class FindUserByIdValidator : AbstractValidator<FindUserByIdRequest>
    {
        public FindUserByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .Must(id => IsGuidValid(id)).WithMessage("Id is not a valid GUID");
        }

        private bool IsGuidValid(Guid id)
        {
            return Guid.TryParse(id.ToString(), out _);
        }
    }
}
