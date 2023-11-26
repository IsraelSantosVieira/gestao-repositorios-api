using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using RepositorioApp.Jwt.Contracts;
using RepositorioApp.Jwt.Models;
namespace RepositorioApp.Jwt.Ef
{
    public class SecurityKeyStoreService<TContext> : ISecurityKeyStoreService where TContext : DbContext
    {
        private const string CacheKey = "security-keys";
        private readonly DbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly JwtOptions _options;

        public SecurityKeyStoreService(TContext context, IOptions<JwtOptions> options, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _context = context;
            _options = options.Value;
        }

        private DateTime MaxExpirationLimitForSecurityKeys =>
            DateTime.UtcNow.AddDays(-_options.KeyExpirationDays).AddMinutes(-_options.RefreshTokenExpirationMinutes);

        public virtual SecurityKey AddSecurityKey()
        {
            var securityKey = SecurityKey.Create();
            _context.Set<SecurityKey>().Add(securityKey);
            _context.SaveChanges();
            return securityKey;
        }

        public virtual SecurityKey GetCurrent()
        {
            var securityKey = GetLastFromCache();

            if (securityKey == null)
            {
                AddSecurityKey();
                _memoryCache.Remove(CacheKey);
                return GetLastFromCache();
            }

            if (securityKey.CreatedAt.AddDays(_options.KeyExpirationDays) <= DateTime.UtcNow)
            {
                AddSecurityKey();
                _memoryCache.Remove(CacheKey);
                return GetLastFromCache();
            }

            return securityKey;
        }

        public virtual ICollection<SecurityKey> GetCurrents()
        {
            var list = _memoryCache.GetOrCreate(CacheKey,
                entry =>
                {
                    var entries = _context.Set<SecurityKey>()
                        .Where(x => x.CreatedAt >= MaxExpirationLimitForSecurityKeys)
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(2)
                        .ToList();

                    if (!entries.Any())
                    {
                        entries.Add(AddSecurityKey());
                    }
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    entry.SetPriority(CacheItemPriority.High);
                    return entries;
                });


            return !list.Any()
                ? new[]
                {
                    AddSecurityKey()
                }
                : list;
        }

        private SecurityKey GetLastFromCache()
        {
            return GetCurrents()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();
        }
    }
}
