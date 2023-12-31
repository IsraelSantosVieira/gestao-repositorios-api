using System;
using System.Collections.Generic;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class UserExperience : IEntity
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }
        public string Title { get; private set; }
        public string Profile { get; private set; }
        public int Participants { get; private set; }
        public Guid FormTypeId { get; private set; }
        
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        
        public User Owner { get; private set; }
        public FormType FormType { get; private set; }
        public ICollection<UserRating> Ratings { get; private set; }
    }
}
