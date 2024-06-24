// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Consumer;
using RabbitMQ.Shared;

var factory = new ConnectionFactory
{
    Uri = new Uri(PubSubSettings.Url)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//QueueConsumer.Consume(channel);
//DirectExchangeConsumer.Consume(channel);
//TopicExchangeConsumer.Consume(channel);
HeaderExchangeConsumer.Consume(channel);