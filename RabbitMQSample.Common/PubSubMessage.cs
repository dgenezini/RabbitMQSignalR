namespace RabbitMQSample.Common
{
    public class PubSubMessage
    {
        public string Text { get; set; }

        public PubSubMessage(string text)
        {
            Text = text;
        }
    }
}
