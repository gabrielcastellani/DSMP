using DSMP.Collector.Protocols.Models;

namespace DSMP.Collector.Infrastructure.Channels.Events
{
    public sealed class ChannelMessageReceivedEventArgs : EventArgs
    {
        public DeviceData DeviceData { get; private set; }

        public ChannelMessageReceivedEventArgs(DeviceData deviceData)
        {
            DeviceData = deviceData;
        }
    }
}
