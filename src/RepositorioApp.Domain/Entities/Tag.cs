using System;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class Tag : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Group { get; private set; }
        public string Code { get; private set; }
    }
}
