using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Persistence.Ef;
namespace RepositorioApp.Data.Repositories
{
    public class EducationalRoleUniversity : Repository<EducationalRole, DataContext>, IEducationalRoleRepository
    {
        public EducationalRoleUniversity(DataContext context) : base(context)
        {
        }
    }
}
