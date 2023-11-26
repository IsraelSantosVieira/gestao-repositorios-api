using System;
using System.Threading.Tasks;
using RepositorioApp.CrossCutting.Constants;
using RepositorioApp.CrossCutting.Models;
using RepositorioApp.Domain.AppServices.Users.Commands;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Results;
using RepositorioApp.Extensions;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Users
{
    public class UpdateAvatarAppService : BaseAppService, IUpdateAvatarAppService
    {
        private readonly ISessionProvider _sessionProvider;
        private readonly IStorageService _storageService;
        private readonly IUserRepository _userRepository;

        public UpdateAvatarAppService(
            IUnitOfWork uow,
            IUserRepository userRepository,
            IStorageService storageService,
            ISessionProvider sessionProvider) : base(uow)
        {
            _userRepository = userRepository;
            _storageService = storageService;
            _sessionProvider = sessionProvider;
        }

        public async Task<UpdateAvatarResult> Update(UpdateAvatarCmd command)
        {
            var userId = _sessionProvider.Id;

            var user = await _userRepository.FindAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new DomainException(UserMessages.NotExistsError);
            }

            var avatarUrl = await StoreImage(command.Image, userId);

            if (string.IsNullOrWhiteSpace(avatarUrl))
            {
                throw new DomainException(AppMessages.ErrorSavingS3File);
            }

            user = user.WithAvatar(avatarUrl);

            _userRepository.Modify(user);

            return await CommitAsync()
                ? new UpdateAvatarResult
                {
                    Url = avatarUrl
                }
                : null;
        }

        private async Task<string> StoreImage(FileUpload image, Guid userId)
        {
            return await _storageService.UploadAsync(
                image.Buffer,
                $"{_storageService.Config.UsersAvatarPath}/{userId}{image.ContentType.GetExtensionFromMimeType()}");
        }
    }

    public interface IUpdateAvatarAppService
    {
        Task<UpdateAvatarResult> Update(UpdateAvatarCmd command);
    }
}
