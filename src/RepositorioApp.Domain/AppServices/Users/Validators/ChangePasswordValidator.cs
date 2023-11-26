using FluentValidation;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCmd>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .WithMessage(UserMessages._Password.CurrentRequiredError);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(UserMessages._Password.NewRequridError);

            RuleFor(x => x.NewPasswordConfirmation)
                .NotEmpty()
                .WithMessage(UserMessages._Password.NewConfirmationRequiredError);

            RuleFor(x => x)
                .Must(x => x.NewPassword == x.NewPasswordConfirmation)
                .WithMessage(UserMessages._Password.NotEqualsError)
                .When(x => !string.IsNullOrWhiteSpace(x.NewPassword) && !string.IsNullOrWhiteSpace(x.NewPasswordConfirmation));
        }
    }
}
