using System;
using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Parameter.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Parameter
{
    public class UpdateParameterAppService : BaseAppService, IUpdateParameterAppService
    {
        private readonly IParameterRepository _parameterRepository;

        public UpdateParameterAppService(IUnitOfWork uow, IParameterRepository parameterRepository) : base(uow)
        {
            _parameterRepository = parameterRepository;
        }

        public async Task<ParameterVm> Update(UpdateParameterCmd command)
        {
            var parameter = await _parameterRepository.FindAsNoTrackingAsync(x => x.Id == command.Id);

            if (parameter is null)
            {
                throw new DomainException(ParameterMessages.ParameterNotExists);
            }

            parameter.Update(
                command.Transaction, 
                command.Group,
                command.Description, 
                command.Value, 
                command.Type ?? parameter.Type,
                command.Active ?? true);

            _parameterRepository.Modify(parameter);

            return await CommitAsync() ? parameter.ToVm() : null;
        }

        public async Task<bool> UpdateStatus(Guid id)
        {
            var parameter = await _parameterRepository.FindAsync(x => x.Id == id);

            if (parameter == null)
            {
                throw new DomainException(ParameterMessages.ParameterNotExists);
            }

            parameter.UpdateStatus();

            return await CommitAsync();
        }
    }

    public interface IUpdateParameterAppService
    {
        Task<ParameterVm> Update(UpdateParameterCmd command);
        Task<bool> UpdateStatus(Guid id);
    }
}
