using DSMP.Collector.Domain.Channels;
using DSMP.Collector.Domain.Channels.Aggregates;
using DSMP.Collector.Infrastructure.Channels;
using DSMP.Collector.Protocols.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IChannelFactory, ChannelFactory>();
builder.Services.AddSingleton<IChannelsService, ChannelsService>();
builder.Services.AddSingleton<IMqttProtocolFactory, MqttProtocolFactory>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

var app = builder.Build();

app.MapPost("/api/channels/default", async (IChannelsService channelsService) =>
{
    var id = 0;

    return id;
});

app.UseHttpsRedirection();
app.Run();
