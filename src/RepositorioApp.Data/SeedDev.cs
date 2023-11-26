using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RepositorioApp.Domain.Entities;
namespace RepositorioApp.Data
{
    public static class SeedDev
    {
        private static bool _isDevelopment;

        public static void SeedEnvProd(DbContext context)
        {
            AddRequiredParameters(context);
        }

        public static void SeedEnvDev(DbContext context, bool isDevelopment)
        {
            _isDevelopment = isDevelopment;
            if (!isDevelopment) return;
            AddUserApp(context);
        }

        private static void AddUserApp(DbContext context)
        {
            const string email = "admin@gmail.com";

            var user = context.Set<User>().FirstOrDefault(x => x.Email == email);

            if (user != null) return;

            user = new User(
                email,
                "User",
                "33999123456",
                "Admin"
            )
            .WithPassword("123456")
            .WithMasterPermission();
            
            context.Add(user);
            context.SaveChanges();
        }

        private static void AddRequiredParameters(DbContext context)
        {
            var parameters = new List<Parameter>();

            parameters.ForEach(param =>
            {
                if (context.Set<Parameter>().FirstOrDefault(x => x.Transaction == param.Transaction) != null)
                {
                    return;
                }

                context.Add(param);
            });

            context.SaveChanges();
        }
    }
}
