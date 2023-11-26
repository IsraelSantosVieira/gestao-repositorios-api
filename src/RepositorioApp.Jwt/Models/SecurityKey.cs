using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
namespace RepositorioApp.Jwt.Models
{
    public class SecurityKey
    {
        protected SecurityKey()
        {

        }

        public Guid Id { get; set; }

        public string Parameters { get; set; }

        public DateTime CreatedAt { get; set; }

        public static SecurityKey Create()
        {
            var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var ecParams = key.ExportParameters(true);
            var id = Guid.NewGuid();
            return new SecurityKey
            {
                Id = id,
                CreatedAt = DateTime.UtcNow,
                Parameters = ecParams.ToJson(id: id)
            };
        }

        public ECDsa GetECDsa()
        {
            return Parameters.FromJson();
        }

        public SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials(new ECDsaSecurityKey(GetECDsa()), ECDsaConstants.Alg);
        }
    }
}
