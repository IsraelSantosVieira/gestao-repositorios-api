using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class UpdateUserAppService : BaseAppService, IUpdateUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserAppService(
            IUnitOfWork uow,
            IUserRepository userRepository) : base(uow)
        {
            _userRepository = userRepository;
        }

        public async Task<UserVm> Update(UpdateProfileCmd command)
        {
            var userApp = await _userRepository.FindAsync(x => x.Id == command.Id, x => x.Phone);

            if (userApp == null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            userApp.UpdatePersonalData(
                command.FirstName, 
                command.LastName,
                command.Avatar,
                command.BirthDate,
                command.EducationalRole,
                command.University);

            if (!userApp.AcceptedTerm && command.AcceptedTerm)
            {
                userApp.UpdateAcceptedTerm(true);
            }

            _userRepository.Modify(userApp);

            return await CommitAsync()
                ? userApp.ToVm()
                : null;
        }

        public async Task<bool> UpdateStatus(UpdateStatusCmd command)
        {
            var user = await _userRepository.FindAsync(u => u.Id == command.Id);

            if (user == null)
                throw new DomainException(UserMessages.NotExistsError);

            user.UpdateStatus(command.RequesterUserId);
            return await CommitAsync();
        }

        public async Task<UserVm> UpdateAcceptedTerm(UpdateAcceptedTermCmd command)
        {
            var userApp = await _userRepository
                .FindAsNoTrackingAsync(where: x => x.Id == command.RequesterUserId);

            if (userApp == null)
                throw new DomainException(UserMessages.NotExistsError);

            userApp.UpdateAcceptedTerm(null);
            _userRepository.Modify(userApp);

            return await CommitAsync() ? userApp.ToVm() : null;
        }
    }

    public interface IUpdateUserAppService
    {
        Task<UserVm> Update(UpdateProfileCmd command);
        Task<bool> UpdateStatus(UpdateStatusCmd command);
        Task<UserVm> UpdateAcceptedTerm(UpdateAcceptedTermCmd command);
    }
}
