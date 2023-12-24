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
    public class ResetPasswordAppService : BaseAppService, IResetPasswordAppService
    {
        private readonly IUserRepository _userRepository;

        public ResetPasswordAppService(IUnitOfWork uow, IUserRepository userRepository) : base(uow)
        {
            _userRepository = userRepository;
        }

        public async Task<ChangeOrResetPasswordResult> Reset(ResetPasswordCmd command)
        {
            command.Code = command.Code.RemoveSpaces();
            
            var user = (await _userRepository.FindCompleteByEmailAsync(command.Email))?
                .ResetPasswordWithCode(command.Code, command.NewPassword);

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

    public interface IResetPasswordAppService
    {
        Task<ChangeOrResetPasswordResult> Reset(ResetPasswordCmd command);
    }
}
