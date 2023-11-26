using System;
namespace RepositorioApp.Domain.Entities
{
    public class Log
    {
        public Guid Id { get; set; }

        public DateTime? OccurredAt { get; set; }

        public string Level { get; set; }

        public string Logger { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }
    }
}
