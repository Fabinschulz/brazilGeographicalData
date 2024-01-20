using FluentValidation;

namespace BrazilGeographicalData.src.Application.Features.LocationFeatures.GetAllLocation
{
    public sealed class GetAllLocationValidator : AbstractValidator<GetAllLocationRequest>
    {
        public GetAllLocationValidator()
        {
            RuleFor(x => x.Page)
               .GreaterThanOrEqualTo(0).WithMessage("A página precisa ser maior ou igual a 0");

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size é obrigatório")
                .GreaterThan(0).WithMessage("Size precisa ser maior que 0");

        }

    }
}
