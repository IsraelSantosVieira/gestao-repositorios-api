using System;
using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class UpdateProfileCmd
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public Guid University { get; set; }
        
        public Guid EducationalRole { get; set; }

        public string Phone { get; set; }
        
        public string Avatar { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public bool AcceptedTerm { get; set; }
    }
}
