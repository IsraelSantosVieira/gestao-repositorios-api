using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Repositories
{
    public class UniversityRepository : Repository<University, DataContext>, IUniversityRepository
    {
        public UniversityRepository(DataContext context) : base(context)
        {
        }
    }
}
