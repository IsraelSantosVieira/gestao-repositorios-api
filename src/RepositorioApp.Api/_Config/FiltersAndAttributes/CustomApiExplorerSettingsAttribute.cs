using Microsoft.AspNetCore.Mvc;
using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Api._Config.FiltersAndAttributes
{
    public class CustomApiExplorerSettingsAttribute : ApiExplorerSettingsAttribute
    {
        public CustomApiExplorerSettingsAttribute(string key, bool ignoreApi = false)
        {
            GroupName = AppConstants.ApisConfiguration[key].Group;
            IgnoreApi = ignoreApi;
        }
    }
}
