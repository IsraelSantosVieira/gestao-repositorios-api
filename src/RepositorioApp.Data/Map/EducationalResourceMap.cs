using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class EducationalResourceMap : IEntityTypeConfiguration<EducationalResource>
    {
        public void Configure(EntityTypeBuilder<EducationalResource> builder)
        {
            builder.ToTable("educational_resources");

            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Name, "name", true);

            builder.MapVarchar(x => x.Authors, "authors", true);

            builder.MapVarchar(x => x.Description, "description", false);

            builder.MapUuid(x => x.CategoryId, "category");

            builder.MapVarchar(x => x.RepositoryLink, "repository_link", true);

            builder.MapVarchar(x => x.Objectives, "objectives", true);

            builder.MapVarchar(x => x.Audience, "audience", true);
            
            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.ResourceMaterials)
                .WithOne()
                .HasForeignKey(x => x.EducationalResourceId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(x => x.Articles)
                .WithOne()
                .HasForeignKey(x => x.EducationalResourceId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
