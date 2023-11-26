using System.Collections.Generic;
using System.Linq;
namespace RepositorioApp.Utilities.Notifications
{
    public class DomainNotification : IDomainNotification
    {
        public List<Notification> Notifications { get; private set; } = new List<Notification>();

        public IEnumerable<string> Values => Notifications.Select(x => x.Value);

        public void Handle(string value)
        {
            Notifications.Add(new Notification(value));
        }

        public void Handle(params string[] values)
        {
            foreach (var value in values)
            {
                Notifications.Add(new Notification(value));
            }
        }

        public void Handle(string key, string value)
        {
            Notifications.Add(new Notification(key, value));
        }

        public void Handle(Notification item)
        {
            Notifications.Add(item);
        }

        public void Handle(params Notification[] items)
        {
            foreach (var item in items)
            {
                Notifications.Add(item);
            }
        }

        public IEnumerable<Notification> Notify()
        {
            return Notifications;
        }

        public bool HasNotifications()
        {
            return Notifications.Count > 0;
        }

        public void Dispose()
        {
            Notifications = new List<Notification>();
        }
    }
}
