namespace RepositorioApp.CrossCutting.Config
{
    public class JwTokenConfig
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int ExpiresIn { get; set; }

        public int RefreshTokenDaysExpiration { get; set; }
    }
}
