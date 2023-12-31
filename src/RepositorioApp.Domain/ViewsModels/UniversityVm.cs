using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class UniversityVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public string Uf { get; set; }
    }
}
