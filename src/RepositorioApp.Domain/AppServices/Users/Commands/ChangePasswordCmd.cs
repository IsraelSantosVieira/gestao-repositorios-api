using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class ChangePasswordCmd
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string NewPasswordConfirmation { get; set; }
    }
}
