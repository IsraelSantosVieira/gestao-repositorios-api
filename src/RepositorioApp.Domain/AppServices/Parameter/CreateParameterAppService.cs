using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Parameter.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Enums;
using RepositorioApp.Domain.Projections;
using RepositorioApp.Domain.ViewsModels;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Parameter
{
    public class CreateParameterAppService : BaseAppService, ICreateParameterAppService
    {
        private readonly IParameterRepository _parameterRepository;

        public CreateParameterAppService(IUnitOfWork uow, IParameterRepository parameterRepository) : base(uow)
        {
            _parameterRepository = parameterRepository;
        }

        public async Task<ParameterVm> Create(CreateParameterCmd command)
        {
            var parameter = new Entities.Parameter(
                command.Transaction, 
                command.Group, 
                command.Description,
                command.Value, 
                command.Type ?? EParameterType.Text);

            await _parameterRepository.AddAsync(parameter);

            return await CommitAsync() ? parameter.ToVm() : null;
        }
    }

    public interface ICreateParameterAppService
    {
        Task<ParameterVm> Create(CreateParameterCmd command);
    }
}
