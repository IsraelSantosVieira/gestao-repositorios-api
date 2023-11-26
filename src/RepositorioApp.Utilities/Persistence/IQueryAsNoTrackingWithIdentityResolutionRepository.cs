using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RepositorioApp.Utilities.Paging;
namespace RepositorioApp.Utilities.Persistence
{
    public interface IQueryAsNoTrackingWithIdentityResolutionRepository<T>
        where T : class
    {
        T FindAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);

        Task<T> FindAsNoTrackingWithIdentityResolutionAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null);
        T FindAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        Task<T> FindAsNoTrackingWithIdentityResolutionAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null);

        IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes);

        IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, IPagination pagination, IEnumerable<string> includes = null);

        IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes);

        PagedList<T> PagedListAsNoTrackingWithIdentityResolution(
            Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes
        );
    }
}
