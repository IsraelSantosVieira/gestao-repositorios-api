using System;
using System.Threading.Tasks;
using Hangfire;
using RepositorioApp.CrossCutting.Contracts;
using RepositorioApp.Domain.ViewsModels.Emails;
using RepositorioApp.Mail;
namespace RepositorioApp.Infra.Services.Jobs
{
    public class JobsEmails
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly ILoggerStorageService _logger;
        private readonly IMailService _mailService;
        private readonly IViewRenderWrapper _viewRenderWrapper;
        
        public JobsEmails(
            IMailService mailService, 
            IViewRenderWrapper viewRenderWrapper, 
            IBackgroundJobClient backgroundJobClient, 
            ILoggerStorageService logger)
        {
            _mailService = mailService;
            _viewRenderWrapper = viewRenderWrapper;
            _backgroundJobClient = backgroundJobClient;
            _logger = logger;
        }
        
        public bool SendNewUserEmailWithBackgroundJob(NewUserEmailVm vm)
        {
            try
            {
                _backgroundJobClient.Enqueue(() => SendNewUserEmail(vm));
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public async Task<bool> SendNewUserEmail(NewUserEmailVm vm)
        {
            try
            {
                var html = await _viewRenderWrapper.RenderAsync("Emails/NewUser", vm);
                _mailService.Send(vm.Email, vm.Subject, html, true);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool SendPasswordRecoverRequestEmailWithBackgroundJob(PasswordRecoverRequestEmailVm vm)
        {
            try
            {
                _backgroundJobClient.Enqueue(() => SendPasswordRecoverRequestEmail(vm));
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return false;
            }
        }

        public async Task<bool> SendPasswordRecoverRequestEmail(PasswordRecoverRequestEmailVm vm)
        {
            try
            {
                var html = await _viewRenderWrapper.RenderAsync("Emails/PasswordRecoverRequest", vm);
                _mailService.Send(vm.Email, vm.Subject, html, true);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        public bool SendCodeEmailWithBackgroundJob(NewCodeGenerateEmailVm vm)
        {
            try
            {
                _backgroundJobClient.Enqueue(() => SendEmailWithCodeRequestEmail(vm));
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return false;
            }
        }

        public async Task<bool> SendEmailWithCodeRequestEmail(NewCodeGenerateEmailVm vm)
        {
            try
            {
                var html = await _viewRenderWrapper.RenderAsync("Emails/NewUserWithCode", vm);
                _mailService.Send(vm.Email, vm.Subject, html, true);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }
    }
}
