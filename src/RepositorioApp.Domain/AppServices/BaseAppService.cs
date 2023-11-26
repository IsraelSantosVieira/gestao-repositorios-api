using System.Threading.Tasks;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices
{
    public class BaseAppService
    {
        private readonly IUnitOfWork _uow;

        public BaseAppService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected void BeginTransaction()
        {
            _uow.BeginTransaction();
        }

        protected void CommitTransaction()
        {
            _uow.CommitTransaction();
        }

        protected void RollbackTransaction()
        {
            _uow.RollbackTransaction();
        }

        protected async Task<bool> CommitAsync(bool throwIfFails = true)
        {
            if (await _uow.SaveChangesAsync() > 0) return true;

            if (throwIfFails)
                throw new DomainException(AppMessages.ProblemSavindDataFriendly);

            return false;
        }
    }
}
