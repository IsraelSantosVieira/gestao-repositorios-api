using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Results;
using RepositorioApp.Domain.ViewsModels.Emails;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class PasswordRecoverRequestAppService : BaseAppService, IPasswordRecoverRequestAppService
    {
        private readonly IJobService _jobService;
        private readonly IUserRepository _userRepository;

        public PasswordRecoverRequestAppService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            IJobService jobService) : base(uow)
        {
            _userRepository = userRepository;
            _jobService = jobService;
        }

        public async Task<PasswordRecoverResult> Generate(RecoverPasswordCmd command)
        {
            var result = new PasswordRecoverResult
            {
                Message = UserMessages._RecoverPassword.MessageResult(command.Email)
            };

            var user = await _userRepository.FindCompleteByEmailAsync(command.Email);

            if (user == null) return result;

            var recoverRequest = user.GeneratePasswordRecoverRequest();

            _userRepository.Modify(user);

            if (!await CommitAsync()) return null;

            var vm = new PasswordRecoverRequestEmailVm
            {
                Code = recoverRequest.Code,
                Email = user.Email,
                Name = user.FirstName
            };

            _jobService.SendPasswordRecoverRequestEmailWithBackgroundJob(vm);

            return result;
        }
    }

    public interface IPasswordRecoverRequestAppService
    {
        Task<PasswordRecoverResult> Generate(RecoverPasswordCmd command);
    }
}
