using System;
using System.Collections.Generic;
using System.Linq;
using RepositorioApp.Utilities.Notifications;
namespace RepositorioApp.Utilities.Exceptions
{
    public class DomainException : Exception
    {
        private const string ExceptionMessage = "Domain Exception";

        public DomainException(params string[] notifications) : base(ExceptionMessage)
        {
            Notifications = notifications.Select(x => new Notification(x));
        }

        public DomainException(Exception innerException, params string[] notifications) : base(ExceptionMessage, innerException)
        {
            Notifications = notifications.Select(x => new Notification(x));
        }

        public DomainException(IEnumerable<string> notifications) : base(ExceptionMessage)
        {
            Notifications = notifications.Select(x => new Notification(x));
        }

        public DomainException(Exception innerException, IEnumerable<string> notifications) : base(ExceptionMessage, innerException)
        {
            Notifications = notifications.Select(x => new Notification(x));
        }

        public DomainException(params Notification[] notifications) : base(ExceptionMessage)
        {
            Notifications = notifications;
        }

        public DomainException(Exception innerException, params Notification[] notifications) : base(ExceptionMessage, innerException)
        {
            Notifications = notifications;
        }

        public DomainException(IEnumerable<Notification> notifications) : base(ExceptionMessage)
        {
            Notifications = notifications;
        }

        public DomainException(Exception innerException, IEnumerable<Notification> notifications) : base(ExceptionMessage, innerException)
        {
            Notifications = notifications;
        }

        public IEnumerable<string> Errors => Notifications.Select(x => x.Value);

        public IEnumerable<Notification> Notifications { get; }
    }
}
