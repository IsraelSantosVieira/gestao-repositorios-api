using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Domain.ViewsModels.Emails
{
    public class PasswordRecoverRequestEmailVm : BaseEmailVm
    {
        public override string Subject => AppConstants.Emails.Subjects.PasswordRecoverRequest;
        public override string Email { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
