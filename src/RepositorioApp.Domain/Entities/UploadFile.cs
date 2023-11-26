using System;
using RepositorioApp.Domain.Contracts.Metadata;
using RepositorioApp.Domain.Enums;
namespace RepositorioApp.Domain.Entities
{
    public class UploadFile : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public EUploadStatus UploadStatus { get; private set; } = EUploadStatus.Processing;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        public UploadFile Processing()
        {
            UploadStatus = EUploadStatus.Processing;
            CreatedAt = DateTime.UtcNow;
            return this;
        }

        public UploadFile Completed()
        {
            UploadStatus = EUploadStatus.Completed;
            return this;
        }

        public UploadFile Failed()
        {
            UploadStatus = EUploadStatus.Failed;
            return this;
        }
    }
}
