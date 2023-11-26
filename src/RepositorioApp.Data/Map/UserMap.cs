using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.MapId(x => x.Id);

            builder.MapCreatedAt(x => x.CreatedAt);

            builder.MapVarchar(x => x.Email, "email", 1025, true);

            builder.MapVarchar(x => x.Password, "password", 1025, true);

            builder.MapVarchar(x => x.FirstName, "first_name", 255, false);

            builder.MapVarchar(x => x.LastName, "last_name", 255, false);

            builder.MapBoolean(x => x.Active, "active");

            builder.MapVarchar(x => x.Avatar, "avatar", false);

            builder.MapVarchar(x => x.Phone, "phone", true);
            
            builder.MapBoolean(x => x.Master, "master");
            
            builder.MapBoolean(x => x.PendingRegisterInformation, "pending_information");

            builder.MapBoolean(x => x.AcceptedTerm, "accepted_term")
                .HasDefaultValue(false);

            builder.HasMany(x => x.PasswordRecoverRequests)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new
                {
                    x.Email,
                    x.Phone
                })
                .HasDatabaseName("ix_users_email")
                .IsUnique();
        }
    }
}
