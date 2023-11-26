using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class ActivationCodeCmd
    {
        [Required] [EmailAddress]
        public string Email { get; set; }
    }
}
