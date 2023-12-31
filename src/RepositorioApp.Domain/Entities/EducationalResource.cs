using System;
using System.Collections.Generic;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class EducationalResource : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Authors { get; private set; }
        public string Description { get; private set; }
        public Guid CategoryId { get; private set; }
        public string RepositoryLink { get; private set; }
        public string Objectives { get; private set; }
        public string Audience { get; private set; }
        
        public ResourceCategory Category { get; private set; }
        
        public ICollection<ResourceMaterial> ResourceMaterials { get; private set; }
        public ICollection<Article> Articles { get; private set; }
    }
}
