using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class UserRatingVm
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public UserVm User { get; private set; }
        public UserExperienceVm UserExperience { get; private set; }
    }
}
