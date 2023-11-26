using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class UploadFileVm
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UploadStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
