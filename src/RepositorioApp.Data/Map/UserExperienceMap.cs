using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class UserExperienceMap : IEntityTypeConfiguration<UserExperience>
    {
        public void Configure(EntityTypeBuilder<UserExperience> builder)
        {
            builder.ToTable("user_experiences");
            
            builder.MapId(x => x.Id);

            builder.MapUuid(x => x.OwnerId, "owner");

            builder.MapVarchar(x => x.Title, "title", true);

            builder.MapVarchar(x => x.Profile, "profile", false);

            builder.MapInt(x => x.Participants, "participants");

            builder.MapUuid(x => x.FormTypeId, "form_type");

            builder.MapCreatedAt(x => x.CreatedAt);

            builder.MapTimestamp(x => x.ExpiresAt, "expires_at");
            
            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.FormType)
                .WithMany()
                .HasForeignKey(x => x.FormTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.Ratings)
                .WithOne()
                .HasForeignKey(x => x.UserExperienceId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
