using Microsoft.EntityFrameworkCore;
using RepositorioApp.Jwt.Models;
namespace RepositorioApp.Jwt.Ef
{
    public interface ISecurityKeyContext
    {
        DbSet<SecurityKey> SecurityKeys { get; set; }
        public DbContext DbContext { get; }
    }
}
