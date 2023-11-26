using Microsoft.AspNetCore.Mvc;
using RepositorioApp.CrossCutting.Constants;
namespace RepositorioApp.Api._Config.FiltersAndAttributes
{
    public class CustomRouteAttribute : RouteAttribute
    {
        public CustomRouteAttribute(string key, string route)
            : base(AppConstants.ApisConfiguration[key].Route + route)
        {
        }
    }
}
