using System;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class UserRating : IEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid UserExperienceId { get; private set; }
        public int Rating { get; private set; }
        public string Feedback { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
