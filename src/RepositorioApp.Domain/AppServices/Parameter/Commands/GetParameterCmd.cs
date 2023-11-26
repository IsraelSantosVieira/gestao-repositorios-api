using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.Domain.AppServices.Parameter.Commands
{
    public class GetParameterCmd
    {
        [Required]
        public string Transaction { get; set; }
        [Required]
        public string Group { get; set; }
    }
}
