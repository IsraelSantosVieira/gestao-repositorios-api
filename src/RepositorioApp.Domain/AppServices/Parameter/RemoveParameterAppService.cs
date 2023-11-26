using System;
using System.Threading.Tasks;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Parameter
{
    public class RemoveParameterAppService : BaseAppService, IRemoveParameterAppService
    {
        private readonly IParameterRepository _parameterRepository;

        public RemoveParameterAppService(IUnitOfWork uow, IParameterRepository parameterRepository) : base(uow)
        {
            _parameterRepository = parameterRepository;
        }

        public async Task<ParameterVm> Remove(Guid id)
        {
            var remover = await _parameterRepository.FindAsync(x => x.Id == id);
            if (remover == null)
                throw new DomainException(ParameterMessages.ParameterNotExists);

            _parameterRepository.Remove(remover);

            return await CommitAsync()
                ? remover.ToVm()
                : null;
        }
    }

    public interface IRemoveParameterAppService
    {
        Task<ParameterVm> Remove(Guid id);
    }
}
