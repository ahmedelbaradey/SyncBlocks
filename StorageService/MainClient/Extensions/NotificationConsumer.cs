using MainClient.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Shared.DataTransferObjects.SharedObject;

namespace MainClient.Extensions
{
    public class NotificationConsumer : IConsumer<SharedObjectDto>
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public NotificationConsumer(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<SharedObjectDto> _context)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Notifiation From MetaData Service",""+ _context.Message.Name + " Uploaded to Blob Storage and Metadata Updated");// SendMessage("Ahmed",_context.Message.Name);
        }

    }
}
