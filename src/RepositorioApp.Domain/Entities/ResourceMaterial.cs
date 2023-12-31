using System;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class ResourceMaterial : IEntity
    {
        public Guid Id { get; private set; }
        public Guid EducationalResourceId { get; private set; }
        public string FileName { get; private set; }
        public string BlobUrl { get; private set; }
    }
}
