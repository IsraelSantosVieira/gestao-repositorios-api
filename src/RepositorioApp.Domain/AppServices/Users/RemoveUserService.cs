using System.Threading.Tasks;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class RemoveUserService : BaseAppService, IRemoveUserService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IUserRepository _userRepository;

        public RemoveUserService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            ISessionProvider sessionProvider) : base(uow)
        {
            _userRepository = userRepository;
            _sessionProvider = sessionProvider;
        }

        public async Task<UserVm> Remove()
        {
            var userId = _sessionProvider.Id;
            var user = await _userRepository.FindAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            _userRepository.Remove(user);
            return await CommitAsync()
                ? user.ToVm()
                : null;
        }
    }

    public interface IRemoveUserService
    {
        Task<UserVm> Remove();
    }
}
