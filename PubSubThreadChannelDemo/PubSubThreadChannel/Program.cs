// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;

var channel = Channel.CreateBounded<string>(new BoundedChannelOptions(100)
{
    FullMode = BoundedChannelFullMode.DropOldest
}, Console.WriteLine);

var writer = channel.Writer;
var reader = channel.Reader;

var readerTask = Task.Factory.StartNew(async () =>
{
    while (!reader.Completion.IsCompleted)
    {
        var response = await reader.ReadAsync();
        Console.WriteLine(response);
    }
});


for (int i = 0; i < 10; i++)
{
    await writer.WriteAsync($"Writer sent message [{i}]");
    await Task.Delay(1000);
}

await readerTask;
Console.WriteLine();