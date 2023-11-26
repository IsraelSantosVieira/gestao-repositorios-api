using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class UpdateStatusCmd
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public Guid RequesterUserId { get; set; }
    }
}
