// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

const string channelName = "demo-queue";
const string brokerUrl = "amqp://guest:guest@localhost:5672";

var factory = new ConnectionFactory
{
    Uri = new Uri(brokerUrl)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    channelName,
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

channel.BasicConsume(channelName, true, consumer);
Console.ReadLine();