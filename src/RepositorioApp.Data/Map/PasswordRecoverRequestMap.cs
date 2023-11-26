using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    internal class PasswordRecoverRequestMap : IEntityTypeConfiguration<PasswordRecoverRequest>
    {
        public void Configure(EntityTypeBuilder<PasswordRecoverRequest> builder)
        {
            builder.ToTable("password_recover_requests");

            builder.MapId(x => x.Id);

            builder.MapUuid(x => x.UserId, "user_id");

            builder.MapVarchar(x => x.Code, "code", 100, true);

            builder.MapCreatedAt(x => x.CreatedAt);

            builder.MapTimestamp(x => x.ExpiresIn, "expires_in");

            builder.MapTimestamp(x => x.UsedIn, "used_in");
        }
    }
}
