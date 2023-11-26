using System.Threading.Tasks;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class RemoveAvatarAppService : BaseAppService, IRemoveAvatarAppService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IStorageService _storageService;
        private readonly IUserRepository _userRepository;

        public RemoveAvatarAppService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            IStorageService storageService,
            ISessionProvider sessionProvider) : base(uow)
        {
            _userRepository = userRepository;
            _storageService = storageService;
            _sessionProvider = sessionProvider;
        }

        public async Task<bool> Remove()
        {
            User user = await _userRepository.FindAsync(x => x.Id == _sessionProvider.Id);

            if (user == null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            await _storageService.RemoveByUrlAsync(user.Avatar);
            user = user.WithAvatar(null);

            _userRepository.Modify(user);

            return await CommitAsync();
        }
    }

    public interface IRemoveAvatarAppService
    {
        Task<bool> Remove();
    }
}
