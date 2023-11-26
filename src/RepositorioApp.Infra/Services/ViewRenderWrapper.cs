using System.Threading.Tasks;
using Razor.Templating.Core;
using RepositorioApp.CrossCutting.Contracts;
namespace RepositorioApp.Infra.Services
{
    public sealed class ViewRenderWrapper : IViewRenderWrapper
    {
        public Task<string> RenderAsync(string viewPath, object model)
        {
            return RazorTemplateEngine.RenderAsync(viewPath, model);
        }
    }
}
