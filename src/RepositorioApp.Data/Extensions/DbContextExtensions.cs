using Microsoft.EntityFrameworkCore;
namespace RepositorioApp.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static DbContextOptionsBuilder UseCustomNpgsql(
            this DbContextOptionsBuilder optionsBuilder,
            string connectionString
        )
        {
            optionsBuilder.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsHistoryTable("_ef_migrations_history", DataContext.Schema);
            });

            return optionsBuilder;
        }
    }
}
