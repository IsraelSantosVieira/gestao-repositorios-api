using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Integration.Commands
{
    public class IntegrationDataCmd
    {
        [Required]
        public string ClientId { get; set; }

        [Required] [EmailAddress]
        public string Email { get; set; }

        public string IntegrationCode { get; set; }
    }
}
