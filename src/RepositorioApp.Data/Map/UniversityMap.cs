using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class UniversityMap : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.ToTable("universities");
            
            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Name, "name", true);

            builder.MapVarchar(x => x.Acronym, "acronym", true);

            builder.MapVarchar(x => x.Uf, "uf", true);
        }
    }
}
