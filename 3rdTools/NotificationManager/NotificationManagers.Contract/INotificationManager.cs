using System.Threading.Tasks;

namespace NotificationManagers.Contract;

public interface INotificationManager<T> where T : class
{
    ValueTask Publish(T message);
}
