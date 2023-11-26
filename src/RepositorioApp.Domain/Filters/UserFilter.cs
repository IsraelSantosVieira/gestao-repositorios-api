using RepositorioApp.Utilities.Paging;
namespace RepositorioApp.Domain.Filters
{
    public class UserFilter : Pagination
    {
        public string Name { get; set; }

        public string Email { get; set; }
        
        public string Phone { get; set; }
    }
}
