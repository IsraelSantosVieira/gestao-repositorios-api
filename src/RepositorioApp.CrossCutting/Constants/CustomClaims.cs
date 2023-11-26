using System.Security.Claims;
namespace RepositorioApp.CrossCutting.Constants
{
    public class CustomClaims
    {
        public const string Id = "id";
        public const string Sub = "sub";
        public const string Master = "master";
        public const string FullName = "full_name";
        public const string Email = ClaimTypes.Email;
        public const string Phone = "phone";
        public const string Avatar = "avatar";
    }
}
