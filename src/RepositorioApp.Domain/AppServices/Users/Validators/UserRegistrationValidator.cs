using System.Linq;
using FluentValidation;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public static class UserRegistrationValidatorExtensions
    {
        public static void RegisterRules<T>(this AbstractValidator<T> validator)
            where T : UserRegistrationCmd
        {
            validator.RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => UserMessages._Email.RequiredError);

            validator.RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(x => UserMessages._Email.InvalidError)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            validator.RuleFor(x => x.Email)
                .MaximumLength(1025)
                .WithMessage(x => UserMessages._Email.MaxLengtheError)
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            validator.RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(x => UserMessages._FirstName.RequiredError);

            validator.RuleFor(x => x.FirstName)
                .MaximumLength(255)
                .WithMessage(x => UserMessages._FirstName.MaxLengthError)
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

            validator.RuleFor(x => x.LastName)
                .MaximumLength(255)
                .WithMessage(x => UserMessages._LastName.MaxLengthError)
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));
        }

        public static IRuleBuilderOptionsConditions<T, UserRegistrationCmd> ValidateUserRegistration<T>(this IRuleBuilder<T, UserRegistrationCmd> ruleBuilder)
        {
            return ruleBuilder.Custom((user, context) =>
            {
                if (user == null) return;

                var result = new UserRegistrationValidator().Validate(user);

                if (!result.Errors.Any()) return;

                foreach (var error in result.Errors)
                {
                    context.AddFailure(error.ErrorMessage);
                }
            });
        }
    }

    public class UserRegistrationValidator : AbstractValidator<UserRegistrationCmd>
    {
        public UserRegistrationValidator()
        {
            this.RegisterRules();
        }
    }
}
