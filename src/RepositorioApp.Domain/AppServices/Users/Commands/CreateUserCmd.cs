using System;
using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class CreateUserCmd
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public bool Agreements { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        [Required]
        public Guid University { get; set; }
        
        [Required]
        public Guid EducationalRole { get; set; }
    }
}
