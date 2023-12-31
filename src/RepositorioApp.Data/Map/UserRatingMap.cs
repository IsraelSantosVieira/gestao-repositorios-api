using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepositorioApp.Data.Extensions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Map
{
    public class UserRatingMap : IEntityTypeConfiguration<UserRating>
    {
        public void Configure(EntityTypeBuilder<UserRating> builder)
        {
            builder.ToTable("user_ratings");
            
            builder.MapId(x => x.Id);

            builder.MapUuid(x => x.UserId, "user");

            builder.MapUuid(x => x.UserExperienceId, "user_experience");

            builder.MapInt(x => x.Rating, "rating");

            builder.MapVarchar(x => x.Feedback, "feedback", false);

            builder.MapCreatedAt(x => x.CreatedAt);
        }
    }
}
