using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Results;
using RepositorioApp.Extensions;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class UserActivateAppService : BaseAppService, IUserActivateAppService
    {
        private readonly IUserRepository _userRepository;

        public UserActivateAppService(IUnitOfWork uow, IUserRepository userRepository) : base(uow)
        {
            _userRepository = userRepository;
        }

        public async Task<UserActivateResult> Activate(UserActivateCmd command)
        {
            command.Code = command.Code.RemoveSpaces();
            
            var user = (await _userRepository.FindCompleteByEmailAsync(command.Email))?
                .UserActivateWithCode(command.Code, command.NewPassword);

            if (user == null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            _userRepository.Modify(user);

            if (!await CommitAsync(false))
                throw new DomainException(UserMessages._UserActivate.CodeInvalidError);

            return new UserActivateResult
            {
                Message = UserMessages._UserActivate.SuccessMessage
            };
        }
    }

    public interface IUserActivateAppService
    {
        Task<UserActivateResult> Activate(UserActivateCmd command);
    }
}
