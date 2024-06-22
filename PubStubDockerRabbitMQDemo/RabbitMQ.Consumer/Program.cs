// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer;
using RabbitMQ.Shared;
using System.Text;

var factory = new ConnectionFactory
{
    Uri = new Uri(ChannelSettings.RabbitMQUrl)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

QueueConsumer.Consume(channel);