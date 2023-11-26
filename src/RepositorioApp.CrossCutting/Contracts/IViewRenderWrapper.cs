using System.Threading.Tasks;
namespace RepositorioApp.CrossCutting.Contracts
{
    public interface IViewRenderWrapper
    {
        Task<string> RenderAsync(string viewPath, object model);
    }
}
