using Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Redis.Shared;
using StackExchange.Redis;
using System.Text.Json;

namespace Publisher;

public class Producer : BackgroundService
{
    private static readonly ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(RedisSettings.ConnectionString);
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

            //await subscriber.PublishAsync(RedisChannel.Literal(RedisSettings.ChannelName), "Test message");
            await subscriber.PublishAsync(RedisChannel.Literal(RedisSettings.ChannelName), json);

            _logger.LogInformation("Sending message: {@message}", message);

            await Task.Delay(5000, stoppingToken);
        }
    }
}
