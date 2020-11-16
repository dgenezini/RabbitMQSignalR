using EasyNetQ;
using RabbitMQSample.Common;
using System;
using System.Threading.Tasks;

namespace RabbitMQSample.Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting");

            var RabbitMqUserName = "localrabbituser";
            var RabbitMqPassword = "localrabbitpassword";

            using (var bus = RabbitHutch.CreateBus($"host=rabbitmq;username={RabbitMqUserName};password={RabbitMqPassword}"))
            {
                int I = 1;

                while (true)
                {
                    var PubSubMsg = new PubSubMessage($"Pub/Sub message {I}");
                    var QueueMsg = new QueueMessage($"Queue message {I}");

                    try
                    {
                        bus.PubSub.Publish(PubSubMsg);

                        Console.WriteLine($"Published message: '{PubSubMsg.Text}'");
                    }
                    catch
                    {
                        Console.WriteLine($"Erro publishing message: '{PubSubMsg.Text}'");
                    }

                    try
                    {
                        bus.PubSub.Publish(QueueMsg);

                        Console.WriteLine($"Published message: '{QueueMsg.Text}'");
                    }
                    catch
                    {
                        Console.WriteLine($"Erro publishing message: '{QueueMsg.Text}'");
                    }

                    await Task.Delay(500);

                    I++;
                }
            }
        }
    }
}
