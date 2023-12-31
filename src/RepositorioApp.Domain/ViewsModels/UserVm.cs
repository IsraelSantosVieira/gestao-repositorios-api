using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public string Avatar { get; set; }
        public bool PendingRegisterInformation { get; set; }
        public bool AcceptedTerm { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Master { get; set; }
        
        public EducationalRoleVm EducationalRole { get; set; }
        public UniversityVm University { get; set; }
    }
}
