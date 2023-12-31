using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class ResetPasswordCmd
    {
        [Required] [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string PasswordConfirm { get; set; }
    }
}
