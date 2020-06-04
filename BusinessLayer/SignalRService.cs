using BusinessLayer.SignalR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CommonLayer.DTO;

namespace BusinessLayer
{
    public class SignalRService
    {
        public IHubContext<SignalRHub, ITypedHubClient> hubContext;
        public SignalRService(IHubContext<SignalRHub, ITypedHubClient> hubContext)
        {
            this.hubContext = hubContext;
        }
        //this is a single method which would be used globally for *ANY* type of SignalR broadcast. This would be intercepted at client & relevant module/component would be invoked
        public async Task BroadcastMessage(SignalRMessage message)
        {
            string msg = JsonConvert.SerializeObject(message);
            if (!string.IsNullOrEmpty(message.connectionID))
                await hubContext.Clients.AllExcept(message.connectionID).BroadcastMessage(msg);
            else
                await hubContext.Clients.All.BroadcastMessage(msg);
        }

        public async Task BroadcastRealTimeNotifications(SignalRMessage message)
        {
            string msg = JsonConvert.SerializeObject(message);
            if (!string.IsNullOrEmpty(message.connectionID))
            {
                if (message.sendTo == "self")
                    await hubContext.Clients.Client(message.connectionID).BroadcastRealTimeNotifications(msg);
                else if (message.sendTo == "others")
                    await hubContext.Clients.AllExcept(message.connectionID).BroadcastRealTimeNotifications(msg);
            }
            else
                await hubContext.Clients.All.BroadcastRealTimeNotifications(msg);
        }
    }
}
