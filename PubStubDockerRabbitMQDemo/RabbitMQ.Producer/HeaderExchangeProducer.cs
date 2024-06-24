using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Shared;
using System.Text;

namespace RabbitMQ.Producer;

public static class HeaderExchangeProducer
{
    public static void Publish(IModel channel, string publishMessage)
    {
        // Declares the lifespan of message (optional)
        var ttl = new Dictionary<string, object>
        {
            { "x-message-ttl", 30000 }
        };

        channel.ExchangeDeclare(PubSubSettings.Exchange.HeaderExchange, ExchangeType.Headers, arguments: ttl);

        var message = new { Name = "Producer", Message = publishMessage };
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        var properties = channel.CreateBasicProperties();
        properties.Headers = PubSubSettings.HeaderExchangeHeaderValues;

        channel.BasicPublish(PubSubSettings.Exchange.HeaderExchange, string.Empty, properties, body);
    }
}
