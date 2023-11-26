using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    internal class LogMap : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("logs");

            builder.MapId(x => x.Id);

            builder.MapVarchar(x => x.Exception, "exception", false);

            builder.MapVarchar(x => x.Level, "level", false);

            builder.MapVarchar(x => x.Logger, "logger", false);

            builder.MapVarchar(x => x.Message, "message", false);

            builder.MapTimestamp(x => x.OccurredAt, "occurred_at");
        }
    }
}
