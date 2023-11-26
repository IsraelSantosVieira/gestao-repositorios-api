namespace RepositorioApp.Jwt
{
    public static class ECDsaCurves
    {
        public const string P256 = "P-256";
        public const string P384 = "P-384";
        public const string P521 = "P-521";

        public static string GetCurve(string curve)
        {
            return curve switch
            {
                P256 => P256,
                P384 => P384,
                P521 => P521,
                _ => P256
            };
        }
    }
}
