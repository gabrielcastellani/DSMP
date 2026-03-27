using DSMP.Collector.Domain.Channels.Aggregates;

namespace DSMP.Collector.Domain.Channels
{
    public interface IChannelsService : IDisposable
    {
        Guid AddMqttChannel(ChannelSettings channelSettings);
    }
}
