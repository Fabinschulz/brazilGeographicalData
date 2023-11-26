using FluentValidation;

namespace BrazilGeographicalData.src.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Nome de usuário é obrigatório.").ChildRules(username =>
            {
                username.RuleFor(x => x).MinimumLength(3).WithMessage("Nome de usuário deve ter no mínimo 3 caracteres.");
                username.RuleFor(x => x).MaximumLength(50).WithMessage("Nome de usuário deve ter no máximo 50 caracteres.");
            });
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório.").ChildRules(email =>
            {
                email.RuleFor(x => x).EmailAddress().WithMessage("Email inválido.");
                email.RuleFor(x => x).MaximumLength(50).WithMessage("Email deve ter no máximo 50 caracteres.");
            });
            RuleFor(x => x.Password).NotEmpty().WithMessage("Senha é obrigatória.").ChildRules(password =>
            {
                password.RuleFor(x => x).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Senha deve ter no mínimo 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
            });
        }
    }
}
