using System;
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
    public class ChangeUserStatusAppService : BaseAppService, IChangeUserStatusAppService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IUserRepository _userRepository;

        public ChangeUserStatusAppService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            ISessionProvider sessionProvider) : base(uow)
        {
            _userRepository = userRepository;
            _sessionProvider = sessionProvider;
        }

        public async Task<UserVm> Change(Guid id)
        {
            var user = (
                await _userRepository.FindAsync(x => x.Id == id)
                )?.UpdateStatus(_sessionProvider.Id);

            if (user == null)
                throw new DomainException(UserMessages.NotExistsError);

            _userRepository.Modify(user);

            return await CommitAsync()
                ? user.ToVm()
                : null;
        }
    }

    public interface IChangeUserStatusAppService
    {
        Task<UserVm> Change(Guid id);
    }
}
