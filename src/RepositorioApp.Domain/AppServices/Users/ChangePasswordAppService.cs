using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Results;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class ChangePasswordAppService : BaseAppService, IChangePasswordAppService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IUserRepository _userRepository;

        public ChangePasswordAppService(IUnitOfWork uow, IUserRepository userRepository, ISessionProvider sessionProvider) : base(uow)
        {
            _userRepository = userRepository;
            _sessionProvider = sessionProvider;
        }

        public async Task<ChangeOrResetPasswordResult> Change(ChangePasswordCmd command)
        {
            var user = (await _userRepository.FindAsync(x => x.Id == _sessionProvider.Id))?
                .ChangePassword(command.CurrentPassword, command.NewPassword);

            if (user == null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            _userRepository.Modify(user);

            await CommitAsync();

            return new ChangeOrResetPasswordResult
            {
                Message = UserMessages._ResetPassword.SuccessMessage
            };
        }
    }

    public interface IChangePasswordAppService
    {
        Task<ChangeOrResetPasswordResult> Change(ChangePasswordCmd command);
    }
}
