using System.ComponentModel.DataAnnotations;
using RepositorioApp.Domain.Enums;
namespace RepositorioApp.Domain.AppServices.Parameter.Commands
{
    public class CreateParameterCmd
    {
        [Required]
        public string Group { get; set; }
        [Required]
        public string Transaction { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Value { get; set; }

        public EParameterType? Type { get; set; }
    }
}
