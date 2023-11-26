using System;
using Newtonsoft.Json;
using RepositorioApp.Extensions;
namespace RepositorioApp.Domain.ValueObjects
{
    public class Phone
    {

        [JsonIgnore]
        public readonly bool IsValid;

        public Phone()
        {
            IsValid = Validate(string.Empty);
        }

        public Phone(string value)
        {
            IsValid = Validate(value ?? string.Empty);
        }

        /// <summary>
        ///     Area Code
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        ///     Number
        /// </summary>
        public string Number { get; set; }

        public string Formatted => ToString()?.Length switch
        {
            10 => Convert.ToUInt64(ToString()?.ToNumber()).ToString(@"## ####-####"),
            11 => Convert.ToUInt64(ToString()?.ToNumber()).ToString(@"## #####-####"),
            _ => ToString()
        };

        public string Value => ToString();

        private bool Validate(string value)
        {
            value = value.ToNumber();

            if (value.Length < 10)
            {
                return false;
            }

            AreaCode = value.Substring(0, 2);
            Number = value.Substring(2, value.Length - 2);

            return true;
        }

        public static implicit operator Phone(string value)
        {
            return new Phone(value);
        }

        public override string ToString()
        {
            return $"{AreaCode}{Number}";
        }
    }
}
