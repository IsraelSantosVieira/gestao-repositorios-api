using RepositorioApp.Extensions;
namespace RepositorioApp.CrossCutting.Config
{
    public class AzureBlobConfig
    {
        public string Key { get; set; }

        public string ConnectionString { get; set; }

        public string PublicContainer { get; set; }

        public string PrivateContainer { get; set; }

        public string RootPath { get; set; }

        public string UsersAvatarPath => RootPath.AddPath("users-avatar");

    }
}
