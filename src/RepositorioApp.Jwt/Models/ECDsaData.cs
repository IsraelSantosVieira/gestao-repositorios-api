using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
namespace RepositorioApp.Jwt.Models
{
    public static class ECDsaConstants
    {
        public const string Kty = "EC";
        public const string Use = "sig";
        public const string Alg = "ES256";
    }

    public class BaseECDsaData
    {
        public string Kty { get; set; } = ECDsaConstants.Kty;
        public string Use { get; set; } = ECDsaConstants.Use;
        public string Alg { get; set; } = ECDsaConstants.Alg;
        public string Kid { get; set; }
        public string Crv { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
    }

    public class ECDsaData : BaseECDsaData
    {
        public string D { get; set; }
    }

    public static class ECDsaDataExtensions
    {
        public static ECDsa FromJson(this string json)
        {
            var ecDsaData = CustomJsonSerializer.Deserialize<ECDsaData>(json);

            if (ecDsaData == null) return null;

            var curve = ecDsaData.Crv switch
            {
                ECDsaCurves.P256 => ECCurve.NamedCurves.nistP256,
                ECDsaCurves.P384 => ECCurve.NamedCurves.nistP384,
                ECDsaCurves.P521 => ECCurve.NamedCurves.nistP521,
                _ => throw new NotSupportedException()
            };

            var key = ECDsa.Create(new ECParameters
            {
                Curve = curve,
                D = Base64UrlEncoder.DecodeBytes(ecDsaData.D),// optional
                Q = new ECPoint
                {
                    X = Base64UrlEncoder.DecodeBytes(ecDsaData.X),
                    Y = Base64UrlEncoder.DecodeBytes(ecDsaData.Y)
                }
            });

            return key;
        }

        public static string ToJson(this ECParameters parameters, string curve = ECDsaCurves.P256, Guid? id = null)
        {
            var ecDsaData = new ECDsaData
            {
                Kid = (id ?? Guid.NewGuid()).ToString("N"),
                Crv = ECDsaCurves.GetCurve(curve),
                D = Base64UrlEncoder.Encode(parameters.D),
                X = Base64UrlEncoder.Encode(parameters.Q.X),
                Y = Base64UrlEncoder.Encode(parameters.Q.Y)
            };

            return CustomJsonSerializer.Serialize(ecDsaData);
        }
    }
}
