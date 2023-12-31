using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.ViewsModels;
namespace RepositorioApp.Domain.Projections
{
    public static class ParameterProjection
    {
        public static IQueryable<ParameterVm> ToVm(this IQueryable<Parameter> query)
        {
            return query.Select(param => new ParameterVm
            {
                Id = param.Id,
                Transaction = param.Transaction,
                Description = param.Description,
                Active = param.Active,
                Value = param.Value,
                Type = param.Type
            });
        }

        public static IEnumerable<ParameterVm> ToVm(this IEnumerable<Parameter> query)
        {
            return query.AsQueryable().ToVm();
        }

        public static ParameterVm ToVm(this Parameter entity)
        {
            return new[]
            {
                entity
            }.AsQueryable().ToVm().FirstOrDefault();
        }
    }
}
