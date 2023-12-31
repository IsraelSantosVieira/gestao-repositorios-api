using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.ViewsModels;
namespace RepositorioApp.Domain.Projections
{
    public static class UniversityProjection
    {
        public static IQueryable<UniversityVm> ToVm(this IQueryable<University> query)
        {
            return query.Select(param => new UniversityVm
            {
                Id = param.Id,
                Name = param.Name,
                Acronym = param.Acronym,
                Uf = param.Uf
            });
        }

        public static IEnumerable<UniversityVm> ToVm(this IEnumerable<University> query)
        {
            return query.AsQueryable().ToVm();
        }

        public static UniversityVm ToVm(this University entity)
        {
            return new[]
            {
                entity
            }.AsQueryable().ToVm().FirstOrDefault();
        }
    }
}
