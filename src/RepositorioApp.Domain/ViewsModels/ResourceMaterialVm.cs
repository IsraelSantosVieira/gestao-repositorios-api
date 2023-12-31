using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class ResourceMaterialVm
    {
        public Guid Id { get; private set; }
        public string FileName { get; set; }
        public string BlobUrl { get; set; }
    }
}
