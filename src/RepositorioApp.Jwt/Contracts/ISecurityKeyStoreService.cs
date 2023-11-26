using System.Collections.Generic;
using RepositorioApp.Jwt.Models;
namespace RepositorioApp.Jwt.Contracts
{
    public interface ISecurityKeyStoreService
    {
        SecurityKey AddSecurityKey();
        SecurityKey GetCurrent();
        ICollection<SecurityKey> GetCurrents();
    }
}
