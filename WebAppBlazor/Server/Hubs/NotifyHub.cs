using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebAppBlazor.Server.Hubs
{
    public class NotifyHub : Hub
    {
        /// <summary>
        /// Send a message to all clients
        /// </summary>
        /// <param name="username"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        //public async Task SendMessage(string username, string message)
        //{
        //    await Clients.All.SendAsync(Messages.RECEIVE, username, message);
        //}

        /// <summary>
        /// Log connection
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Log disconnection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception e)
        {
            await base.OnDisconnectedAsync(e);
        }
    }
}
