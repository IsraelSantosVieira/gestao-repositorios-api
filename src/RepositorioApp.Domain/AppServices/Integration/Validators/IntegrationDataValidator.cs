using FluentValidation;
using RepositorioApp.Domain.AppServices.Integration.Commands;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Utilities.Security;
namespace RepositorioApp.Domain.AppServices.Integration.Validators
{
    public class IntegrationDataValidator : AbstractValidator<IntegrationDataCmd>
    {
        public IntegrationDataValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .Length(IntegrationUtils.INTEGRATION_CODE_SIZE)
                .WithMessage(IntegrationMessages.IdRequired);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(IntegrationMessages.EmailRequired);
        }
    }
}
