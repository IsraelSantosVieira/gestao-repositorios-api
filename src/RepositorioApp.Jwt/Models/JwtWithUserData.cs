using System;
namespace RepositorioApp.Jwt.Models
{
    public class JwtWithUserData<T>
    {
        public T UserData { get; set; }

        public DateTime NotBefore { get; set; }

        public DateTime ExpiresIn { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiresIn { get; set; }
    }
}
