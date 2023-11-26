using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Repositories
{
    public sealed class UploadFileRepository : Repository<UploadFile, DataContext>, IUploadFileRepository
    {
        public UploadFileRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
