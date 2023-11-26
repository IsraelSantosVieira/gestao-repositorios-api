using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class RecoverPasswordCmd
    {
        [Required] [EmailAddress]
        public string Email { get; set; }
    }
}
