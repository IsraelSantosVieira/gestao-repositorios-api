using System;
namespace RepositorioApp.Domain.ViewsModels
{
    public class ArticleVm
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public string Link { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
