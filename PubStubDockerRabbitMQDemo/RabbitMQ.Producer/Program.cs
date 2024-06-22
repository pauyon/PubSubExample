// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RabbitMQ.Client;
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

for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Producer -- sending message...{i}");
    var message = new { Name = "Producer", Message = $"Hello! [{i}]" };
    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

    channel.BasicPublish(string.Empty, channelName, null, body);
    await Task.Delay(2000);
}

Console.WriteLine("Producer done -- stopping...");
Console.ReadLine();