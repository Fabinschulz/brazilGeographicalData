using FluentValidation;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.PutUser
{
    public sealed class PutUserValidator : AbstractValidator<PutUserRequest>
    {
        public PutUserValidator()
        {
            RuleFor(x => x.id).NotEmpty();
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Role).NotEmpty().MaximumLength(50);
        }
    }
}
