using FluentValidation;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCmd>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(UserMessages._ResetPassword.CodeRequiredError);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(UserMessages._Email.RequiredError);

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(UserMessages._Email.InvalidError)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(UserMessages._Password.NewRequridError);

            RuleFor(x => x.PasswordConfirm)
                .NotEmpty()
                .WithMessage(UserMessages._Password.NewConfirmationRequiredError);

            RuleFor(x => x)
                .Must(x => x.NewPassword == x.PasswordConfirm)
                .WithMessage(UserMessages._Password.NotEqualsError)
                .When(x => !string.IsNullOrWhiteSpace(x.NewPassword) && !string.IsNullOrWhiteSpace(x.PasswordConfirm));
        }
    }
}
