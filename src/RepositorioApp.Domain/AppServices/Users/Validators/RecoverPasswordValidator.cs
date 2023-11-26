using FluentValidation;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public class RecoverPasswordValidator : AbstractValidator<RecoverPasswordCmd>
    {
        public RecoverPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(UserMessages._Email.RequiredError);

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(UserMessages._Email.InvalidError)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
        }
    }
}
