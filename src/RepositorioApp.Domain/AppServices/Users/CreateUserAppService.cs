using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Entities;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.Results;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Domain.ViewsModels.Emails;
using RepositorioApp.Extensions;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class CreateUserAppService : BaseAppService, ICreateUserAppService
    {
        private readonly IJobService _jobService;
        private readonly IUserRepository _userRepository;

        public CreateUserAppService(
            IUnitOfWork uow,
            IJobService jobService,
            IUserRepository userRepository) : base(uow)
        {
            _jobService = jobService;
            _userRepository = userRepository;
        }

        public async Task<UserVm> Create(CreateUserCmd command)
        {
            if (!command.Agreements)
            {
                throw new DomainException(UserMessages.AcceptTermRequired);
            }
            
            var newUser = new User(
                command.Email.ToLower(),
                command.FirstName,
                command.Phone,
                command.University,
                command.EducationalRole,
                command.BirthDate,
                command.LastName);

            return await CreateUserWithCode(newUser);
        }

        private async Task<UserVm> CreateUserWithCode(User newUser)
        {
            if (await _userRepository.AnyAsync(u => u.Email == newUser.Email))
            {
                throw new DomainException(UserMessages.EmailIsUsed);
            }

            string generateRandomPassword = string.Empty.GenerateRandomPassword();
            newUser?.WithPassword(generateRandomPassword);

            await _userRepository.AddAsync(newUser);
            if (!await CommitAsync())
            {
                return null;
            }

            await GenerateCode(newUser?.Email);
            return newUser.ToVm();
        }

        public async Task<PasswordRecoverResult> GenerateCode(string email)
        {
            var result = new PasswordRecoverResult
            {
                Message = UserMessages._RecoverPassword.MessageResult(email)
            };

            var user = await _userRepository.FindCompleteByEmailAsync(email);

            if (user == null) return result;

            var generatePasswordRecoverRequest = user.GeneratePasswordRecoverRequest();

            _userRepository.Modify(user);

            if (!await CommitAsync()) return null;

            var vm = new NewCodeGenerateEmailVm
            {
                Code = generatePasswordRecoverRequest.Code,
                Email = user.Email,
                Name = user.FirstName
            };

            _jobService.SendCodeEmailWithBackgroundJob(vm);
            return result;
        }
    }

    public interface ICreateUserAppService
    {
        Task<UserVm> Create(CreateUserCmd command);
    }
}
