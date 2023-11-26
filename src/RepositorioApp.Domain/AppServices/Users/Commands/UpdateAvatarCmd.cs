using RepositorioApp.CrossCutting.Models;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class UpdateAvatarCmd
    {
        public FileUpload Image { get; set; }
    }
}
