using System;
using System.Collections.Generic;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class ResourceCategory : IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool Active { get; private set; }

        public ICollection<FormType> FormTypes { get; private set; }
    }
}
