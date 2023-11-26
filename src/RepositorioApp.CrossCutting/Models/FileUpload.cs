using System.ComponentModel.DataAnnotations;
namespace RepositorioApp.CrossCutting.Models
{
    public class FileUpload
    {
        [Required]
        public byte[] Buffer { get; set; }

        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        public string Url { get; set; }
    }
}
