using System.Collections.Generic;
using Newtonsoft.Json;
using RepositorioApp.CrossCutting.Models;
namespace RepositorioApp.Domain.Results
{
    public class AuthenticateUserResult : JwToken
    {
        public SessionUser User { get; set; }

        public bool? RequiresTwoFactorAuthentication { get; set; }

        [JsonIgnore]
        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
