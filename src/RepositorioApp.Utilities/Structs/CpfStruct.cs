using System;
using System.Text.RegularExpressions;
namespace RepositorioApp.Utilities.Structs
{
    public struct CpfStruct
    {
        private readonly string _value;

        public readonly bool IsValid;

        public CpfStruct(string value)
        {
            value ??= "";
            value = Regex.Replace(value, @"[^0-9]", "");
            _value = value;

            var position = 0;
            var totalDigit1 = 0;
            var totalDigit2 = 0;
            var dv1 = 0;
            var dv2 = 0;

            var identicalDigits = true;
            var lastDigit = -1;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    var digit = c - '0';
                    if (position != 0 && lastDigit != digit)
                    {
                        identicalDigits = false;
                    }

                    lastDigit = digit;
                    if (position < 9)
                    {
                        totalDigit1 += digit * (10 - position);
                        totalDigit2 += digit * (11 - position);
                    }
                    else if (position == 9)
                    {
                        dv1 = digit;
                    }
                    else if (position == 10)
                    {
                        dv2 = digit;
                    }

                    position++;
                }
            }

            if (position > 11)
            {
                IsValid = false;
                return;
            }

            if (identicalDigits)
            {
                IsValid = false;
                return;
            }

            var digit1 = totalDigit1 % 11;
            digit1 = digit1 < 2
                ? 0
                : 11 - digit1;

            if (dv1 != digit1)
            {
                IsValid = false;
                return;
            }

            totalDigit2 += digit1 * 2;
            var digit2 = totalDigit2 % 11;
            digit2 = digit2 < 2
                ? 0
                : 11 - digit2;

            IsValid = dv2 == digit2;
        }

        public static implicit operator CpfStruct(string value)
        {
            return new CpfStruct(value);
        }

        public override string ToString()
        {
            return _value;
        }

        public string Format()
        {
            return string.IsNullOrWhiteSpace(_value)
                ? string.Empty
                : Convert.ToUInt64(_value).ToString(@"000\.000\.000\-00");
        }
    }
}
