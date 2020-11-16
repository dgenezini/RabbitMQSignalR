using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RabbitMQSample.SignalRConsumer.Hubs
{
    public class PubSubHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceivePubSubMessage", message);
        }
    }
}
