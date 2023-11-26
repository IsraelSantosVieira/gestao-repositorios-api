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

        public string CustomersLogoPath => RootPath.AddPath("customers-logo");
        public string BannersPath => RootPath.AddPath("banners");
        public string FipeTablePath => RootPath.AddPath("fipe-tables");
        public string AdvertisementsPath => RootPath.AddPath("advertisements");
        public string ModelsPath => RootPath.AddPath("models");
        public string StoresPath => RootPath.AddPath("stores");
        public string CategoriesPath => RootPath.AddPath("categories");
        public string PartnerPath => RootPath.AddPath("parceiros");
    }
}
