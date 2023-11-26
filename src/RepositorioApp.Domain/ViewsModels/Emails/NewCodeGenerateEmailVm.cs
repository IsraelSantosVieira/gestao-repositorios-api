using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Domain.ViewsModels.Emails
{
    public class NewCodeGenerateEmailVm : BaseEmailVm
    {

        public override string Subject => AppConstants.Emails.Subjects.NewUser;
        public override string Email { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
