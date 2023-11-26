using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class ParameterMap : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.ToTable("parameter");

            builder.MapId(x => x.Id);
            builder.Property(p => p.Transaction)
                .HasColumnName("transaction")
                .HasColumnType("text")
                .IsRequired();
            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasColumnType("text")
                .IsRequired();
            builder.Property(p => p.Value)
                .HasColumnName("value")
                .HasColumnType("text")
                .IsRequired();
            builder.MapEnumAsShort(p => p.Type, "parameter_type", true);
            builder.MapBoolean(p => p.Active, "active");
        }
    }
}
