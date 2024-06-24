using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Shared;
using System.Text;

namespace RabbitMQ.Producer;

public static class FanoutExchangeProducer
{
    public static void Publish(IModel channel, string publishMessage)
    {
        // Declares the lifespan of message (optional)
        var ttl = new Dictionary<string, object>
        {
            { "x-message-ttl", 30000 }
        };

        channel.ExchangeDeclare(PubSubSettings.Exchange.Fanout, ExchangeType.Fanout, arguments: ttl);

        var message = new { Name = "Producer", Message = publishMessage };
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        // With or without routing key or header attributes, consumers will receive these messages
        channel.BasicPublish(PubSubSettings.Exchange.Fanout, string.Empty, null, body);
    }
}
