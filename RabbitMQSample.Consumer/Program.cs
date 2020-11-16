using EasyNetQ;
using RabbitMQSample.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQSample.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting!");

            var RabbitMqUserName = "localrabbituser";
            var RabbitMqPassword = "localrabbitpassword";

            using (var RabbitMQBus = RabbitHutch.CreateBus($"host=rabbitmq;username={RabbitMqUserName};password={RabbitMqPassword}"))
            {
                CancellationTokenSource cts = new CancellationTokenSource();

                await RabbitMQBus.PubSub
                    .SubscribeAsync<QueueMessage>("queueconsumer", HandleQueueMessageAsync,
                        c => c.WithAutoDelete(false), cts.Token);

                await RabbitMQBus.PubSub
                    .SubscribeAsync<PubSubMessage>("pubsubconsumer2", HandlePubSubMessageAsync,
                        c => c.WithAutoDelete(false), cts.Token);

                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                await Task.Delay(Timeout.Infinite);
            }
        }

        private static async Task HandlePubSubMessageAsync(PubSubMessage obj, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffff")} - {obj.Text}");
        }

        private static async Task HandleQueueMessageAsync(QueueMessage obj, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.ffff")} - {obj.Text}");
        }
    }
}
