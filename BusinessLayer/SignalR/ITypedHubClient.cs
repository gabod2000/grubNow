using System.Threading.Tasks;

namespace BusinessLayer.SignalR
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string message);
        Task BroadcastRealTimeNotifications(string notifications);
    }
}
