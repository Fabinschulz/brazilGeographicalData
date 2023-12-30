using BrazilGeographicalData.src.Domain.Entities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.GetAllUser
{
    public sealed class GetAllUserValidator : AbstractValidator<GetAllUserRequest>
    {
        public GetAllUserValidator()
        {
            RuleFor(x => x.Page)
               .GreaterThanOrEqualTo(0).WithMessage("A página precisa ser maior ou igual a 0");


            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size é obrigatório")
                .GreaterThan(0).WithMessage("Size precisa ser maior que 0");


            RuleFor(x => x.Email)
                .Must(email => IsEmailValid(email)).WithMessage("Email inválido.");

        }

        private bool IsOrderByValid(string orderBy)
        {
            return orderBy == "username" || orderBy == "email" || orderBy == "role";
        }

        private bool IsRoleValid(string role)
        {
            var admin = IdentityData.AdminPolicy;
            var user = IdentityData.UserPolicy;
            return role == admin || role == user;
        }

        private bool IsEmailValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
