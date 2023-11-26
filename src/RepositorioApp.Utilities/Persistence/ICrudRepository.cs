using System.Collections.Generic;
using System.Threading.Tasks;
namespace RepositorioApp.Utilities.Persistence
{
    public interface ICrudRepository<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);

        void Modify(T entity);
        void Remove(T entity);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}
