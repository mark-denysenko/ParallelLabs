using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab7_Client
{
    class Program
    {
        private static HubConnection connection;
        static void Main(string[] args)
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:59949/chathub")
                //.WithUrl("http://localhost:44355/chathub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, string>("ReceiveMessage", (user, msg) => Console.WriteLine(user + " : " + msg));

            var task = connection.StartAsync();
            task.Wait();

            var message = string.Empty;
            while (message.ToLowerInvariant() != "x")
            {
                message = Console.ReadLine();
                connection.SendAsync("SendMessage", connection.ConnectionId, message).Wait();
            }
        }
    }
}
