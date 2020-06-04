using Microsoft.AspNetCore.SignalR;
namespace BusinessLayer.SignalR
{
    public class SignalRHub : Hub<ITypedHubClient>
    {
        public string GetConnectionID()
        {
            return Context.ConnectionId;
        }

    }
}
