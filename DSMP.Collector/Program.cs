using DSMP.Collector.Domain.Channels;
using DSMP.Collector.Infrastructure.Channels;
using DSMP.Collector.Protocols.Mqtt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Informe a porta do serviço:");
var hostPort = Console.ReadLine();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IChannelFactory, ChannelFactory>();
builder.Services.AddSingleton<IChannelsService, ChannelsService>();
builder.Services.AddSingleton<IMqttProtocolFactory, MqttProtocolFactory>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(Convert.ToInt32(hostPort), listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.Run();
