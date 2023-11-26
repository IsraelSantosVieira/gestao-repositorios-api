using FluentValidation;
using RepositorioApp.Domain.AppServices.Parameter.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Parameter.Validators
{
    public class CreateParameterValidator : AbstractValidator<CreateParameterCmd>
    {
        public CreateParameterValidator()
        {
            RuleFor(x => x.Group)
                .NotEmpty()
                .WithMessage(ParameterMessages.ContentIsEmpty);
            
            RuleFor(x => x.Transaction)
                .NotEmpty()
                .WithMessage(ParameterMessages.ContentIsEmpty);

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage(ParameterMessages.ValueIsEmpty);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(ParameterMessages.DescriptionIsEmpty);
        }
    }
}
