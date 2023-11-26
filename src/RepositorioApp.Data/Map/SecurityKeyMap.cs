using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Jwt.Models;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    internal class SecurityKeyMap : IEntityTypeConfiguration<SecurityKey>
    {
        public void Configure(EntityTypeBuilder<SecurityKey> builder)
        {
            builder.ToTable("security_keys");

            builder.HasKey(x => x.Id);

            builder.MapUuid(x => x.Id, "id");

            builder.MapVarchar(x => x.Parameters, "parameters", true);

            builder.MapTimestamp(x => x.CreatedAt, "created_at");
        }
    }
}
