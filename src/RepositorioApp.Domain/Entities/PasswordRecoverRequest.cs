using System;
using RepositorioApp.Extensions;
namespace RepositorioApp.Domain.Entities
{
    public class PasswordRecoverRequest
    {
        private PasswordRecoverRequest()
        {
        }

        public PasswordRecoverRequest(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Code = "".GenerateRandonNumberCode(4);
            CreatedAt = DateTime.Now;
            ExpiresIn = CreatedAt.AddHours(48);
        }

        public PasswordRecoverRequest(
            Guid userId,
            DateTime createdAt,
            DateTime expiresIn,
            DateTime? usedIn = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Code = "".GenerateRandonNumberCode(4);
            CreatedAt = createdAt;
            ExpiresIn = expiresIn;
            UsedIn = usedIn;
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string Code { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime ExpiresIn { get; private set; }

        public DateTime? UsedIn { get; private set; }

        public static PasswordRecoverRequest New(Guid id, Guid userId)
        {
            var now = DateTime.UtcNow;

            return new PasswordRecoverRequest
            {
                Id = id,
                UserId = userId,
                Code = "".GenerateRandonNumberCode(4),
                CreatedAt = now,
                ExpiresIn = now.AddHours(48)
            };
        }

        public PasswordRecoverRequest UsePasswordRecoverRequest()
        {
            UsedIn = DateTime.UtcNow;
            return this;
        }

        public void UpdateDates()
        {
            CreatedAt = DateTime.Now;
            ExpiresIn = CreatedAt.AddHours(48);
        }
    }
}
