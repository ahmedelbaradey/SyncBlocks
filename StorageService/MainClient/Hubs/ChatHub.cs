using MassTransit;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.SignalR;
using Shared.DataTransferObjects.SharedObject;

namespace MainClient.Hubs
{
    public class ChatHub : Hub
    {

        //private static IHubContext hubContext =Glo ConnectionManager.GetHubContext<ChatHub>();
        //public void Send(string channel, string content)
        //{
        //    Clients.Group(channel).addMessage(content);
        //}

        //// Call this from C#: NewsFeedHub.Static_Send(channel, content)
        //public static void Static_Send(string channel, string content)
        //{
        //    hubContext.Clients.Group(channel).addMessage(content);
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        
      
    }
}
