using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class ResourceMaterialMap : IEntityTypeConfiguration<ResourceMaterial>
    {
        public void Configure(EntityTypeBuilder<ResourceMaterial> builder)
        {
            builder.ToTable("resource_materials");
            
            builder.MapId(x => x.Id);

            builder.MapUuid(x => x.EducationalResourceId, "educational_resource");

            builder.MapVarchar(x => x.FileName, "file_name", true);

            builder.MapVarchar(x => x.BlobUrl, "blob_url", true);
        }
    }
}
