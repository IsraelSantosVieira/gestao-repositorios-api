using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class ResourceCategoryMap : IEntityTypeConfiguration<ResourceCategory>
    {
        public void Configure(EntityTypeBuilder<ResourceCategory> builder)
        {
            builder.ToTable("resource_category");
            
            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Name, "name", true);

            builder.MapBoolean(x => x.Active, "active");
            
            builder.HasMany(x => x.FormTypes)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
