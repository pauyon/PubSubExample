using Contracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Redis.Shared;
using StackExchange.Redis;
using System.Text.Json;

namespace Subscriber;

public class Consumer : BackgroundService
{
    private static readonly ConnectionMultiplexer _connection = ConnectionMultiplexer.Connect(RedisSettings.ConnectionString);
    private readonly ILogger<Consumer> _logger;

    public Consumer(ILogger<Consumer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = _connection.GetSubscriber();

        await subscriber.SubscribeAsync(RedisChannel.Literal(RedisSettings.ChannelName), (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<Message>(message);

            //_logger.LogInformation("Received message: {channel} {message}", channel, message); 
            _logger.LogInformation("Received message: {channel} {@message}", channel, messageDeserialized);
        });
    }
}
