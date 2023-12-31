using System;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class Article : IEntity
    {
        public Guid Id { get; private set; }
        public Guid? EducationalResourceId { get; private set; }
        public string Title { get; private set; }
        public string Authors { get; private set; }
        public string Link { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
