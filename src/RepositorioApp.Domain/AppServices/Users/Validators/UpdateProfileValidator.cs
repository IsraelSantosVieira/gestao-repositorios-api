using FluentValidation;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCmd>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(UserMessages._FirstName.RequiredError);

            RuleFor(x => x.FirstName)
                .MaximumLength(255)
                .WithMessage(UserMessages._FirstName.MaxLengthError)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(255)
                .WithMessage(UserMessages._LastName.MaxLengthError)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));

            RuleFor(x => x.Phone)
                .MaximumLength(15)
                .WithMessage(UserMessages._Phone.MaxLengthError)
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage(UserMessages._Phone.RequiredError);
        }
    }
}
