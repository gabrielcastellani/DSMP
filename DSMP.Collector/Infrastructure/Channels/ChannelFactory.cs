using DSMP.Collector.Protocols;
using DSMP.Collector.Protocols.Mqtt;
using Microsoft.Extensions.DependencyInjection;

namespace DSMP.Collector.Infrastructure.Channels
{
    internal sealed class ChannelFactory : IChannelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ChannelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IChannel CreateChannel(IProtocolSettings protocolSettings)
        {
            return protocolSettings switch
            {
                MqttProtocolSettings mqttProtocolSettings
                    => CreateMqttChannel(mqttProtocolSettings),
                _ => throw new NotSupportedException("Formato de canal não suportado")
            };
        }

        private Channel CreateMqttChannel(MqttProtocolSettings mqttProtocolSettings)
        {
            return ActivatorUtilities.CreateInstance<Channel>(_serviceProvider, mqttProtocolSettings);
        }
    }
}
