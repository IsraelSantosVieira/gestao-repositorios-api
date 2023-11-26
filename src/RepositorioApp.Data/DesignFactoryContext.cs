using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RepositorioApp.Data.Extensions;
namespace RepositorioApp.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder().AddUserSecrets<DataContext>().Build();
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = config.GetConnectionString("Default");
            builder.UseCustomNpgsql(connectionString);
            Console.WriteLine(connectionString);
            return new DataContext(builder.Options);
        }
    }
}
