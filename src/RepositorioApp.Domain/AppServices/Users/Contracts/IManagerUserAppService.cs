using System.Threading.Tasks;
using RepositorioApp.Domain.AppServices.Users.Commands;
namespace RepositorioApp.Domain.AppServices.Users.Contracts
{
    public interface IManagerUserAppService
    {
        Task ResendActivationCode(ActivationCodeCmd command);
    }
}
