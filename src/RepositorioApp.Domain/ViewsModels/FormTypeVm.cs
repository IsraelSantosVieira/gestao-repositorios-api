using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class FormTypeVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
