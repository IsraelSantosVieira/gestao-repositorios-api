using System;
using RepositorioApp.Domain.Enums;
namespace RepositorioApp.Domain.ViewsModels
{
    public class ParameterVm
    {
        public Guid Id { get; set; }
        public string Transaction { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Value { get; set; }
        public EParameterType Type { get; set; }
    }
}
