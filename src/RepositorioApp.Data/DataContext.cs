using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RepositorioApp.Data.Map;
using RepositorioApp.Jwt.Ef;
using RepositorioApp.Jwt.Models;
namespace RepositorioApp.Data
{
    public class DataContext : DbContext, ISecurityKeyContext
    {
        public const string Schema = "public";

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            DbContext = this;
        }

        public DbContext DbContext { get; }

        public DbSet<SecurityKey> SecurityKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.HasDefaultSchema(Schema);

            mb.ApplyConfigurationsFromAssembly(typeof(LogMap).GetTypeInfo().Assembly);
        }
    }
}
