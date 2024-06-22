using Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Publisher;

public class Producer : BackgroundService
{
    private static readonly string _connectionString = "localhost:6379";
    private static readonly ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(_connectionString);
    private const string _channel = "test-channel";

    private readonly ILogger<Producer> _logger;

    public Producer(ILogger<Producer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = _connection.GetSubscriber();

        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation("Producer running at: {time}", DateTimeOffset.Now);
            var message = new Message(Guid.NewGuid(), DateTime.Now);

            var json = JsonSerializer.Serialize(message);

            //await subscriber.PublishAsync(_channel, "Test message");
            await subscriber.PublishAsync(_channel, json);

            _logger.LogInformation("Sending message: {@message}", message);

            await Task.Delay(5000, stoppingToken);
        }
    }
}
