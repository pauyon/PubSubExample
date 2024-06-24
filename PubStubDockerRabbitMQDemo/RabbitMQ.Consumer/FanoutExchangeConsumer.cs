using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQ.Shared;
using System.Text;

namespace RabbitMQ.Consumer;

public static class FanoutExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare(PubSubSettings.Exchange.Fanout, ExchangeType.Fanout);

        channel.QueueDeclare(
            PubSubSettings.Queue.Fanout,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        // What binds the consumer to the appropriate channel/queue
        channel.QueueBind(PubSubSettings.Queue.Fanout, PubSubSettings.Exchange.Fanout, string.Empty);

        // Will make consumer fetch 10 messages as once
        channel.BasicQos(0, 10, false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Consuemer received: " + message);
        };

        channel.BasicConsume(PubSubSettings.Queue.Fanout, true, consumer);
        Console.WriteLine("Consumer started");
        Console.ReadLine();
    }
}
