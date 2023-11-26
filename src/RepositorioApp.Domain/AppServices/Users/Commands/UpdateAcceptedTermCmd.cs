using System;
using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Users.Commands
{
    public class UpdateAcceptedTermCmd
    {
        [Required]
        public Guid RequesterUserId { get; set; }
    }
}
