using System;
namespace RepositorioApp.Jwt
{
    public class JwtOptions
    {
        public int KeyExpirationDays { get; set; }
        public int TokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
        public bool RequireHttps { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuer { get; set; } = true;
        public Uri JwksUri { get; set; }
        public int TokenClockSkewMinutes { get; set; }
    }
}
