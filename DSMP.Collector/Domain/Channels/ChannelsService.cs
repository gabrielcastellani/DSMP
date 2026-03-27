using DSMP.Collector.Domain.Channels.Aggregates;
using DSMP.Collector.Infrastructure.Channels;
using DSMP.Collector.Infrastructure.Channels.Events;
using DSMP.Collector.Protocols.Mqtt;

namespace DSMP.Collector.Domain.Channels
{
    internal sealed class ChannelsService : IChannelsService
    {
        private readonly IChannelFactory _channelFactory;
        private readonly IDictionary<Guid, IChannel> _channels;

        public ChannelsService(IChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
            _channels = new Dictionary<Guid, IChannel>();
        }

        public Guid AddMqttChannel(ChannelSettings channelSettings)
        {
            var channelId = Guid.NewGuid();
            var channel = _channelFactory.CreateChannel(
                protocolSettings: new MqttProtocolSettings
                {
                    Host = channelSettings.Host,
                    Port = channelSettings.Port,
                    Username = channelSettings.Username,
                    Password = channelSettings.Password,
                    Topic =  channelSettings.Topic,
                });

            channel.MessageReceived += Channel_MessageReceived;
            channel.Initialize();

            _channels.Add(channelId, channel);

            return channelId;
        }

        private void Channel_MessageReceived(object? sender, ChannelMessageReceivedEventArgs e)
        {
            Console.WriteLine("Mensagem recebida");
        }

        public void Dispose()
        {
            foreach (var channel in _channels.Values)
            {
                channel.MessageReceived -= Channel_MessageReceived;
                channel.DisposeAsync();
            }

            _channels.Clear();
        }
    }
}
