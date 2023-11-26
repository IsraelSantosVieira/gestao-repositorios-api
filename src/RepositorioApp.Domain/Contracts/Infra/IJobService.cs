using System.Threading.Tasks;
using RepositorioApp.Domain.ViewsModels.Emails;
namespace RepositorioApp.Domain.Contracts.Infra
{
    public interface IJobService
    {
        bool SendNewUserEmailWithBackgroundJob(NewUserEmailVm vm);
        Task<bool> SendNewUserEmail(NewUserEmailVm vm);
        bool SendPasswordRecoverRequestEmailWithBackgroundJob(PasswordRecoverRequestEmailVm vm);
        bool SendCodeEmailWithBackgroundJob(NewCodeGenerateEmailVm vm);
        Task<bool> SendPasswordRecoverRequestEmail(PasswordRecoverRequestEmailVm vm);
    }
}
