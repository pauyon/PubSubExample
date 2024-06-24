using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer;

public static class TopicExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare(PubSubSettings.Exchange.TopicExchange, ExchangeType.Topic);

        channel.QueueDeclare(
            PubSubSettings.Queue.Topic,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        // What binds the consumer to the appropriate channel/queue
        channel.QueueBind(PubSubSettings.Queue.Topic, PubSubSettings.Exchange.TopicExchange, PubSubSettings.Routing.WildCardKey);

        // Will make consumer fetch 10 messages as once
        channel.BasicQos(0, 10, false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Consuemer received: " + message);
        };

        channel.BasicConsume(PubSubSettings.Queue.Topic, true, consumer);
        Console.WriteLine("Consumer started");
        Console.ReadLine();
    }
}
