using System;
using RepositorioApp.Domain.Contracts.Metadata;
using RepositorioApp.Domain.Enums;
namespace RepositorioApp.Domain.Entities
{
    public class Parameter : IEntity
    {
        public Parameter(string transaction, string group, string description, string value, EParameterType type)
        {
            Id = Guid.NewGuid();
            Transaction = transaction;
            Group = group;
            Description = description;
            Value = value;
            Type = type;
            Active = true;
        }
        
        public Guid Id { get; private set; }
        public string Transaction { get; private set; }
        public string Group { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public string Value { get; private set; }
        public EParameterType Type { get; private set; }

        public Parameter Update(string transaction, string group, string description, string value, EParameterType type, bool? active = true)
        {
            Group = group;
            Transaction = transaction;
            Description = description;
            Value = value;
            Type = type;
            Active = active ?? true;

            return this;
        }

        public Parameter UpdateTransaction(string transaction)
        {
            Transaction = transaction;
            return this;
        }

        public Parameter UpdateStatus()
        {
            Active = !Active;
            return this;
        }

        public Parameter UpdateValue(string value)
        {
            Value = value;
            return this;
        }
    }
}
