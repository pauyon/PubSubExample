// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Producer;
using RabbitMQ.Shared;

var factory = new ConnectionFactory
{
    Uri = new Uri(PubSubSettings.Url)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Producer -- sending message...[{i}]");
    //QueueProducer.Publish(channel, $"Hello! [{i}]");
    //DirectExchangeProducer.Publish(channel, $"Hello! [{i}]");
    TopicExchangeProducer.Publish(channel, $"Hello! [{i}]");
    await Task.Delay(2000);
}

Console.WriteLine("Producer done");
Console.ReadLine();