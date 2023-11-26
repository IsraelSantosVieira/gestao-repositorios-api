using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class UserRegistrationCmd
    {
        [Required] [EmailAddress] [MaxLength(1025)]
        public string Email { get; set; }

        [Required] [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength]
        public string LastName { get; set; }
        
        [Required]
        public string Phone { get; set; }
    }
}
