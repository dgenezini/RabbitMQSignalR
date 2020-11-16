namespace RabbitMQSample.Common
{
    public class QueueMessage
    {
        public string Text { get; set; }

        public QueueMessage(string text)
        {
            Text = text;
        }
    }
}
