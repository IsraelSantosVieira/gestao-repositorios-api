using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Filters;
using RepositorioApp.Persistence.Ef;
using RepositorioApp.Utilities.Linq;
namespace RepositorioApp.Data.Repositories
{
    public sealed class UserRepository : Repository<User, DataContext>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public Expression<Func<User, bool>> Where(UserFilter filter)
        {
            var predicate = PredicateBuilder.True<User>();

            predicate = string.IsNullOrWhiteSpace(filter.Name)
                ? predicate
                : predicate.And(x =>
                    EF.Functions.ILike((x.FirstName + " " + x.LastName).Trim(), $"%{filter.Name}%"));

            predicate = string.IsNullOrWhiteSpace(filter.Email)
                ? predicate
                : predicate.And(x => EF.Functions.ILike(x.Email, filter.Email));

            return predicate;
        }

        public Task<User> FindCompleteByEmailAsync(string email)
        {
            return _context.Set<User>()
                .AllIncludes()
                .FirstOrDefaultAsync(x =>
                    EF.Functions.ILike(x.Email, email));
        }
    }

    internal static class UserExtensions
    {
        public static IQueryable<User> AllIncludes(this IQueryable<User> query)
        {
            return query.Include(x => x.PasswordRecoverRequests);
        }
    }
}
