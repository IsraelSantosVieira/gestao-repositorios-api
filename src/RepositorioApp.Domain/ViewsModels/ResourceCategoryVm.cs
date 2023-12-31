using System;
using System.Collections.Generic;
namespace RepositorioApp.Domain.ViewsModels
{
    public class ResourceCategoryVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public ICollection<FormTypeVm> FormTypes { get; set; }
    }
}
