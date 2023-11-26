using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class AuthenticateUserCmd
    {
        /// <summary>
        ///     password or refresh_token
        /// </summary>
        [Required]
        public string GrantType { get; set; }

        [Required] [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     Required when grant type is password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Required when grant type is refresh_token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
