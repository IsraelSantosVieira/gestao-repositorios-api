using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
namespace RepositorioApp.Data.Map
{
    internal class UploadFileMap : IEntityTypeConfiguration<UploadFile>
    {
        public void Configure(EntityTypeBuilder<UploadFile> builder)
        {
            builder.ToTable("upload_files");

            builder.MapId(x => x.Id);

            builder.MapCreatedAt(x => x.CreatedAt);
            
            builder.MapEnumAsShort(x => x.UploadStatus, "upload_status", true);
        }
    }
}
