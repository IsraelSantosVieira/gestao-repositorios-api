using System;
using System.Collections.Generic;
namespace RepositorioApp.Domain.ViewsModels
{
    public class EducationalResourceVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string RepositoryLink { get; set; }
        public string Objectives { get; set; }
        public string Audience { get; set; }
        
        public ResourceCategoryVm CategoryVm { get; set; }
        
        public ICollection<ResourceMaterialVm> ResourceMaterials { get; set; }
        public ICollection<ArticleVm> Articles { get; set; }
    }
}
