using System;
using System.Collections.Generic;
namespace RepositorioApp.Domain.ViewsModels
{
    public class UserExperienceVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Profile { get; set; }
        public int Participants { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        
        public UserVm Owner { get; set; }
        public FormTypeVm FormType { get; set; }
        public ICollection<UserRatingVm> Ratings { get; set; }
    }
}
