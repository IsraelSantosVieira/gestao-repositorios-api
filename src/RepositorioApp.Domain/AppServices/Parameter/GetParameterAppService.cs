using System;
using RepositorioApp.Domain.AppServices.Parameter.Commands;
using RepositorioApp.Domain.Contracts.Repositories;
using RepositorioApp.Domain.Enums;
using RepositorioApp.Domain.Messages;
using RepositorioApp.Utilities.Exceptions;
using RepositorioApp.Utilities.Persistence;
namespace RepositorioApp.Domain.AppServices.Parameter
{
    public class GetParameterAppService : BaseAppService, IGetParameterAppService
    {
        private readonly IParameterRepository _parameterRepository;

        public GetParameterAppService(IUnitOfWork uow, IParameterRepository parameterRepository) : base(uow)
        {
            _parameterRepository = parameterRepository;
        }

        public T ReadParameter<T>(GetParameterCmd command)
        {
            var parameter = _parameterRepository.Find(x => 
                x.Transaction == command.Transaction && x.Group == command.Group);
            
            if (parameter == null)
            {
                throw new DomainException(ParameterMessages.ParameterNotExists);
            }

            return parameter.Type switch
            {
                EParameterType.Text => (T)Convert.ChangeType(parameter.Value, typeof(T)),
                EParameterType.Integer => (T)Convert.ChangeType(int.Parse(parameter.Value), typeof(T)),
                EParameterType.Decimal => (T)Convert.ChangeType(double.Parse(parameter.Value), typeof(T)),
                EParameterType.Boolean => (T)Convert.ChangeType(bool.Parse(parameter.Value), typeof(T)),
                _ => default
            };
        }
    }

    public interface IGetParameterAppService
    {
        T ReadParameter<T>(GetParameterCmd command);
    }
}
