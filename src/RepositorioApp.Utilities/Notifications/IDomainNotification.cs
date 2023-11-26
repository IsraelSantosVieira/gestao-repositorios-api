using System.Collections.Generic;
namespace RepositorioApp.Utilities.Notifications
{
    public interface IDomainNotification
    {
        List<Notification> Notifications { get; }
        IEnumerable<string> Values { get; }
        void Handle(string value);
        void Handle(params string[] values);
        void Handle(string key, string value);
        void Handle(Notification item);
        void Handle(params Notification[] items);
        IEnumerable<Notification> Notify();
        bool HasNotifications();
        void Dispose();
    }
}
