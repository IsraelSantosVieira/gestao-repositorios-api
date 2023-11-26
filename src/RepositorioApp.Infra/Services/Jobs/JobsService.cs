using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using RepositorioApp.Domain.Contracts.Infra;
using RepositorioApp.Domain.ViewsModels.Emails;
namespace RepositorioApp.Infra.Services.Jobs
{
    public class JobsService : IJobService
    {
        private readonly JobsEmails _jobsEmails;

        public JobsService(JobsEmails jobsEmails)
        {
            _jobsEmails = jobsEmails;
        }

        private static async IAsyncEnumerable<string> FilePreparer(byte[] buffer)
        {
            using var sw = new StreamReader(new MemoryStream(buffer));
            while (!sw.EndOfStream)
                yield return await sw.ReadLineAsync();
        }
        
        public bool SendNewUserEmailWithBackgroundJob(NewUserEmailVm vm)
        {
            return _jobsEmails.SendNewUserEmailWithBackgroundJob(vm);
        }
        
        public async Task<bool> SendNewUserEmail(NewUserEmailVm vm)
        {
            return await _jobsEmails.SendNewUserEmail(vm);
        }
        
        public bool SendCodeEmailWithBackgroundJob(NewCodeGenerateEmailVm vm)
        {
            return _jobsEmails.SendCodeEmailWithBackgroundJob(vm);
        }
        
        public bool SendPasswordRecoverRequestEmailWithBackgroundJob(PasswordRecoverRequestEmailVm vm)
        {
            return _jobsEmails.SendPasswordRecoverRequestEmailWithBackgroundJob(vm);
        }
        
        public async Task<bool> SendPasswordRecoverRequestEmail(PasswordRecoverRequestEmailVm vm)
        {
            return await _jobsEmails.SendPasswordRecoverRequestEmail(vm);
        }
    }
}
