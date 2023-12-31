using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class FormTypeMap : IEntityTypeConfiguration<FormType>
    {
        public void Configure(EntityTypeBuilder<FormType> builder)
        {
            builder.ToTable("form_types");
            
            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Name, "name", true);

            builder.MapVarchar(x => x.Code, "code", true);

            builder.MapCreatedAt(x => x.CreatedAt);
        }
    }
}
