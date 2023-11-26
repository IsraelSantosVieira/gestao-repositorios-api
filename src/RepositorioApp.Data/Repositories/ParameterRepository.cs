using System;
using System.Linq.Expressions;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Filters;
using RepositorioApp.Persistence.Ef;
using RepositorioApp.Utilities.Linq;
namespace RepositorioApp.Data.Repositories
{
    public class ParameterRepository : Repository<Parameter, DataContext>, IParameterRepository
    {
        public ParameterRepository(DataContext context) : base(context)
        {
        }

        public Expression<Func<Parameter, bool>> Where(ParameterFilter filter)
        {
            var predicate = PredicateBuilder.True<Parameter>();

            predicate = filter.Active.HasValue
                ? predicate.And(x => x.Active == filter.Active.Value)
                : predicate;

            predicate = filter.Transaction != null
                ? predicate.And(x => x.Transaction == filter.Transaction)
                : predicate;

            predicate = filter.Type.HasValue
                ? predicate.And(x => x.Type == filter.Type.Value)
                : predicate;

            return predicate;
        }
    }
}
