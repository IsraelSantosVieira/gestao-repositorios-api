using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Persistence.Ef
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public void PersistChanges()
        {
            _context.SaveChanges();
        }

        public async Task PersistChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.ChangeTracker.HasChanges()
                ? _context.SaveChanges()
                : 1;
        }

        public async Task<int> SaveChangesAsync()
        {
            return _context.ChangeTracker.HasChanges()
                ? await _context.SaveChangesAsync()
                : 1;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            try
            {
                _context.Database.CurrentTransaction?.Commit();
            }
            catch (Exception e)
            {
                RollbackTransaction();
                throw new Exception(e.InnerException?.Message ?? e.Message);
            }
        }

        public void RollbackTransaction()
        {
            _context.Database.CurrentTransaction?.Rollback();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            _context?.Dispose();
        }
    }
}
