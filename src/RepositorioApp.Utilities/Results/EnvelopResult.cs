using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Utilities.Notifications;
namespace RepositorioApp.Utilities.Results
{

    public class EnvelopResult
    {
        public bool Success { get; set; }

        public static EnvelopResult Ok()
        {
            return new EnvelopResult
            {
                Success = true
            };
        }


        public static EnvelopDataResult<T> Ok<T>(T data)
        {
            return EnvelopDataResult<T>.Ok(data);
        }

        public static EnvelopFailResult Fail()
        {
            return new EnvelopFailResult
            {
                Success = false
            };
        }

        public static EnvelopFailResult Fail(IEnumerable<Notification> notifications)
        {
            return new EnvelopFailResult
            {
                Errors = notifications.Select(x => x.Value),
                Success = false
            };
        }
    }
}
