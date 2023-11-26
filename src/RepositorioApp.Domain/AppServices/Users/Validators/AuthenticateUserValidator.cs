using System.Linq;
using FluentValidation;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCmd>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(UserMessages._Email.RequiredError);

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(UserMessages._Email.InvalidError)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.GrantType)
                .NotEmpty()
                .WithMessage(UserMessages._Authentication.GrantTypeRequiredError);

            RuleFor(x => x.GrantType)
                .Must(x => GrantTypes.All.Contains(x))
                .WithMessage(x => UserMessages._Authentication.GrantTypeInvalidError)
                .When(x => !string.IsNullOrWhiteSpace(x.GrantType));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(UserMessages._Password.RequiredError)
                .When(x => !string.IsNullOrWhiteSpace(x.GrantType) && x.GrantType == GrantTypes.Password);

            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage(UserMessages._RefreshToken.RequiredError)
                .When(x => !string.IsNullOrWhiteSpace(x.GrantType) && x.GrantType == GrantTypes.RefreshToken);
        }
    }
}
