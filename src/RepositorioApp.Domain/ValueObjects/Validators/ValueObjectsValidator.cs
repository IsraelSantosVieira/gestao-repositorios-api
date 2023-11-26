using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using RepositorioApp.CrossCutting.Models;
using RepositorioApp.Extensions;
namespace RepositorioApp.Domain.ValueObjects.Validators
{
    public static class ValueObjectsValidator
    {
        public static IRuleBuilderOptionsConditions<T, FileUpload> ValidateFileUpload<T>(this IRuleBuilder<T, FileUpload> ruleBuilder,
            bool required = true,
            string fileRequiredError = "Arquivo é obrigatório",
            string bufferRequiredError = "Buffer é obrigatório",
            string contentTypeRequiredError = "Content Type é obrigatório",
            string contentTypeInvalidError = "Content Type é inválido",
            IEnumerable<string> contentTypeExtensions = null)
        {
            return ruleBuilder.Custom((fileUpload, context) =>
            {
                if (!required && fileUpload == null)
                {
                    return;
                }

                if (required && fileUpload == null)
                {
                    context.AddFailure(fileRequiredError);
                    return;
                }

                if (fileUpload.Buffer?.Length is null or 0)
                {
                    context.AddFailure(bufferRequiredError);
                }

                if (string.IsNullOrEmpty(fileUpload.ContentType))
                {
                    context.AddFailure(contentTypeRequiredError);
                }

                var contentTypeExtensionsArray = contentTypeExtensions?.ToList() ?? new List<string>(0);

                if (
                    !string.IsNullOrEmpty(fileUpload.ContentType) &&
                    contentTypeExtensionsArray.Any() &&
                    (
                    !fileUpload.ContentType.TryGetExtensionFromMimeType(out var ext) ||
                    !contentTypeExtensionsArray.Contains(fileUpload.ContentType)
                    )
                )
                {
                    context.AddFailure(contentTypeInvalidError);
                }
            });
        }
    }
}
