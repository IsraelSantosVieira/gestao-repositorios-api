using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
namespace RepositorioApp.Domain.ValueObjects
{

    public class Cnpj
    {
        private static readonly int[] Multiplier1 =
        {
            5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2
        };
        private static readonly int[] Multiplier2 =
        {
            6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2
        };

        [JsonIgnore]
        public readonly bool IsValid;

        public Cnpj(string number)
        {
            number ??= "";
            number = Regex.Replace(number, @"[^0-9]", "");
            Number = number;

            var identicalDigits = true;
            var lastDigit = -1;
            var position = 0;
            var totalDigit1 = 0;
            var totalDigit2 = 0;

            foreach (var c in Number)
            {
                if (char.IsDigit(c))
                {
                    var digit = c - '0';
                    if (position != 0 && lastDigit != digit)
                    {
                        identicalDigits = false;
                    }

                    lastDigit = digit;
                    if (position < 12)
                    {
                        totalDigit1 += digit * Multiplier1[position];
                        totalDigit2 += digit * Multiplier2[position];
                    }
                    else if (position == 12)
                    {
                        var dv1 = totalDigit1 % 11;
                        dv1 = dv1 < 2
                            ? 0
                            : 11 - dv1;

                        if (digit != dv1)
                        {
                            IsValid = false;
                            return;
                        }

                        totalDigit2 += dv1 * Multiplier2[12];
                    }
                    else if (position == 13)
                    {
                        var dv2 = totalDigit2 % 11;

                        dv2 = dv2 < 2
                            ? 0
                            : 11 - dv2;

                        if (digit != dv2)
                        {
                            IsValid = false;
                            return;
                        }
                    }

                    position++;
                }
            }

            IsValid = position == 14 && !identicalDigits;
        }
        public string Number { get; set; }

        public string Formatted
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Number)) return string.Empty;

                var number = Regex.Replace(Number, @"[^0-9]", string.Empty);

                return Convert.ToUInt64(number).ToString(@"00\.000\.000\/0000\-00");
            }
        }

        public static implicit operator Cnpj(string value)
        {
            return new Cnpj(value);
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
