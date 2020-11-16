using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RabbitMQSample.SignalRConsumer.Hubs
{
    public class QueueHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveQueueMessage", message);
        }
    }
}
