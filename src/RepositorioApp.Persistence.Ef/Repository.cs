using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositorioApp.Utilities.Paging;
using RepositorioApp.Utilities.Persistence;
using RepositorioApp.Utilities.Spec;
namespace RepositorioApp.Persistence.Ef
{
    public class Repository<T, TContext> : IRepository<T>
        where T : class
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        #region Protected

        protected IQueryable<T> CurrentSet(
            Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null)
        {
            IQueryable<T> currentSet = _context.Set<T>();

            where ??= x => true;

            if (includes != null)
            {
                currentSet = includes.Where(include => !string.IsNullOrEmpty(include))
                    .Aggregate(currentSet, (current, include) => current.Include(include));
            }

            currentSet = currentSet.Where(where);

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortType))
            {
                var order = string.Join(",",
                    sortField.Split(",")
                        .Select(x => x.Replace(" ", ""))
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => $"{x} {sortType}")
                        .ToArray());
                currentSet = currentSet.OrderBy(order);
            }

            if (page != null && pageSize != null)
            {
                currentSet = currentSet
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return currentSet;
        }

        protected IQueryable<T> CurrentSet(
            Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> currentSet = _context.Set<T>();

            where ??= x => true;

            if (includes != null && includes.Any())
            {
                currentSet = includes.Aggregate(currentSet, (current, include) => current.Include(include));
            }

            currentSet = currentSet.Where(where);

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortType))
            {
                var order = string.Join(",",
                    sortField.Split(",")
                        .Select(x => x.Replace(" ", ""))
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => $"{x} {sortType}")
                        .ToArray());
                currentSet = currentSet.OrderBy(order);
            }

            if (page != null && pageSize != null)
            {
                currentSet = currentSet
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return currentSet;
        }

        #endregion

        #region CrudRepository

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public virtual void Modify(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        #endregion

        #region QueryRepository

        public virtual int Count(Expression<Func<T, bool>> where = null)
        {
            where ??= x => true;
            return _context.Set<T>().Count(where);
        }

        public virtual Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            where ??= x => true;
            return _context.Set<T>().CountAsync(where);
        }

        public virtual bool Any(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().Any(where);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return _context.Set<T>().AnyAsync(where);
        }

        public virtual T FindByKey(params object[] keyValues)
        {
            return _context.Set<T>().Find(keyValues);
        }

        public virtual ValueTask<T> FindByKeyAsync(params object[] keyValues)
        {
            return _context.Set<T>().FindAsync(keyValues);
        }

        public virtual T Find(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes).FirstOrDefault(where);
        }

        public virtual Task<T> FindAsync(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes).FirstOrDefaultAsync(where);
        }

        public virtual T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes).FirstOrDefault(where);
        }

        public virtual Task<T> FindAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes).FirstOrDefaultAsync(where);
        }

        public virtual IQueryable<T> List(IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes);
        }

        public virtual IQueryable<T> List(params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes);
        }


        public virtual IQueryable<T> List(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null)
        {
            return CurrentSet(where, page, pageSize, sortField, sortType, includes);
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(where, page, pageSize, sortField, sortType, includes);
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> where, IPagination pagination, IEnumerable<string> includes = null)
        {
            return CurrentSet(where,
                pagination.PageIndex,
                pagination.PageSize,
                pagination.SortField,
                pagination.SortType,
                includes);
        }

        public virtual IQueryable<T> List(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(where,
                pagination.PageIndex,
                pagination.PageSize,
                pagination.SortField,
                pagination.SortType,
                includes);
        }

        public virtual PagedList<T> PagedList(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes)
        {
            var total = Count(where);

            var itens = List(where, pagination, includes);

            return new PagedList<T>(itens, total, pagination.PageSize);
        }

        public virtual IQueryable<T> List(ISpec<T> spec)
        {
            return List(spec.Predicate, includes: spec.Includes.ToArray());
        }

        public virtual IQueryable<T> List(ISpec<T> spec, IPagination pagination)
        {
            return List(spec.Predicate, pagination, spec.Includes.ToArray());
        }

        public virtual Task<T> FindAsync(ISpec<T> spec)
        {
            return List(spec.Predicate, includes: spec.Includes.ToArray()).FirstOrDefaultAsync();
        }

        #endregion

        #region QueryAsNoTrackingRepository

        public virtual T FindAsNoTracking(ISpec<T> spec)
        {
            return List(spec.Predicate, includes: spec.Includes.ToArray()).AsNoTracking().FirstOrDefault();
        }

        public virtual T FindAsNoTracking(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes).AsNoTracking().FirstOrDefault(where);
        }

        public virtual Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes).AsNoTracking().FirstOrDefaultAsync(where);
        }

        public virtual T FindAsNoTracking(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes).AsNoTracking().FirstOrDefault(where);
        }

        public virtual Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes).AsNoTracking().FirstOrDefaultAsync(where);
        }

        public virtual IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null)
        {
            return CurrentSet(where, page, pageSize, sortField, sortType, includes).AsNoTracking();
        }

        public virtual IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(where, page, pageSize, sortField, sortType, includes).AsNoTracking();
        }

        public virtual Task<List<T>> ListAsNoTrackingAsync(Expression<Func<T, bool>> where = null, int? page = null, int? pageSize = null,
            string sortField = null, string sortType = null, params Expression<Func<T, object>>[] includes)
        {
            return ListAsNoTracking(where, page, pageSize, sortField, sortType, includes).ToListAsync();
        }

        public virtual IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination, IEnumerable<string> includes = null)
        {
            return ListAsNoTracking(where, pagination.PageIndex, pagination.PageSize, pagination.SortField, pagination.SortType, includes);
        }

        public virtual IQueryable<T> ListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes)
        {
            return ListAsNoTracking(where, pagination.PageIndex, pagination.PageSize, pagination.SortField, pagination.SortType, includes);
        }

        public virtual PagedList<T> PagedListAsNoTracking(Expression<Func<T, bool>> where, IPagination pagination, params Expression<Func<T, object>>[] includes)
        {
            var total = _context.Set<T>().Count(where);

            var itens = ListAsNoTracking(where, pagination, includes);

            return new PagedList<T>(itens, total, pagination.PageSize);
        }

        public virtual IQueryable<T> ListAsNoTracking(ISpec<T> spec)
        {
            return ListAsNoTracking(spec.Predicate, includes: spec.Includes.ToArray());
        }

        public virtual IQueryable<T> ListAsNoTracking(ISpec<T> spec, IPagination pagination)
        {
            return ListAsNoTracking(spec.Predicate, pagination, spec.Includes.ToArray());
        }

        public virtual Task<T> FindAsNoTrackingAsync(ISpec<T> spec)
        {
            return ListAsNoTracking(spec.Predicate, includes: spec.Includes.ToArray()).FirstOrDefaultAsync();
        }

        public virtual async ValueTask<PagedList<TVm>> PagedListAsNoTrackingAsync<TVm>(
            ISpec<T> spec,
            IPagination pagination,
            Func<IQueryable<T>, IQueryable<TVm>> toVm)
            where TVm : class
        {
            var total = await CountAsync(spec.Predicate);
            var items = toVm(ListAsNoTracking(spec.Predicate, pagination, spec.Includes.ToArray())).ToList();
            return new PagedList<TVm>(items, total, pagination.PageSize);
        }



        public virtual async ValueTask<PagedList<TVm>> ProjectedPagedListAsNoTrackingAsync<TVm>(
            Expression<Func<T, bool>> where,
            IPagination pagination,
            Expression<Func<T, TVm>> projection)
            where TVm : class
        {
            var currentSet = _context.Set<T>().AsNoTracking().Where(where).Select(projection);

            if (!string.IsNullOrEmpty(pagination.SortField) && !string.IsNullOrEmpty(pagination.SortType))
            {
                var order = string.Join(",",
                    pagination.SortField.Split(",")
                        .Select(x => x.Replace(" ", ""))
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => $"{x} {pagination.SortType}")
                        .ToArray());

                currentSet = currentSet.OrderBy(order);
            }

            currentSet = currentSet
                .Skip((pagination.PageIndex - 1) * pagination.PageSize)
                .Take(pagination.PageSize);

            return new PagedList<TVm>(currentSet.ToArray(), await CountAsync(where), pagination.PageSize);
        }
        public virtual async ValueTask<PagedList<TVm>> ProjectedPagedListAsNoTrackingAsync<TVm>(
            ISpec<T> spec,
            IPagination pagination,
            Expression<Func<T, TVm>> projection)
            where TVm : class
        {
            return await ProjectedPagedListAsNoTrackingAsync(spec.Predicate, pagination, projection);
        }

        #endregion

        #region QueryAsNoTrackingWithIdentityResolutionRepository

        public virtual T FindAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes).AsNoTrackingWithIdentityResolution().FirstOrDefault(where);
        }

        public virtual Task<T> FindAsNoTrackingWithIdentityResolutionAsync(Expression<Func<T, bool>> where,
            IEnumerable<string> includes = null)
        {
            return CurrentSet(includes: includes).AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(where);
        }

        public virtual T FindAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes).AsNoTrackingWithIdentityResolution().FirstOrDefault(where);
        }

        public virtual Task<T> FindAsNoTrackingWithIdentityResolutionAsync(Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(includes: includes).AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(where);
        }

        public virtual IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            IEnumerable<string> includes = null)
        {
            return CurrentSet(where, page, pageSize, sortField, sortType, includes).AsNoTrackingWithIdentityResolution();
        }

        public virtual IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where = null,
            int? page = null,
            int? pageSize = null,
            string sortField = null,
            string sortType = null,
            params Expression<Func<T, object>>[] includes)
        {
            return CurrentSet(where, page, pageSize, sortField, sortType, includes).AsNoTrackingWithIdentityResolution();
        }

        public virtual IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where, IPagination pagination, IEnumerable<string> includes = null)
        {
            return ListAsNoTrackingWithIdentityResolution(where, pagination.PageIndex, pagination.PageSize, pagination.SortField, pagination.SortType, includes);
        }

        public virtual IQueryable<T> ListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes)
        {
            return ListAsNoTrackingWithIdentityResolution(where, pagination.PageIndex, pagination.PageSize, pagination.SortField, pagination.SortType, includes);
        }

        public virtual PagedList<T> PagedListAsNoTrackingWithIdentityResolution(Expression<Func<T, bool>> where,
            IPagination pagination,
            params Expression<Func<T, object>>[] includes)
        {
            var total = _context.Set<T>().Count(where);

            var itens = ListAsNoTrackingWithIdentityResolution(where, pagination, includes);

            return new PagedList<T>(itens, total, pagination.PageSize);
        }

        #endregion
    }
}
