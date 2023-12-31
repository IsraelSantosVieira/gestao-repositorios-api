using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RepositorioApp.Domain.Entities;
namespace RepositorioApp.Data
{
    public static class SeedDev
    {
        public static void SeedEnvProd(DbContext context)
        {
            AddMasterUser(context);
            AddRequiredParameters(context);
        }

        public static void SeedEnvDev(DbContext context, bool isDevelopment)
        {
            if (!isDevelopment)
            {
                SeedEnvProd(context);
                return;
            }
            
            AddMasterUser(context);
        }

        private static void AddMasterUser(DbContext context)
        {
            const string email = "admin@repositorio.com";

            var user = context.Set<User>().FirstOrDefault(x => x.Email == email);

            if (user != null) return;

            user = new User(
                email,
                "Master",
                "33999670943",
                null,
                null,
                new DateTime(1999, 06, 15),
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
