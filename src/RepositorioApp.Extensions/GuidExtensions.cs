using System;
namespace RepositorioApp.Extensions
{
    public static class GuidExtensions
    {
        public static string ToDigits(this Guid value)
        {
            return value.ToString("N");
        }
    }
}
