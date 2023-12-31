using System;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class EducationalRole : IEntity
    {
        public EducationalRole(string role)
        {
            Id = Guid.NewGuid();
            Role = role;
            CreatedAt = DateTime.Now;
        }
        
        public Guid Id { get; private set; }
        public string Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        public void Update(string role)
        {
            Role = role;
        }
    }
}
