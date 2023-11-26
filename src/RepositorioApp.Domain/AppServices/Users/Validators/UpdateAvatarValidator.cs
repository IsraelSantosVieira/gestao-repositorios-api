using System.Linq;
using FluentValidation;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Messages;
namespace RepositorioApp.Domain.AppServices.Users.Validators
{
    public class UpdateAvatarValidator : AbstractValidator<UpdateAvatarCmd>
    {
        public UpdateAvatarValidator()
        {
            RuleFor(x => x.Image)
                .Custom((file, context) =>
                {
                    if (file == null)
                    {
                        context.AddFailure(UserMessages._Avatar.ImageRequiredError);
                        return;
                    }

                    if (file.Buffer == null)
                    {
                        context.AddFailure(UserMessages._Avatar.BufferRequiredError);
                    }

                    if (file.Buffer is { Length: <= 0 })
                    {
                        context.AddFailure(UserMessages._Avatar.BufferRequiredError);
                    }

                    if (string.IsNullOrWhiteSpace(file.ContentType))
                    {
                        context.AddFailure(UserMessages._Avatar.ContentTypeRequiredError);
                    }

                    if (!string.IsNullOrWhiteSpace(file.ContentType) && !AppConstants.ImagesContentTypes.Contains(file.ContentType))
                    {
                        context.AddFailure(UserMessages._Avatar.ContentTypeInvalidError);
                    }
                });
        }
    }
}
