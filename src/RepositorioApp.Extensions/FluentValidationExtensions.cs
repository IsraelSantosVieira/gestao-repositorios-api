using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
namespace RepositorioApp.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IValidator GetValidator(object obj)
        {
            var abstractValidatorType = typeof(AbstractValidator<>);
            var objType = obj.GetType();
            var genericType = abstractValidatorType.MakeGenericType(objType);
            var validatorType = FindValidatorType(objType.Assembly, genericType);
            return validatorType == null
                ? null
                : (IValidator)Activator.CreateInstance(validatorType);
        }

        public static Type FindValidatorType(Assembly assembly, Type genericType)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (genericType == null) throw new ArgumentNullException(nameof(genericType));
            return assembly.GetTypes().FirstOrDefault(t => t.IsSubclassOf(genericType));
        }

        public static ICollection<string> ErrorMessages(this IList<ValidationFailure> value)
        {
            return value.Select(x => x.ErrorMessage)?.ToList();
        }

        public static bool IsValid(this object value, out ICollection<string> errors)
        {
            errors = new List<string>();

            var validator = GetValidator(value);

            if (validator == null) return true;

            var validation = validator.Validate(new ValidationContext<object>(value));

            if (validation.IsValid) return true;

            foreach (var error in validation.Errors.Select(x => x.ErrorMessage))
            {
                errors.Add(error);
            }

            return false;
        }
    }
}
