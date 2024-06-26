﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Shared;
using System.Text;

namespace RabbitMQ.Consumer;

public static class QueueConsumer
{
    public static void Consume(IModel channel)
    {
        channel.QueueDeclare(
            PubSubSettings.Queue.Default,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Consuemer received: " + message);
        };

        channel.BasicConsume(PubSubSettings.Queue.Default, true, consumer);
        Console.WriteLine("Consumer started");
        Console.ReadLine();
    }
}
