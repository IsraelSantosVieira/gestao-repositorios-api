using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("tags");
            
            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Name, "name", true);

            builder.MapVarchar(x => x.Group, "group", true);

            builder.MapVarchar(x => x.Code, "code", true);
        }
    }
}
