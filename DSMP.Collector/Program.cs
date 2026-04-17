using DSMP.Collector;
using DSMP.Collector.Domain.Channels;
using DSMP.Collector.Infrastructure.Channels;
using DSMP.Collector.Infrastructure.SharedMemory;
using DSMP.Collector.Protocols.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Informe a porta do serviço:");
var hostPort = Console.ReadLine();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IChannelFactory, ChannelFactory>();
builder.Services.AddSingleton<IChannelsService, ChannelsService>();
builder.Services.AddSingleton<IMqttProtocolFactory, MqttProtocolFactory>();
builder.Services.AddSingleton<IMemorySessionRepository, MemorySessionRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "CollectorApp:";
});
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(Convert.ToInt32(hostPort), listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/api/cache", async (IDistributedCache distributedCache) =>
{
    return await distributedCache.GetStringAsync("ChannelName");
});

app.MapGet("/api/cache/add", async (IDistributedCache distributedCache) =>
{
    await distributedCache.SetStringAsync("ChannelName", "MQTT");
});

app.Run();
