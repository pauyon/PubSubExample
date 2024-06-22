using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Producer>(); // Register Consumer as a hosted service
    });

await builder.RunConsoleAsync();