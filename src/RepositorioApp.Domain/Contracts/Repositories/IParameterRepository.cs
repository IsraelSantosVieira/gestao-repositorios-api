using System;
using System.Linq.Expressions;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Filters;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.Contracts.Repositories
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        Expression<Func<Parameter, bool>> Where(ParameterFilter filter);
    }
}
