using System.Collections.Generic;
namespace RepositorioApp.CrossCutting.Constants
{
    public static class GrantTypes
    {
        public const string Password = "password";
        public const string RefreshToken = "refresh_token";

        public static readonly IReadOnlyCollection<string> All = new[]
        {
            Password, RefreshToken
        };
    }
}
