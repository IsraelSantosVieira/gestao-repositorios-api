using System.Threading.Tasks;
namespace RepositorioApp.Utilities.Persistence
{
    public interface IUnitOfWork
    {
        void PersistChanges();
        Task PersistChangesAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
