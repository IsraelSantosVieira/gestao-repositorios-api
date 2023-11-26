using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Filters;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Expression<Func<User, bool>> Where(UserFilter filter);

        Task<User> FindCompleteByEmailAsync(string email);
    }
}
