using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Extensions;
namespace RepositorioApp.CrossCutting.Utils
{
    public static class Extensions
    {
        public static string FormatSlug([NotNull] this string value)
        {
            return string.Join("_", value.RemoveAccents().ToLower().Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public static string GetEmail(this IEnumerable<Claim> claims)
        {
            return claims.FirstOrDefault(x => x.Type is CustomClaims.Email or ClaimTypes.Email)?.Value;
        }

        public static string FormatPhone(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            var number = Regex.Replace(value, @"[^0-9]", "");

            value = number.Length == 10
                ? Convert.ToUInt64(number).ToString(@"00\-00000000")
                : number.Length == 11
                    ? Convert.ToUInt64(number).ToString(@"00\-000000000")
                    : value;

            return value;
        }

        public static string HasDigits(this Guid value)
        {
            return value.ToString("N");
        }

        public static string HandleContentType(this string fileName)
        {
            var extension = Path.GetExtension(fileName);

            if (extension.Equals(".js", StringComparison.OrdinalIgnoreCase))
            {
                return "application/javascript";
            }

            if (extension.Equals(".png", StringComparison.OrdinalIgnoreCase))
            {
                return "image/png";
            }

            if (extension.Equals(".svg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/svg+xml";
            }

            if (extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return "image/jpeg";
            }

            if (extension.Equals(".gif", StringComparison.OrdinalIgnoreCase))
            {
                return "image/gif";
            }

            if (extension.Equals(".ico", StringComparison.OrdinalIgnoreCase))
            {
                return "image/x-icon";
            }

            if (extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return "application/pdf";
            }

            if (extension.Equals(".doc", StringComparison.OrdinalIgnoreCase))
            {
                return "application/msword";
            }

            if (extension.Equals(".docx", StringComparison.OrdinalIgnoreCase))
            {
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            }

            return "application/octet-stream";
        }
    }
}
