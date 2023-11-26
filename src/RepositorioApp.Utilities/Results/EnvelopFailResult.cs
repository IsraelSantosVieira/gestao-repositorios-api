using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Utilities.Notifications;
namespace RepositorioApp.Utilities.Results
{

    public class EnvelopFailResult : EnvelopResult
    {
        public EnvelopFailResult()
        {
            Success = false;
        }

        public IEnumerable<string> Errors { get; set; }

        public static new EnvelopFailResult Fail()
        {
            return new EnvelopFailResult
            {
                Success = false
            };
        }

        public static new EnvelopFailResult Fail(IEnumerable<Notification> notifications)
        {
            return new EnvelopFailResult
            {
                Errors = notifications.Select(x => x.Value),
                Success = false
            };
        }
    }
}
