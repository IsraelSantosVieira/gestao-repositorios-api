using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MimeTypes;
namespace RepositorioApp.Extensions
{
    public static class StringExtensions
    {
        public static StringContent ToJsonContent(this string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
        public static string ToNumber(this string value)
        {
            return string.IsNullOrWhiteSpace(value?.Trim())
                ? ""
                : Regex.Replace(value, @"[^0-9]", "");
        }

        public static string ToBase64Hash(this string value)
        {
            using (var hashData = SHA256.Create())
            {
                var data = hashData.ComputeHash(Encoding.ASCII.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        public static string ToSha256(this string value)
        {
            using (var hashData = SHA256.Create())
            {
                var hash = new StringBuilder();

                foreach (var theByte in hashData.ComputeHash(Encoding.ASCII.GetBytes(value)))
                {
                    hash.Append(theByte.ToString("x2"));
                }

                return hash.ToString();
            }
        }

        public static string FormatCpfCnpj(this string value)
        {
            var result = "";

            if (string.IsNullOrWhiteSpace(value)) return result;

            var number = value.ToNumber();

            result = number.Length == 11
                ? Convert.ToUInt64(number).ToString(@"000\.000\.000\-00")
                : number.Length == 14
                    ? Convert.ToUInt64(number).ToString(@"00\.000\.000\/0000\-00")
                    : result;

            return result;
        }

        public static string FromBase64(this string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
        public static string ToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }
        public static string ReplaceSpecial(this string value)
        {
            return Regex.Replace(value, @"[\\/]", "_");
        }

        public static string ToBase64Key(this string value)
        {
            return value.ToBase64().ReplaceSpecial();
        }

        public static string RemoveSpaces(this string value)
        {
            value = value.Replace(" ", "").Trim();
            return value;
        }

        public static T ToEnumValue<T>(this string value)
            where T : struct
        {
            if (Enum.TryParse(typeof(T), value, true, out var result) && result != null)
            {
                return (T)result;
            }

            return default;
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrWhiteSpace(value?.Trim());
        }

        public static string GenerateCode(this string value, int length)
        {
            var random = new Random();
            var characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = new StringBuilder(length);
            for (var i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            return result.ToString();
        }

        public static string GenerateRandonNumberCode(this string value, int length)
        {
            var random = new Random();
            var str = "0123456789";
            var stringBuilder = new StringBuilder(length);
            for (var index = 0; index < length; ++index)
                stringBuilder.Append(str[random.Next(str.Length)]);
            return stringBuilder.ToString();
        }

        public static string OrderByPgsql(this string value,
            string sortField,
            string sortDir = "asc",
            string alias = null)
        {
            var fields = alias.IsNull()
                ? string.Join(",", sortField.Split(",").Select(x => $@"""{x.Trim()}"" {sortDir}"))
                : string.Join(",", sortField.Split(",").Select(x => $@"""{alias}"".""{x.Trim()}"" {sortDir}"));

            return $"order by {fields}";
        }

        public static string LimitRowsPgsql(this string value, int pageIndex, int pageSize)
        {
            return $"LIMIT {pageSize} OFFSET {(pageIndex - 1) * pageSize}";
        }

        public static bool IsUrl(this string value)
        {
            const string pattern = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
            if (value.Contains("http://localhost") || value.Contains("https://localhost")) return true;
            return Regex.IsMatch(value, pattern);
        }

        public static bool IsEmail(this string email)
        {
            return Regex.IsMatch(
                email ?? "",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase);
        }


        // public static string RemoveAccents(this string value)
        // {
        //     Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        //     return Encoding.ASCII.GetString(Encoding.GetEncoding("Cyrillic").GetBytes(value));
        // }

        public static string AddPath(this string url, string path)
        {
            return string.IsNullOrWhiteSpace(url) ? path : url.EndsWith("/") ? $"{url}{path}" : $"{url}/{path}";
        }

        public static string AddPath(this string url, object path)
        {
            return url.AddPath(path.ToString());
        }

        public static string AddQueryStringParameter(this string url, string parameter, string value)
        {
            if (string.IsNullOrWhiteSpace(url)) return string.Empty;

            var builder = new UriBuilder(url);

            if (builder.Query.Length > 1)
            {
                builder.Query = $"{builder.Query.Substring(1)}&{parameter}={value}";
                return builder.Uri.ToString();
            }

            builder.Query = $"{parameter}={value}";

            return builder.Uri.ToString();
        }

        public static string AddQueryStringParameter(this string url, string parameter, object value)
        {
            return url.AddQueryStringParameter(parameter, value.ToString());
        }

        public static string UrlEncode(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : WebUtility.UrlEncode(value);
        }

        public static string UrlDecode(this string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : WebUtility.UrlDecode(value);
        }

        public static string FormattedElapsed(this TimeSpan timeSpan)
        {
            return timeSpan.ToString("hh':'mm':'ss'.'fff");
        }

        public static string GetExtension(this string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public static string GetExtensionFromMimeType(this string value)
        {
            return MimeTypeMap.GetExtension(value);
        }

        public static string GetMimeTypeFromFileName(this string fileName)
        {
            return MimeTypeMap.GetMimeType(Path.GetExtension(fileName));
        }

        public static bool TryGetMimeTypeFromFileName(this string fileName, out string mimeType)
        {
            return MimeTypeMap.TryGetMimeType(Path.GetExtension(fileName), out mimeType);
        }

        public static bool TryGetExtensionFromMimeType(this string value, out string extension)
        {
            extension = null;
            try
            {
                extension = MimeTypeMap.GetExtension(value);
                return !string.IsNullOrWhiteSpace(extension);
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateRandomPassword(this string value, int length = 6)
        {
            char GetPasswordElement(Random rd, string charsValue, bool toUpper = false)
            {
                var result = charsValue[rd.Next(charsValue.Length - 1)];
                return toUpper ? char.ToUpper(result) : result;
            }

            char[] ShuffleArray(Random rd, char[] array)
            {
                for (var i = array.Length; i > 0; i--)
                {
                    var j = rd.Next(i);
                    var k = array[j];
                    array[j] = array[i - 1];
                    array[i - 1] = k;
                }

                return array;
            }

            if (length < 6)
            {
                throw new ArgumentException("Password lenngth must be ate least 6 chars");
            }
            const int maxIdenticalChars = 2;
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string specials = @"!#$%&*@\";

            var random = new Random();
            var characterSet = $"{chars}{chars.ToUpper()}{numbers}{specials}";

            var password = new char[length];
            var temp = ShuffleArray(random, new[]
            {
                GetPasswordElement(random, chars, true), GetPasswordElement(random, chars), GetPasswordElement(random, numbers), GetPasswordElement(random, specials)
            });

            for (var i = 0; i < temp.Length; i++)
            {
                password[i] = temp[i];
            }

            for (var i = 4; i < password.Length; i++)
            {
                password[i] = characterSet[random.Next(characterSet.Length - 1)];

                var moreThanTwoIdenticalInARow =
                    i > maxIdenticalChars
                    && password[i] == password[i - 1]
                    && password[i - 1] == password[i - 2];

                if (moreThanTwoIdenticalInARow)
                {
                    i--;
                }
            }

            return string.Join(null, password);
        }

        public static int CalculateSimilarityScore(this string currentString, string otherString)
        {
            var distanceMatrix = new int[currentString.Length + 1, otherString.Length + 1];

            for (var i = 0; i <= currentString.Length; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (var j = 0; j <= otherString.Length; j++)
            {
                distanceMatrix[0, j] = j;
            }

            for (var i = 1; i <= currentString.Length; i++)
            {
                for (var j = 1; j <= otherString.Length; j++)
                {
                    var cost = currentString[i - 1] == otherString[j - 1] ? 0 : 1;

                    distanceMatrix[i, j] = Math.Min(Math.Min(
                            distanceMatrix[i - 1, j] + 1,
                            distanceMatrix[i, j - 1] + 1),
                        distanceMatrix[i - 1, j - 1] + cost);
                }
            }

            var distance = distanceMatrix[currentString.Length, otherString.Length];
            return 100 - (int)((double)distance / Math.Max(currentString.Length, otherString.Length) * 100);
        }

        public static string RemoveAccents(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return "";

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var result = new StringBuilder();

            foreach (var c in normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                switch (c)
                {
                    case 'á':
                    case 'à':
                    case 'â':
                    case 'ä':
                    case 'ã':
                        result.Append('a');
                        break;

                    case 'é':
                    case 'è':
                    case 'ê':
                    case 'ë':
                        result.Append('e');
                        break;

                    case 'í':
                    case 'ì':
                    case 'î':
                    case 'ï':
                        result.Append('i');
                        break;

                    case 'ó':
                    case 'ò':
                    case 'ô':
                    case 'ö':
                    case 'õ':
                        result.Append('o');
                        break;

                    case 'ú':
                    case 'ù':
                    case 'û':
                    case 'ü':
                        result.Append('u');
                        break;

                    case 'ç':
                        result.Append('c');
                        break;

                    default:
                        result.Append(c);
                        break;
                }
            }

            return result.ToString();
        }

        public static string CleanString(this string input)
        {
            var cleanedString = input.RemoveAccents();
            cleanedString = Regex.Replace(cleanedString, @"[^0-9a-zA-Z]+", "");
            cleanedString = cleanedString.ToLower();
            cleanedString = cleanedString.RemovePluralLetters();
            return cleanedString;
        }

        public static string RemovePluralLetters(this string input)
        {
            string[] pluralLetters =
            {
                "s", "es"
            };

            foreach (var letter in pluralLetters)
            {
                if (input.EndsWith(letter))
                {
                    input = input.Remove(input.Length - letter.Length);
                    break;
                }
            }

            return input;
        }

        public static string ExtractNumbersWithX(this string inputString)
        {
            var regex = new Regex(@"\b(\d+x\d+)\b", RegexOptions.IgnoreCase);
            var match = regex.Match(inputString);
            return match.Success ? match.Groups[0].Value : inputString;
        }
    }
}
