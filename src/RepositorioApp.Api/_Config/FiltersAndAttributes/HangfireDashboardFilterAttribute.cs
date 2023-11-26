using System.Threading.Tasks;
using Hangfire.Dashboard;
namespace RepositorioApp.Api._Config.FiltersAndAttributes
{
    public class HangfireDashboardFilterAttribute : IDashboardAsyncAuthorizationFilter
    {
        public async Task<bool> AuthorizeAsync(DashboardContext context)
        {
            return true;
        }
    }
}
