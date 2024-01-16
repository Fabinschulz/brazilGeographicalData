using FluentValidation;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.CreateLocation
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationRequest>
    {
        public CreateLocationValidator()
        {
            RuleFor(x => x.IBGECode)
                .NotEmpty().WithMessage("Código IBGE é obrigatório.")
                .GreaterThan(0).WithMessage("Código IBGE deve ser maior que zero.")
                .LessThanOrEqualTo(9999999).WithMessage("Código IBGE deve ser menor ou igual a 9999999.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("O campo Estado é obrigatório.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("O campo Cidade é obrigatório.");
        }
    }
}
