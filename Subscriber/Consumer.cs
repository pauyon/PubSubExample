using Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Subscriber;

public class Consumer : BackgroundService
{
    private static readonly string _connectionString = "localhost:6379";
    private static readonly ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(_connectionString);
    private const string _channel = "test-channel";

    private readonly ILogger<Consumer> _logger;

    public Consumer(ILogger<Consumer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = _connection.GetSubscriber();

        await subscriber.SubscribeAsync(_channel, (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<Message>(message);

            //_logger.LogInformation("Received message: {channel} {message}", channel, message); 
            _logger.LogInformation("Received message: {channel} {@message}", channel, messageDeserialized);
        });
    }
}
