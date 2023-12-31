using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("articles");
            
            builder.MapId(x => x.Id);

            builder.MapUuid(x => x.EducationalResourceId, "educational_resource");

            builder.MapVarchar(x => x.Title, "title", true);

            builder.MapVarchar(x => x.Authors, "authors", true);

            builder.MapVarchar(x => x.Link, "link", false);

            builder.MapCreatedAt(x => x.CreatedAt);
        }
    }
}
