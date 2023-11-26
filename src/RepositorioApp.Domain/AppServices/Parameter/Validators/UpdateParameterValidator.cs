using FluentValidation;
using RepositorioApp.Domain.AppServices.Parameter.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Parameter.Validators
{
    public class UpdateParameterValidator : AbstractValidator<UpdateParameterCmd>
    {
        public UpdateParameterValidator()
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

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(ParameterMessages.IdIsEmpty);
        }
    }
}
