using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RepositorioApp.Utilities.Paging;
using RepositorioApp.Utilities.Spec;
namespace RepositorioApp.Utilities.Persistence
{
    public interface IQueryRepository<T>
        where T : class
    {
        int Count(Expression<Func<T, bool>> where = null);
        Task<int> CountAsync(Expression<Func<T, bool>> where = null);
        bool Any(Expression<Func<T, bool>> where);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        T FindByKey(params object[] keyValues);
        ValueTask<T> FindByKeyAsync(params object[] keyValues);
        T Find(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);
        T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        Task<T> FindAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        IQueryable<T> List(IEnumerable<string> includes = null);

        IQueryable<T> List(params Expression<Func<T, object>>[] includes);
        IQueryable<T> List(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null);

        IQueryable<T> List(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> List(Expression<Func<T, bool>> where, IPagination pagination, IEnumerable<string> includes = null);

        IQueryable<T> List(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes);

        PagedList<T> PagedList(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes);
        IQueryable<T> List(ISpec<T> spec);

        IQueryable<T> List(ISpec<T> spec, IPagination pagination);

        Task<T> FindAsync(ISpec<T> spec);
    }
}
