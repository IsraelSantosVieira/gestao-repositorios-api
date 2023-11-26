using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RepositorioApp.Utilities.Paging;
using RepositorioApp.Utilities.Spec;
namespace RepositorioApp.Utilities.Persistence
{
    public interface IQueryAsNoTrackingRepository<T> where T : class
    {
        T FindAsNoTracking(ISpec<T> spec);

        T FindAsNoTracking(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);

        Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null);
        T FindAsNoTracking(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes);
        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null);
        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes);
        Task<List<T>> ListAsNoTrackingAsync(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination, IEnumerable<string> includes = null);
        IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes);

        PagedList<T> PagedListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListAsNoTracking(ISpec<T> spec);

        IQueryable<T> ListAsNoTracking(ISpec<T> spec, IPagination pagination);

        Task<T> FindAsNoTrackingAsync(ISpec<T> spec);

        ValueTask<PagedList<TVm>> PagedListAsNoTrackingAsync<TVm>(
            ISpec<T> spec,
            IPagination pagination,
            Func<IQueryable<T>, IQueryable<TVm>> toVm)
            where TVm : class;

        ValueTask<PagedList<TVm>> ProjectedPagedListAsNoTrackingAsync<TVm>(
            Expression<Func<T, bool>> where,
            IPagination pagination,
            Expression<Func<T, TVm>> projection
        ) where TVm : class;

        ValueTask<PagedList<TVm>> ProjectedPagedListAsNoTrackingAsync<TVm>(
            ISpec<T> spec,
            IPagination pagination,
            Expression<Func<T, TVm>> projection
        ) where TVm : class;
    }
}
