using System;
using RepositorioApp.Domain.Contracts.Metadata;
namespace RepositorioApp.Domain.Entities
{
    public class University : IEntity
    {
        public University(string name, string acronym, string uf)
        {
            Id = Guid.NewGuid();
            Name = name;
            Acronym = acronym;
            Uf = uf;
        }
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Acronym { get; private set; }
        public string Uf { get; private set; }
    }
}
