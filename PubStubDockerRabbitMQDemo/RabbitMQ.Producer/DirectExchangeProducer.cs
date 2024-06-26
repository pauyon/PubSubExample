﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Shared;
using System.Text;

namespace RabbitMQ.Producer;

public static class DirectExchangeProducer
{
    public static void Publish(IModel channel, string publishMessage)
    {
        // Declares the lifespan of message (optional)
        var ttl = new Dictionary<string, object>
        {
            { "x-message-ttl", 30000 }
        };

        channel.ExchangeDeclare(PubSubSettings.Exchange.DirectExchange, ExchangeType.Direct, arguments: ttl);

        var message = new { Name = "Producer", Message = publishMessage };
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        channel.BasicPublish(PubSubSettings.Exchange.DirectExchange, PubSubSettings.Routing.RoutingKey1, null, body);
    }
}
