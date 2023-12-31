using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.ViewsModels;
namespace RepositorioApp.Domain.Projections
{
    public static class EducationalRoleProjection
    {
        public static IQueryable<EducationalRoleVm> ToVm(this IQueryable<EducationalRole> query)
        {
            return query.Select(param => new EducationalRoleVm
            {
                Id = param.Id,
                Role = param.Role,
                CreatedAt = param.CreatedAt
            });
        }

        public static IEnumerable<EducationalRoleVm> ToVm(this IEnumerable<EducationalRole> query)
        {
            return query.AsQueryable().ToVm();
        }

        public static EducationalRoleVm ToVm(this EducationalRole entity)
        {
            return new[]
            {
                entity
            }.AsQueryable().ToVm().FirstOrDefault();
        }
    }
}
