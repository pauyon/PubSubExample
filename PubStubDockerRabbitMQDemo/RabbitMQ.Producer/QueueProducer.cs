using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Shared;
using System.Text;

namespace RabbitMQ.Producer;

public static class QueueProducer
{
    public static void Publish(IModel channel, string publishMessage)
    {
        channel.QueueDeclare(
            PubSubSettings.Queue.Default,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = new { Name = "Producer", Message = publishMessage };
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        channel.BasicPublish(string.Empty, PubSubSettings.Queue.Default, null, body);
    }
}
