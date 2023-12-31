using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class EducationalRoleMap : IEntityTypeConfiguration<EducationalRole>
    {
        public void Configure(EntityTypeBuilder<EducationalRole> builder)
        {
            builder.ToTable("educational_role");
            
            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Role, "role", true);

            builder.MapCreatedAt(x => x.CreatedAt);
        }
    }
}
