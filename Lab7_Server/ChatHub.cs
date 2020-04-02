using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Lab7_Server
{
    public class ChatHub : Hub
    {
        public  override Task OnConnectedAsync()
        {
            SendMessage("[Server]", Context.ConnectionId + " connected!");

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            SendMessage("[Server]", Context.ConnectionId + " disconnected!");
            return base.OnDisconnectedAsync(exception);
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}