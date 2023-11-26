using RepositorioApp.Domain.Enums;
using RepositorioApp.Utilities.Paging;
namespace RepositorioApp.Domain.Filters
{
    public class ParameterFilter : Pagination
    {
        public string Group { get; set; }
        public string Transaction { get; set; }
        public EParameterType? Type { get; set; }
        public bool? Active { get; set; }
    }
}
