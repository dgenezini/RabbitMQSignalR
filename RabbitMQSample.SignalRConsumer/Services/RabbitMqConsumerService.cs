using EasyNetQ;
using Microsoft.AspNetCore.SignalR;
using RabbitMQSample.Common;
using RabbitMQSample.SignalRConsumer.Hubs;
using System;
using System.Threading.Tasks;

namespace RabbitMQSample.SignalRConsumer.Services
{
    public class RabbitMqConsumerService : IDisposable
    {
        private readonly IHubContext<PubSubHub> _PubSubHubContext;
        private readonly IHubContext<QueueHub> _QueueHubContext;
        private IBus _RabbitMQBus;

        public RabbitMqConsumerService(IHubContext<PubSubHub> pubSubHubContext,
            IHubContext<QueueHub> queueHubContext)
        {
            _PubSubHubContext = pubSubHubContext;
            _QueueHubContext = queueHubContext;
        }

        public async Task StartAsync()
        {
            var RabbitMqUserName = "localrabbituser";
            var RabbitMqPassword = "localrabbitpassword";

            _RabbitMQBus = RabbitHutch.CreateBus($"host=rabbitmq;username={RabbitMqUserName};password={RabbitMqPassword}");

            await _RabbitMQBus.PubSub
                .SubscribeAsync<PubSubMessage>("pubsubconsumer1", HandlePubSubMessageAsync);

            await _RabbitMQBus.PubSub
                .SubscribeAsync<QueueMessage>("queueconsumer", HandleQueueMessageAsync);
        }

        private async Task HandlePubSubMessageAsync(PubSubMessage obj)
        {
            await _PubSubHubContext.Clients.All
                .SendAsync("ReceivePubSubMessage", $"{DateTime.Now.ToString("HH:mm:ss.ffff")} - {obj.Text}");
        }

        private async Task HandleQueueMessageAsync(QueueMessage obj)
        {
            await _QueueHubContext.Clients.All
                .SendAsync("ReceiveQueueMessage", $"{DateTime.Now.ToString("HH:mm:ss.ffff")} - {obj.Text}");
        }

        public void Dispose()
        {
            _RabbitMQBus.Dispose();
        }
    }
}
