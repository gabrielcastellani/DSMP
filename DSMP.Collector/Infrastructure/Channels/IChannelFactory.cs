using DSMP.Collector.Protocols;

namespace DSMP.Collector.Infrastructure.Channels
{
    public interface IChannelFactory
    {
        IChannel CreateChannel(IProtocolSettings protocolSettings);
    }
}
