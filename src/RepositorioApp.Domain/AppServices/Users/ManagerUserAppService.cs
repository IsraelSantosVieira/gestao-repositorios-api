using System.Threading.Tasks;
using Hangfire;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.AppServices.Users.Contracts;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.ViewsModels.Emails;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class ManagerUserAppService : BaseAppService, IManagerUserAppService
    {
        private readonly IJobService _jobService;
        private readonly IUserRepository _userRepository;

        public ManagerUserAppService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            IJobService jobService
        ) : base(uow)
        {
            _userRepository = userRepository;
            _jobService = jobService;
        }

        public async Task ResendActivationCode(ActivationCodeCmd command)
        {
            var user = await _userRepository.FindCompleteByEmailAsync(command.Email);

            if (user is null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            var generatePasswordRecoverRequest = user.GeneratePasswordRecoverRequest();

            _userRepository.Modify(user);

            if (!await CommitAsync())
            {
                throw new DomainException(UserMessages._RecoverPassword.GenericError);
            }

            var vm = new NewCodeGenerateEmailVm
            {
                Code = generatePasswordRecoverRequest.Code,
                Email = user.Email,
                Name = user.FirstName
            };

            _jobService.SendCodeEmailWithBackgroundJob(vm);
        }
    }

}
