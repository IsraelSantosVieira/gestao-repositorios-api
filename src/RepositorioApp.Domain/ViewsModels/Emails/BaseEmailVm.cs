namespace RepositorioApp.Domain.ViewsModels.Emails
{
    public abstract class BaseEmailVm
    {
        public abstract string Subject { get; }
        public abstract string Email { get; set; }
    }
}
